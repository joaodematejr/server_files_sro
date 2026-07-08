using System;
using System.IO;
using System.Text;
using System.Runtime.InteropServices;
using System.Xml.Linq;

public class PaddedString_16
{
    public String value;
    public PaddedString_16(String val) { value = val; }
}
public class PaddedString_32
{
    public String value;
    public PaddedString_32(String val) { value = val; }
}
public class PaddedString_64
{
    public String value;
    public PaddedString_64(String val) { value = val; }
}
public class PaddedString_256
{
    public String value;
    public PaddedString_256(String val) { value = val; }
}

public class VisitStream
{
    public VisitStream() { }
    // core types
    public virtual VisitStream visit(String name, ref byte value) { return this; }
    public virtual VisitStream visit(String name, ref sbyte value) { return this; }
    public virtual VisitStream visit(String name, ref ushort value) { return this; }
    public virtual VisitStream visit(String name, ref short value) { return this; }
    public virtual VisitStream visit(String name, ref uint value) { return this; }
    public virtual VisitStream visit(String name, ref int value) { return this; }
    public virtual VisitStream visit(String name, ref ulong value) { return this; }
    public virtual VisitStream visit(String name, ref long value) { return this; }
    public virtual VisitStream visit(String name, ref float value) { return this; }
    public virtual VisitStream visit(String name, ref double value) { return this; }
    public virtual VisitStream visit(String name, ref string value) { return this; }
    // custom types 1
    public virtual VisitStream visit(String name, ref PaddedString_16 value) { return this; }
    public virtual VisitStream visit(String name, ref PaddedString_32 value) { return this; }
    public virtual VisitStream visit(String name, ref PaddedString_64 value) { return this; }
    public virtual VisitStream visit(String name, ref PaddedString_256 value) { return this; }
    // etc...
}

public class INIWriterVisitStream : VisitStream
{
    [DllImport("KERNEL32.DLL", EntryPoint = "WritePrivateProfileStringW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern int WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFilename);

    public string ini = "";
    public string section = "";

    public override VisitStream visit(String name, ref byte value) { if (name != "<ignore>") { WritePrivateProfileString(section, name, String.Format("{0}", value), ini); } return this; }
    public override VisitStream visit(String name, ref sbyte value) { if (name != "<ignore>") { WritePrivateProfileString(section, name, String.Format("{0}", value), ini); } return this; }
    public override VisitStream visit(String name, ref ushort value) { if (name != "<ignore>") { WritePrivateProfileString(section, name, String.Format("{0}", value), ini); } return this; }
    public override VisitStream visit(String name, ref short value) { if (name != "<ignore>") { WritePrivateProfileString(section, name, String.Format("{0}", value), ini); } return this; }
    public override VisitStream visit(String name, ref uint value) { if (name != "<ignore>") { WritePrivateProfileString(section, name, String.Format("{0}", value), ini); } return this; }
    public override VisitStream visit(String name, ref int value) { if (name != "<ignore>") { WritePrivateProfileString(section, name, String.Format("{0}", value), ini); } return this; }
    public override VisitStream visit(String name, ref ulong value) { if (name != "<ignore>") { WritePrivateProfileString(section, name, String.Format("{0}", value), ini); } return this; }
    public override VisitStream visit(String name, ref long value) { if (name != "<ignore>") { WritePrivateProfileString(section, name, String.Format("{0}", value), ini); } return this; }
    public override VisitStream visit(String name, ref float value) { if (name != "<ignore>") { WritePrivateProfileString(section, name, String.Format("{0}", value), ini); } return this; }
    public override VisitStream visit(String name, ref double value) { if (name != "<ignore>") { WritePrivateProfileString(section, name, String.Format("{0}", value), ini); } return this; }
    public override VisitStream visit(String name, ref string value) { if (name != "<ignore>") { WritePrivateProfileString(section, name, String.Format("{0}", value), ini); } return this; }

    public override VisitStream visit(String name, ref PaddedString_16 value) { if (name != "<ignore>") { if (value.value.Length >= 16) { throw new NotImplementedException("[visit] The padded string is too long."); } WritePrivateProfileString(section, name, String.Format("{0}", value.value), ini); } return this; }
    public override VisitStream visit(String name, ref PaddedString_32 value) { if (name != "<ignore>") { if (value.value.Length >= 32) { throw new NotImplementedException("[visit] The padded string is too long."); } WritePrivateProfileString(section, name, String.Format("{0}", value.value), ini); } return this; }
    public override VisitStream visit(String name, ref PaddedString_64 value) { if (name != "<ignore>") { if (value.value.Length >= 64) { throw new NotImplementedException("[visit] The padded string is too long."); } WritePrivateProfileString(section, name, String.Format("{0}", value.value), ini); } return this; }
    public override VisitStream visit(String name, ref PaddedString_256 value) { if (name != "<ignore>") { if (value.value.Length >= 256) { throw new NotImplementedException("[visit] The padded string is too long."); } WritePrivateProfileString(section, name, String.Format("{0}", value.value), ini); } return this; }
}
public class INIReaderVisitStream : VisitStream
{
    [DllImport("KERNEL32.DLL", EntryPoint = "GetPrivateProfileStringW", SetLastError = true, CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
    public static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnString, int nSize, string lpFilename);

    public string ini = "";
    public string section = "";

    public override VisitStream visit(String name, ref byte value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = Byte.Parse(sb.ToString()); } return this; }
    public override VisitStream visit(String name, ref sbyte value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = SByte.Parse(sb.ToString()); } return this; }
    public override VisitStream visit(String name, ref ushort value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = UInt16.Parse(sb.ToString()); } return this; }
    public override VisitStream visit(String name, ref short value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = Int16.Parse(sb.ToString()); } return this; }
    public override VisitStream visit(String name, ref uint value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = UInt32.Parse(sb.ToString()); } return this; }
    public override VisitStream visit(String name, ref int value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = Int32.Parse(sb.ToString()); } return this; }
    public override VisitStream visit(String name, ref ulong value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = UInt64.Parse(sb.ToString()); } return this; }
    public override VisitStream visit(String name, ref long value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = Int64.Parse(sb.ToString()); } return this; }
    public override VisitStream visit(String name, ref float value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = Single.Parse(sb.ToString()); } return this; }
    public override VisitStream visit(String name, ref double value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = Double.Parse(sb.ToString()); } return this; }
    public override VisitStream visit(String name, ref string value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = sb.ToString(); } return this; }

    public override VisitStream visit(String name, ref PaddedString_16 value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = new PaddedString_16(sb.ToString()); if (value.value.Length >= 16) { throw new NotImplementedException("[visit] The padded string is too long."); } } return this; }
    public override VisitStream visit(String name, ref PaddedString_32 value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = new PaddedString_32(sb.ToString()); if (value.value.Length >= 32) { throw new NotImplementedException("[visit] The padded string is too long."); } } return this; }
    public override VisitStream visit(String name, ref PaddedString_64 value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = new PaddedString_64(sb.ToString()); if (value.value.Length >= 64) { throw new NotImplementedException("[visit] The padded string is too long."); } } return this; }
    public override VisitStream visit(String name, ref PaddedString_256 value) { if (name != "<ignore>") { StringBuilder sb = new StringBuilder(512); GetPrivateProfileString(section, name, "", sb, sb.Capacity, ini); value = new PaddedString_256(sb.ToString()); if (value.value.Length >= 256) { throw new NotImplementedException("[visit] The padded string is too long."); } } return this; }
}

public class XMLWriterVisitStream : VisitStream
{
    public XElement current_element = null;

    public XMLWriterVisitStream() { }

    public override VisitStream visit(String name, ref byte value) { if (name != "<ignore>") { current_element.Add(new XElement(name, value)); } return this; }
    public override VisitStream visit(String name, ref sbyte value) { if (name != "<ignore>") { current_element.Add(new XElement(name, value)); } return this; }
    public override VisitStream visit(String name, ref ushort value) { if (name != "<ignore>") { current_element.Add(new XElement(name, value)); } return this; }
    public override VisitStream visit(String name, ref short value) { if (name != "<ignore>") { current_element.Add(new XElement(name, value)); } return this; }
    public override VisitStream visit(String name, ref uint value) { if (name != "<ignore>") { current_element.Add(new XElement(name, value)); } return this; }
    public override VisitStream visit(String name, ref int value) { if (name != "<ignore>") { current_element.Add(new XElement(name, value)); } return this; }
    public override VisitStream visit(String name, ref ulong value) { if (name != "<ignore>") { current_element.Add(new XElement(name, value)); } return this; }
    public override VisitStream visit(String name, ref long value) { if (name != "<ignore>") { current_element.Add(new XElement(name, value)); } return this; }
    public override VisitStream visit(String name, ref float value) { if (name != "<ignore>") { current_element.Add(new XElement(name, value)); } return this; }
    public override VisitStream visit(String name, ref double value) { if (name != "<ignore>") { current_element.Add(new XElement(name, value)); } return this; }
    public override VisitStream visit(String name, ref string value) { if (name != "<ignore>") { current_element.Add(new XElement(name, value)); } return this; }

    public override VisitStream visit(String name, ref PaddedString_16 value) { if (name != "<ignore>") { if (value.value.Length >= 16) { throw new NotImplementedException("[visit] The padded string is too long."); } current_element.Add(new XElement(name, value.value)); } return this; }
    public override VisitStream visit(String name, ref PaddedString_32 value) { if (name != "<ignore>") { if (value.value.Length >= 32) { throw new NotImplementedException("[visit] The padded string is too long."); } current_element.Add(new XElement(name, value.value)); } return this; }
    public override VisitStream visit(String name, ref PaddedString_64 value) { if (name != "<ignore>") { if (value.value.Length >= 64) { throw new NotImplementedException("[visit] The padded string is too long."); } current_element.Add(new XElement(name, value.value)); } return this; }
    public override VisitStream visit(String name, ref PaddedString_256 value) { if (name != "<ignore>") { if (value.value.Length >= 256) { throw new NotImplementedException("[visit] The padded string is too long."); } current_element.Add(new XElement(name, value.value)); } return this; }
}
public class XMLReaderVisitStream : VisitStream
{
    public XElement current_element = null;

    public XMLReaderVisitStream() { }

    public override VisitStream visit(String name, ref byte value) { if (name != "<ignore>") { value = (byte)(int)current_element.Element(name); } return this; }
    public override VisitStream visit(String name, ref sbyte value) { if (name != "<ignore>") { value = (sbyte)(int)current_element.Element(name); } return this; }
    public override VisitStream visit(String name, ref ushort value) { if (name != "<ignore>") { value = (ushort)(int)current_element.Element(name); } return this; }
    public override VisitStream visit(String name, ref short value) { if (name != "<ignore>") { value = (short)(int)current_element.Element(name); } return this; }
    public override VisitStream visit(String name, ref uint value) { if (name != "<ignore>") { value = (uint)current_element.Element(name); } return this; }
    public override VisitStream visit(String name, ref int value) { if (name != "<ignore>") { value = (int)current_element.Element(name); } return this; }
    public override VisitStream visit(String name, ref ulong value) { if (name != "<ignore>") { value = (ulong)current_element.Element(name); } return this; }
    public override VisitStream visit(String name, ref long value) { if (name != "<ignore>") { value = (long)current_element.Element(name); } return this; }
    public override VisitStream visit(String name, ref float value) { if (name != "<ignore>") { value = (float)current_element.Element(name); } return this; }
    public override VisitStream visit(String name, ref double value) { if (name != "<ignore>") { value = (double)current_element.Element(name); } return this; }
    public override VisitStream visit(String name, ref string value) { if (name != "<ignore>") { value = (string)current_element.Element(name); } return this; }

    public override VisitStream visit(String name, ref PaddedString_16 value) { if (name != "<ignore>") { value = new PaddedString_16((string)current_element.Element(name)); if (value.value.Length >= 16) { throw new NotImplementedException("[visit] The padded string is too long."); } } return this; }
    public override VisitStream visit(String name, ref PaddedString_32 value) { if (name != "<ignore>") { value = new PaddedString_32((string)current_element.Element(name)); if (value.value.Length >= 32) { throw new NotImplementedException("[visit] The padded string is too long."); } } return this; }
    public override VisitStream visit(String name, ref PaddedString_64 value) { if (name != "<ignore>") { value = new PaddedString_64((string)current_element.Element(name)); if (value.value.Length >= 64) { throw new NotImplementedException("[visit] The padded string is too long."); } } return this; }
    public override VisitStream visit(String name, ref PaddedString_256 value) { if (name != "<ignore>") { value = new PaddedString_256((string)current_element.Element(name)); if (value.value.Length >= 256) { throw new NotImplementedException("[visit] The padded string is too long."); } } return this; }
}

public class BinaryWriterVisitStream : VisitStream
{
    BinaryWriter m_writer;
    MemoryStream m_memory;

    public MemoryStream MemoryStream { get { return m_memory; } }

    public BinaryWriterVisitStream()
    {
        m_memory = new MemoryStream();
        m_writer = new BinaryWriter(m_memory);
    }

    public BinaryWriterVisitStream(BinaryWriter writer)
    {
        m_writer = writer;
    }

    public override VisitStream visit(String name, ref byte value) { m_writer.Write(value); return this; }
    public override VisitStream visit(String name, ref sbyte value) { m_writer.Write(value); return this; }
    public override VisitStream visit(String name, ref ushort value) { m_writer.Write(value); return this; }
    public override VisitStream visit(String name, ref short value) { m_writer.Write(value); return this; }
    public override VisitStream visit(String name, ref uint value) { m_writer.Write(value); return this; }
    public override VisitStream visit(String name, ref int value) { m_writer.Write(value); return this; }
    public override VisitStream visit(String name, ref ulong value) { m_writer.Write(value); return this; }
    public override VisitStream visit(String name, ref long value) { m_writer.Write(value); return this; }
    public override VisitStream visit(String name, ref float value) { m_writer.Write(value); return this; }
    public override VisitStream visit(String name, ref double value) { m_writer.Write(value); return this; }
    public override VisitStream visit(String name, ref string value) { throw new NotImplementedException("[BinaryWriterVisitStream] The \"String\" type is not implemented by default."); }

    // null terminated, fixed size strings
    private void WritePaddedString(string value, int padding)
    {
        byte[] buffer = Encoding.ASCII.GetBytes(value);
        if (buffer.Length >= padding)
        {
            throw new NotImplementedException("[WritePaddedString] The padded string is too long.");
        }
        m_writer.Write(buffer);
        for (int x = 0; x < padding - buffer.Length; ++x)
        {
            m_writer.Write((byte)0);
        }
    }
    public override VisitStream visit(String name, ref PaddedString_16 value) { WritePaddedString(value.value, 16); return this; }
    public override VisitStream visit(String name, ref PaddedString_32 value) { WritePaddedString(value.value, 32); return this; }
    public override VisitStream visit(String name, ref PaddedString_64 value) { WritePaddedString(value.value, 64); return this; }
    public override VisitStream visit(String name, ref PaddedString_256 value) { WritePaddedString(value.value, 256); return this; }
}
public class BinaryReaderVisitStream : VisitStream
{
    BinaryReader m_reader;
    MemoryStream m_memory;

    public BinaryReaderVisitStream(MemoryStream memory)
    {
        m_memory = memory;
        m_reader = new BinaryReader(m_memory);
    }

    public BinaryReaderVisitStream(BinaryReader reader)
    {
        m_reader = reader;
    }

    public override VisitStream visit(String name, ref byte value) { value = m_reader.ReadByte(); return this; }
    public override VisitStream visit(String name, ref sbyte value) { value = m_reader.ReadSByte(); return this; }
    public override VisitStream visit(String name, ref ushort value) { value = m_reader.ReadUInt16(); return this; }
    public override VisitStream visit(String name, ref short value) { value = m_reader.ReadInt16(); return this; }
    public override VisitStream visit(String name, ref uint value) { value = m_reader.ReadUInt32(); return this; }
    public override VisitStream visit(String name, ref int value) { value = m_reader.ReadInt32(); return this; }
    public override VisitStream visit(String name, ref ulong value) { value = m_reader.ReadUInt64(); return this; }
    public override VisitStream visit(String name, ref long value) { value = m_reader.ReadInt64(); return this; }
    public override VisitStream visit(String name, ref float value) { value = m_reader.ReadSingle(); return this; }
    public override VisitStream visit(String name, ref double value) { value = m_reader.ReadDouble(); return this; }
    public override VisitStream visit(String name, ref string value) { throw new NotImplementedException("[BinaryReaderVisitStream] The \"String\" type is not implemented by default."); }

    // null terminated, fixed size strings
    private String ReadPaddedString(int padding)
    {
        int count = 0;
        byte[] buffer = m_reader.ReadBytes(padding);
        for (count = 0; count < buffer.Length; ++count)
        {
            if (buffer[count] == 0)
            {
                break;
            }
        }
        if (count == padding)
        {
            throw new NotImplementedException("[ReadPaddedString] The padded string is too long.");
        }
        return Encoding.ASCII.GetString(buffer, 0, count);
    }
    public override VisitStream visit(String name, ref PaddedString_16 value) { value = new PaddedString_16(ReadPaddedString(16)); return this; }
    public override VisitStream visit(String name, ref PaddedString_32 value) { value = new PaddedString_32(ReadPaddedString(32)); return this; }
    public override VisitStream visit(String name, ref PaddedString_64 value) { value = new PaddedString_64(ReadPaddedString(64)); return this; }
    public override VisitStream visit(String name, ref PaddedString_256 value) { value = new PaddedString_256(ReadPaddedString(256)); return this; }
}
