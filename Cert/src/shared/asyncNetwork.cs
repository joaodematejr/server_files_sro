using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Threading;

namespace asyncNetwork
{
    public class Async
    {
        public enum asyncOperation { Accept, Connect };

        public interface IAsyncInterface
        {
            bool OnConnect(asyncContext context);
            bool OnReceive(asyncContext context, byte[] buffer, int count);
            void OnDisconnect(asyncContext context);
            void OnError(asyncContext context, object user);
            void OnTick(asyncContext context);
        }

        public class asyncToken
        {
            public Socket Socket { get; set; }
            public IAsyncInterface Interface { get; set; }
            public object User { get; set; }
        }

        public class asyncContext
        {
            public asyncState State { get; set; }
            public Guid Guid { get { return guid; } }
            public IAsyncInterface Interface { get; set; }
            public object User { get; set; }

            Guid guid;

            public asyncContext()
            {
                guid = Guid.NewGuid();
            }

            public void Disconnect()
            {
                State.Disconnect();
            }

            public void Send(byte[] buffer)
            {
                State.Write(new asyncBuffer(buffer));
            }

            public void Send(byte[] buffer, int offset, int count)
            {
                State.Write(new asyncBuffer(buffer, offset, count));
            }
        }

        public class asyncBuffer
        {
            public byte[] Buffer { get; set; }
            public int Offset { get; set; }
            public int Count { get; set; }

            public asyncBuffer(byte[] buffer, int offset, int count)
            {
                Buffer = buffer;
                Offset = offset;
                Count = count;
            }

            public asyncBuffer(byte[] buffer)
            {
                Buffer = buffer;
                Offset = 0;
                Count = buffer.Length;
            }
        }

        public class asyncState
        {
            Socket m_socket;
            asyncNetwork m_server;

            SocketAsyncEventArgs m_read_event_args;
            SocketAsyncEventArgs m_write_event_args;

            byte[] m_read_buffer;

            Queue<asyncBuffer> m_write_buffers;
            asyncBuffer m_current_write_buffer;

            asyncOperation m_operation;

            public asyncContext Context { get; set; }
            public asyncOperation Operation { get { return m_operation; } }
            public EndPoint EndPoint { get { return m_socket.RemoteEndPoint; } }

            public asyncState(asyncNetwork server, Socket socket, asyncOperation operation, IAsyncInterface interface_, object user)
            {
                m_server = server;
                m_socket = socket;
                m_operation = operation;

                m_current_write_buffer = null;

                m_read_buffer = new byte[8192];

                m_read_event_args = new SocketAsyncEventArgs();
                m_read_event_args.Completed += OnIO;
                m_read_event_args.UserToken = this;
                m_read_event_args.SetBuffer(m_read_buffer, 0, m_read_buffer.Length);

                m_write_event_args = new SocketAsyncEventArgs();
                m_write_event_args.Completed += OnIO;
                m_write_event_args.UserToken = this;

                m_write_buffers = new Queue<asyncBuffer>();

                Context = new asyncContext();
                Context.State = this;
                Context.User = user;
                Context.Interface = interface_;
            }

            internal void Disconnect()
            {
                try
                {
                    m_socket.Shutdown(SocketShutdown.Both);
                }
                catch (Exception) { }
            }

            internal void Cleanup()
            {
                try
                {
                    m_socket.Shutdown(SocketShutdown.Both);
                }
                catch (Exception) { }

                try
                {
                    m_socket.Close();
                }
                catch (Exception) { }
            }

            void OnIO(object sender, SocketAsyncEventArgs e)
            {
                switch (e.LastOperation)
                {
                    case SocketAsyncOperation.Receive:
                        {
                            ProcessRecv(e);
                        }
                        break;

                    case SocketAsyncOperation.Send:
                        {
                            ProcessSend(e);
                        }
                        break;

                    default:
                        throw new NotImplementedException("The code will only handle send and receive operations.");
                }
            }

            void ProcessSend(SocketAsyncEventArgs e)
            {
                asyncState state = e.UserToken as asyncState;

                if (state.ProcessWrite(e))
                {
                    return;
                }

                m_server.RemoveState(this);
            }

            void ProcessRecv(SocketAsyncEventArgs e)
            {
                asyncState state = e.UserToken as asyncState;

                if (state.ProcessRead(e))
                {
                    return;
                }

                m_server.RemoveState(this);
            }

            internal void Read()
            {
                if (!m_socket.ReceiveAsync(m_read_event_args))
                {
                    ProcessRecv(m_read_event_args);
                }
            }

            bool ProcessRead(SocketAsyncEventArgs e)
            {
                try
                {
                    if (e.BytesTransferred <= 0 || e.SocketError != SocketError.Success)
                    {
                        try
                        {
                            Context.Interface.OnDisconnect(Context);
                        }
                        catch (Exception) { }
                        Cleanup();
                        return false;
                    }

                    bool result = false;
                    try
                    {
                        result = Context.Interface.OnReceive(Context, m_read_buffer, e.BytesTransferred);
                    }
                    catch (Exception) { }

                    if (!result)
                    {
                        try
                        {
                            Context.Interface.OnDisconnect(Context);
                        }
                        catch (Exception) { }
                        Cleanup();
                        return false;
                    }

                    Read();
                }
                catch (Exception)
                {
                    Cleanup();
                    return false;
                }
                return true;
            }

            void DispatchWrite()
            {
                if (!m_socket.SendAsync(m_write_event_args)) // Attempt the write
                {
                    ProcessSend(m_write_event_args); // It completed immediately, we must manually invoke the handler
                }
            }

            void CheckWrite()
            {
                lock (m_write_buffers)
                {
                    if (m_current_write_buffer == null) // If no write is in progress
                    {
                        if (m_write_buffers.Count > 0) // There is pending data
                        {
                            m_current_write_buffer = m_write_buffers.Dequeue(); // Get the next buffer

                            m_write_event_args.SetBuffer(m_current_write_buffer.Buffer, m_current_write_buffer.Offset, m_current_write_buffer.Count); // Setup the async write

                            DispatchWrite(); // Begin the write
                        }
                    }
                }
            }

            bool ProcessWrite(SocketAsyncEventArgs e)
            {
                try
                {
                    if (e.BytesTransferred <= 0 || e.SocketError != SocketError.Success) // Check for errors
                    {
                        try
                        {
                            Context.Interface.OnDisconnect(Context);
                        }
                        catch (Exception) { }
                        Cleanup();
                        return false;
                    }

                    lock (m_write_buffers)
                    {
                        m_current_write_buffer.Offset += e.BytesTransferred; // Update index
                        m_current_write_buffer.Count -= e.BytesTransferred; // Update count

                        if (m_current_write_buffer.Count > 0) // If there is data left to be sent
                        {
                            m_write_event_args.SetBuffer(m_current_write_buffer.Offset, m_current_write_buffer.Count); // Setup the next async write
                            DispatchWrite(); // Begin the next write
                            return true; // Everything is fine
                        }

                        m_current_write_buffer = null; // Clear out the last write object
                        CheckWrite(); // Perform the logic to check for the next write
                    }
                }
                catch (Exception)
                {
                    Cleanup();
                    return false;
                }
                return true; // Everything is fine
            }

            internal void Write(asyncBuffer buffer)
            {
                lock (m_write_buffers)
                {
                    m_write_buffers.Enqueue(buffer); // Save the buffer

                    CheckWrite(); // Perform the logic to check for the next write. NOTE: This stays inside the lock to keep FIFO order.
                }
            }
        }

        public class asyncNetwork
        {
            List<asyncState> states;

            public asyncNetwork()
            {
                states = new List<asyncState>();
            }

            public void Tick()
            {
                lock (states)
                {
                    foreach (asyncState state in states)
                    {
                        try
                        {
                            state.Context.Interface.OnTick(state.Context);
                        }
                        catch (Exception) { }
                    }
                }
            }

            public void Connect(String host, int port, IAsyncInterface interface_)
            {
                Connect(host, port, interface_, null);
            }

            public void Connect(String host, int port, IAsyncInterface interface_, object user)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress address = null;
                if (!IPAddress.TryParse(host, out address))
                {
                    IPHostEntry host_entry = Dns.GetHostEntry(host);
                    address = host_entry.AddressList[0];
                }

                asyncToken token = new asyncToken();
                token.Socket = socket;
                token.User = user;
                token.Interface = interface_;

                SocketAsyncEventArgs connectEvtArgs = new SocketAsyncEventArgs();
                connectEvtArgs.UserToken = token;
                connectEvtArgs.Completed += NetworkOnConnect;
                connectEvtArgs.RemoteEndPoint = new IPEndPoint(address, port);
                ProcessConnect(connectEvtArgs);
            }

            public void Accept(String host, int port, int outstanding, IAsyncInterface interface_)
            {
                Accept(host, port, outstanding, interface_, null);
            }

            public void Accept(String host, int port, int outstanding, IAsyncInterface interface_, object user)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IPAddress address = null;
                if (!IPAddress.TryParse(host, out address))
                {
                    IPHostEntry host_entry = Dns.GetHostEntry(host);
                    address = host_entry.AddressList[0];
                }
                socket.Bind(new IPEndPoint(address, port));

                socket.Listen(outstanding);

                for (int x = 0; x < outstanding; ++x)
                {
                    asyncToken token = new asyncToken();
                    token.Socket = socket;
                    token.User = user;
                    token.Interface = interface_;

                    SocketAsyncEventArgs acceptEvtArgs = new SocketAsyncEventArgs();
                    acceptEvtArgs.UserToken = token;
                    acceptEvtArgs.Completed += NetworkOnAccept;
                    ProcessAccept(acceptEvtArgs);
                }
            }

            private void DispatchAccept(object param)
            {
                SocketAsyncEventArgs e = (SocketAsyncEventArgs)param;

                NetworkOnAccept(null, e);
            }

            private void ProcessAccept(SocketAsyncEventArgs e)
            {
                asyncToken token = (asyncToken)e.UserToken;

                e.AcceptSocket = null;

                if (!token.Socket.AcceptAsync(e))
                {
                    ThreadPool.QueueUserWorkItem(DispatchAccept, e);
                }
            }

            private void NetworkOnAccept(object sender, SocketAsyncEventArgs e)
            {
                asyncToken token = (asyncToken)e.UserToken;

                Socket socket = e.AcceptSocket;

                ProcessAccept(e); // Start the next accept asap.

                if (socket == null)
                {
                    return; // Ignore errors because there is nothing to do
                }

                asyncState state = new asyncState(this, socket, asyncOperation.Accept, token.Interface, token.User); // Now handle the current connection.

                bool result = false;
                try
                {
                    result = state.Context.Interface.OnConnect(state.Context);
                }
                catch (Exception) { }
                if (!result)
                {
                    try
                    {
                        state.Context.Interface.OnError(state.Context, token.User); // Ensure the user can cleanup anything before the object dies
                    }
                    catch (Exception) { }

                    state.Cleanup(); // Cleanup the socket

                    return;
                }

                try
                {
                    state.Read(); // Begin receiving data on the socket
                }
                catch (Exception)
                {
                    state.Cleanup(); // Cleanup the object
                    return;
                }

                AddState(state); // Store the state to keep it alive
            }

            private void ProcessConnect(SocketAsyncEventArgs e)
            {
                asyncToken token = (asyncToken)e.UserToken;

                if (!token.Socket.ConnectAsync(e))
                {
                    NetworkOnConnect(null, e);
                }
            }

            private void NetworkOnConnect(object sender, SocketAsyncEventArgs e)
            {
                asyncToken token = (asyncToken)e.UserToken;

                Socket socket = e.ConnectSocket;

                if (socket == null) // The connection failed
                {
                    token.Interface.OnError(null, token.User); // There is no state object yet, so pass the user object
                    return;
                }

                asyncState state = new asyncState(this, socket, asyncOperation.Connect, token.Interface, token.User); // Now handle the current connection.

                bool result = false;
                try
                {
                    result = state.Context.Interface.OnConnect(state.Context);
                }
                catch (Exception) { }

                if (!result)
                {
                    try
                    {
                        state.Context.Interface.OnError(state.Context, token.User); // Ensure the user can cleanup anything before the object dies
                    }
                    catch (Exception) { }

                    state.Cleanup(); // Cleanup the object
                    return;
                }

                try
                {
                    state.Read(); // Begin receiving data on the socket
                }
                catch (Exception)
                {
                    state.Cleanup(); // Cleanup the object
                    return;
                }

                AddState(state); // Store the state to keep it alive
            }

            public void AddState(asyncState state)
            {
                lock (states)
                {
                    states.Add(state);
                }
            }

            public void RemoveState(asyncState state)
            {
                lock (states)
                {
                    states.Remove(state);
                }
            }
        }
    }
}
