using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SilkroadSecurityApi;
using asyncNetwork;
using System.Threading;
using System.IO;

class CertificationServerData
{
    public bool m_connected;
    public Security m_security = new Security();
    public byte[] m_certification_buffer;

    public CertificationServerData()
    {
        m_security.GenerateSecurity(false, false, false);
        m_connected = false;
    }
}

class CertificationServerInterface : Async.IAsyncInterface
{
    public CertificationServerInterface()
    {
    }

    public bool OnConnect(Async.asyncContext context)
    {
        CertificationServerData context_data = new CertificationServerData();

        context_data.m_certification_buffer = (byte[])context.User;
        context_data.m_connected = true;
        context.User = context_data;

        return true;
    }

    public bool OnReceive(Async.asyncContext context, byte[] buffer, int count)
    {
        CertificationServerData context_data = (CertificationServerData)context.User;

        try
        {
            context_data.m_security.Recv(buffer, 0, count);
            List<Packet> packets = context_data.m_security.TransferIncoming();
            if (packets != null)
            {
                foreach (Packet packet in packets)
                {
                    byte[] payload = packet.GetBytes();
                    Console.WriteLine("[{7}][{0:X4}][{1} bytes]{2}{3}{4}{5}{6}", packet.Opcode, payload.Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Environment.NewLine, Utility.HexDump(payload), Environment.NewLine, context.Guid);

                    if (packet.Opcode == 0x5000) // Ignore
                    {
                    }
                    else if (packet.Opcode == 0x9000) // Ignore
                    {
                    }
                    else if (packet.Opcode == 0x2001)
                    {
                        String name = packet.ReadAscii();
                        byte flag = packet.ReadUInt8();

                        if (flag == 0)
                        {
                            // todo //
                        }
                        else if (flag == 1)
                        {
                            if (name == "GlobalManager")
                            {
                                Packet response = new Packet(0x2001);
                                response.WriteAscii("Certification");
                                response.WriteUInt8(1);
                                context_data.m_security.Send(response);
                            }
                            else
                            {
                                // todo //
                            }
                        }
                    }
                    else if (packet.Opcode == 0x6003)
                    {
                        String name = packet.ReadAscii();
                        String ip = packet.ReadAscii();

                        // todo: verify data against certification info //

                        Packet response = new Packet(0xA003, false, true);
                        response.WriteUInt8Array(context_data.m_certification_buffer);
                        context_data.m_security.Send(response);
                    }
                    else if (packet.Opcode == 0x2005)
                    {
                        byte unk1 = packet.ReadUInt8();
                        if (unk1 == 1)
                        {
                            byte unk2 = packet.ReadUInt8();
                            byte unk3 = packet.ReadUInt8();
                            UInt16 node_id = packet.ReadUInt16(); // node id
                            UInt32 unk5 = packet.ReadUInt32();
                            byte unk6 = packet.ReadUInt8();
                        }
                        else if (unk1 == 2)
                        {
                            byte unk2 = packet.ReadUInt8();
                            byte unk3 = packet.ReadUInt8();
                            UInt32 link_id = packet.ReadUInt32(); // link id
                            UInt32 unk5 = packet.ReadUInt32();
                            byte unk6 = packet.ReadUInt8();
                        }

                        // todo: 00499968  |.  B9 05200000   MOV ECX, 2005                            ;  3

                        // todo: 0049A666  |.  B9 05200000   MOV ECX, 2005                            ;  4

                        Console.Write("");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return false;
        }

        return true;
    }

    public void OnDisconnect(Async.asyncContext context)
    {
        CertificationServerData context_data = (CertificationServerData)context.User;
        context_data.m_connected = false;
    }

    public void OnError(Async.asyncContext context, object user)
    {
        if (context != null && context.User != null)
        {
            CertificationServerData context_data = (CertificationServerData)context.User;
            context_data.m_connected = false;
        }
    }

    public void OnTick(Async.asyncContext context)
    {
        CertificationServerData context_data = (CertificationServerData)context.User;
        if (context_data == null)
            return;

        if (!context_data.m_connected)
            return;

        List<KeyValuePair<TransferBuffer, Packet>> buffers = context_data.m_security.TransferOutgoing();
        if (buffers != null)
        {
            foreach (KeyValuePair<TransferBuffer, Packet> buffer in buffers)
            {
                context.Send(buffer.Key.Buffer, 0, buffer.Key.Size);
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        srCertification certification = new srCertification();
        byte[] certification_buffer = null;

        try
        {
            using (BinaryReader br = new BinaryReader(new FileStream(args[0], FileMode.Open)))
            {
                BinaryReaderVisitStream brvs = new BinaryReaderVisitStream(br);
                srCertification_Processor.read_binary(certification, brvs);
            }

            BinaryWriterVisitStream bwvs = new BinaryWriterVisitStream();
            srCertification_Processor.write_binary(certification, bwvs);

            byte[] buffer = bwvs.MemoryStream.ToArray();
            certification_buffer = new byte[buffer.Length];
            Buffer.BlockCopy(buffer, 0, certification_buffer, 0, buffer.Length);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return;
        }

        srCertification.srNodeType certification_node_type = null;
        srCertification.srNodeData certification_node_data = null;

        foreach (var obj in certification.srNodeTypes)
        {
            if (obj.name.value == "Certification Manager")
            {
                certification_node_type = obj;
                break;
            }
        }

        if (certification_node_type == null)
        {
            Console.WriteLine("Error: Could not locate a \"Certification Manager\" node type.");
            return;
        }

        foreach (var obj in certification.srNodeDatas)
        {
            if (obj.node_type == certification_node_type.id)
            {
                certification_node_data = obj;
                break;
            }
        }

        if (certification_node_data == null)
        {
            Console.WriteLine("Error: Could not locate a \"Certification Manager\" node.");
            return;
        }

        Async.asyncNetwork network = new Async.asyncNetwork();

        CertificationServerInterface certificationServerInterface = new CertificationServerInterface();
        network.Accept(certification_node_type.wip.value, certification_node_data.port, 5, certificationServerInterface, certification_buffer);

        Console.WriteLine("Status: Certification Server started on {0}:{1}", certification_node_type.wip.value, certification_node_data.port);
        Console.WriteLine("Status: Press ESC to exit...");

        while (true)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
            network.Tick();
            Thread.Sleep(1);
        }
    }
}
