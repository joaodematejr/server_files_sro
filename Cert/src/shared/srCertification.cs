using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;

public class srCertification
{
    public class srServiceType
    {
        public byte id;
        public PaddedString_64 name;
    }
    public class srOperationType
    {
        public byte id;
        public PaddedString_64 name;
    }
    public class srGlobalService
    {
        public byte operation_type;
        public PaddedString_32 name;
        public PaddedString_256 query;
        public ushort global_manager_node_id;
    }
    public class srGlobalOperation
    {
        public byte id;
        public byte operation_type;
        public PaddedString_32 name;
        public PaddedString_256 query;
    }
    public class srUnknown
    {
        public byte global_operation_id;
        public byte operation_type;
        public byte u3;
        public byte u4;
        public byte u5;
        public byte u6;
    }
    public class srShard
    {
        public ushort id;
        public byte global_operation_id;
        public byte operation_type;
        public PaddedString_32 name;
        public PaddedString_256 query;
        public PaddedString_256 query_log;
        public ushort capacity;
        public ushort shard_manager_node_id;
        public byte u1;
        public byte u2;
        public byte u3;
        public byte u4;
        public byte u5;
        public byte u6;
        public byte u7;
    }
    public class srNodeType
    {
        public int id;
        public byte operation_type;
        public PaddedString_32 name;
        public PaddedString_16 wip;
        public PaddedString_16 nip;
        public ushort machine_manager_node_id;
    }
    public class srNodeData
    {
        public ushort node_id;
        public byte operation_type;
        public byte global_operation_id;
        public ushort associated_shard_id;
        public int node_type;
        public ushort service_type;
        public ushort certification_node_id;
        public ushort port;
        public int node_icon;
        public byte u1;
        public byte u2;
        public byte u3;
        public byte u4;
        public byte u5;
        public byte u6;
        public byte u7;
        public byte u8;
        public byte u9;
        public byte u10;
        public byte u11;
        public byte u12;
        public byte u13;
        public byte u14;
        public byte u15;
        public byte u16;
        public byte u17;
        public byte u18;
        public byte u19;
        public byte u20;

    }
    public class srNodeLink
    {
        public int id;
        public ushort child_node_id;
        public ushort parent_node_id;
        public int p_label;
        public byte u1;
        public byte u2;
        public byte u3;
        public byte u4;
        public byte u5;
    }

    public List<srServiceType> srServiceTypes = new List<srServiceType>();
    public List<srOperationType> srOperationTypes = new List<srOperationType>();
    public List<srGlobalService> srGlobalServices = new List<srGlobalService>();
    public List<srGlobalOperation> srGlobalOperations = new List<srGlobalOperation>();
    public List<srUnknown> srUnknowns = new List<srUnknown>();
    public List<srShard> srShards = new List<srShard>();
    public List<srNodeType> srNodeTypes = new List<srNodeType>();
    public List<srNodeData> srNodeDatas = new List<srNodeData>();
    public List<srNodeLink> srNodeLinks = new List<srNodeLink>();

    public void Clear()
    {
        srServiceTypes.Clear();
        srOperationTypes.Clear();
        srGlobalServices.Clear();
        srGlobalOperations.Clear();
        srUnknowns.Clear();
        srShards.Clear();
        srNodeTypes.Clear();
        srNodeDatas.Clear();
        srNodeLinks.Clear();
    }
}

public class srCertification_Visistor
{
    public static VisitStream visit(srCertification.srServiceType value, VisitStream stream)
    {
        stream.visit("id", ref value.id);
        stream.visit("name", ref value.name);
        return stream;
    }
    public static VisitStream visit(srCertification.srOperationType value, VisitStream stream)
    {
        stream.visit("id", ref value.id);
        stream.visit("name", ref value.name);
        return stream;
    }
    public static VisitStream visit(srCertification.srGlobalService value, VisitStream stream)
    {
        stream.visit("operation_type", ref value.operation_type);
        stream.visit("name", ref value.name);
        stream.visit("query", ref value.query);
        stream.visit("global_manager_node_id", ref value.global_manager_node_id);
        return stream;
    }
    public static VisitStream visit(srCertification.srGlobalOperation value, VisitStream stream)
    {
        stream.visit("id", ref value.id);
        stream.visit("operation_type", ref value.operation_type);
        stream.visit("name", ref value.name);
        stream.visit("query", ref value.query);
        return stream;
    }
    public static VisitStream visit(srCertification.srUnknown value, VisitStream stream)
    {
        stream.visit("global_operation_id", ref value.global_operation_id);
        stream.visit("operation_type", ref value.operation_type);
        stream.visit("u3", ref value.u3);
        stream.visit("u4", ref value.u4);
        stream.visit("u5", ref value.u5);
        stream.visit("u6", ref value.u6);
        return stream;
    }
    public static VisitStream visit(srCertification.srShard value, VisitStream stream)
    {
        stream.visit("id", ref value.id);
        stream.visit("global_operation_id", ref value.global_operation_id);
        stream.visit("operation_type", ref value.operation_type);
        stream.visit("name", ref value.name);
        stream.visit("query", ref value.query);
        stream.visit("query_log", ref value.query_log);
        stream.visit("capacity", ref value.capacity);
        stream.visit("shard_manager_node_id", ref value.shard_manager_node_id);
        stream.visit("u1", ref value.u1);
        stream.visit("u2", ref value.u2);
        stream.visit("u3", ref value.u3);
        stream.visit("u4", ref value.u4);
        stream.visit("u5", ref value.u5);
        stream.visit("u6", ref value.u6);
        stream.visit("u7", ref value.u7);
        return stream;
    }
    public static VisitStream visit(srCertification.srNodeType value, VisitStream stream)
    {
        stream.visit("id", ref value.id);
        stream.visit("operation_type", ref value.operation_type);
        stream.visit("name", ref value.name);
        stream.visit("wip", ref value.wip);
        stream.visit("nip", ref value.nip);
        stream.visit("machine_manager_node_id", ref value.machine_manager_node_id);
        return stream;
    }
    public static VisitStream visit(srCertification.srNodeData value, VisitStream stream)
    {
        stream.visit("node_id", ref value.node_id);
        stream.visit("operation_type", ref value.operation_type);
        stream.visit("global_operation_id", ref value.global_operation_id);
        stream.visit("associated_shard_id", ref value.associated_shard_id);
        stream.visit("node_type", ref value.node_type);
        stream.visit("service_type", ref value.service_type);
        stream.visit("certification_node_id", ref value.certification_node_id);
        stream.visit("port", ref value.port);
        stream.visit("node_icon", ref value.node_icon);
        stream.visit("u1", ref value.u1);
        stream.visit("u2", ref value.u2);
        stream.visit("u3", ref value.u3);
        stream.visit("u4", ref value.u4);
        stream.visit("u5", ref value.u5);
        stream.visit("u6", ref value.u6);
        stream.visit("u7", ref value.u7);
        stream.visit("u8", ref value.u8);
        stream.visit("u9", ref value.u9);
        stream.visit("u10", ref value.u10);
        stream.visit("u11", ref value.u11);
        stream.visit("u12", ref value.u12);
        stream.visit("u13", ref value.u13);
        stream.visit("u14", ref value.u14);
        stream.visit("u15", ref value.u15);
        stream.visit("u16", ref value.u16);
        stream.visit("u17", ref value.u17);
        stream.visit("u18", ref value.u18);
        stream.visit("u19", ref value.u19);
        stream.visit("u20", ref value.u20);
        return stream;
    }
    public static VisitStream visit(srCertification.srNodeLink value, VisitStream stream)
    {
        stream.visit("id", ref value.id);
        stream.visit("child_node_id", ref value.child_node_id);
        stream.visit("parent_node_id", ref value.parent_node_id);
        stream.visit("p_label", ref value.p_label);
        stream.visit("u1", ref value.u1);
        stream.visit("u2", ref value.u2);
        stream.visit("u3", ref value.u3);
        stream.visit("u4", ref value.u4);
        stream.visit("u5", ref value.u5);
        return stream;
    }
}

public class srCertification_Processor
{
    private static void write_binary(List<srCertification.srServiceType> input, BinaryWriterVisitStream stream)
    {
        foreach (var obj in input)
        {
            byte tmp0 = 1;
            stream.visit("<ignore>", ref tmp0);
            srCertification_Visistor.visit(obj, stream);
        }
        short tmp1 = 2;
        stream.visit("<ignore>", ref tmp1);
    }
    private static void write_binary(List<srCertification.srOperationType> input, BinaryWriterVisitStream stream)
    {
        foreach (var obj in input)
        {
            byte tmp0 = 1;
            stream.visit("<ignore>", ref tmp0);
            srCertification_Visistor.visit(obj, stream);
        }
        short tmp1 = 2;
        stream.visit("<ignore>", ref tmp1);
    }
    private static void write_binary(List<srCertification.srGlobalService> input, BinaryWriterVisitStream stream)
    {
        foreach (var obj in input)
        {
            byte tmp0 = 1;
            stream.visit("<ignore>", ref tmp0);
            srCertification_Visistor.visit(obj, stream);
        }
        short tmp1 = 2;
        stream.visit("<ignore>", ref tmp1);
    }
    private static void write_binary(List<srCertification.srGlobalOperation> input, BinaryWriterVisitStream stream)
    {
        foreach (var obj in input)
        {
            byte tmp0 = 1;
            stream.visit("<ignore>", ref tmp0);
            srCertification_Visistor.visit(obj, stream);
        }
        short tmp1 = 2;
        stream.visit("<ignore>", ref tmp1);
    }
    private static void write_binary(List<srCertification.srUnknown> input, BinaryWriterVisitStream stream)
    {
        foreach (var obj in input)
        {
            byte tmp0 = 1;
            stream.visit("<ignore>", ref tmp0);
            srCertification_Visistor.visit(obj, stream);
        }
        short tmp1 = 2;
        stream.visit("<ignore>", ref tmp1);
    }
    private static void write_binary(List<srCertification.srShard> input, BinaryWriterVisitStream stream)
    {
        foreach (var obj in input)
        {
            byte tmp0 = 1;
            stream.visit("<ignore>", ref tmp0);
            srCertification_Visistor.visit(obj, stream);
        }
        short tmp1 = 2;
        stream.visit("<ignore>", ref tmp1);
    }
    private static void write_binary(List<srCertification.srNodeType> input, BinaryWriterVisitStream stream)
    {
        foreach (var obj in input)
        {
            byte tmp0 = 1;
            stream.visit("<ignore>", ref tmp0);
            srCertification_Visistor.visit(obj, stream);
        }
        short tmp1 = 2;
        stream.visit("<ignore>", ref tmp1);
    }
    private static void write_binary(List<srCertification.srNodeData> input, BinaryWriterVisitStream stream)
    {
        foreach (var obj in input)
        {
            byte tmp0 = 1;
            stream.visit("<ignore>", ref tmp0);
            srCertification_Visistor.visit(obj, stream);
        }
        short tmp1 = 2;
        stream.visit("<ignore>", ref tmp1);
    }
    private static void write_binary(List<srCertification.srNodeLink> input, BinaryWriterVisitStream stream)
    {
        foreach (var obj in input)
        {
            byte tmp0 = 1;
            stream.visit("<ignore>", ref tmp0);
            srCertification_Visistor.visit(obj, stream);
        }
        short tmp1 = 2;
        stream.visit("<ignore>", ref tmp1);
    }
    public static void write_binary(srCertification certification, BinaryWriterVisitStream stream)
    {
        short header = 1;
        stream.visit("<ignore>", ref header);

        srCertification_Processor.write_binary(certification.srServiceTypes, stream);
        srCertification_Processor.write_binary(certification.srOperationTypes, stream);
        srCertification_Processor.write_binary(certification.srGlobalServices, stream);
        srCertification_Processor.write_binary(certification.srGlobalOperations, stream);
        srCertification_Processor.write_binary(certification.srUnknowns, stream);
        srCertification_Processor.write_binary(certification.srShards, stream);
        srCertification_Processor.write_binary(certification.srNodeTypes, stream);
        srCertification_Processor.write_binary(certification.srNodeDatas, stream);
        srCertification_Processor.write_binary(certification.srNodeLinks, stream);
    }

    private static void read_binary(List<srCertification.srServiceType> output, BinaryReaderVisitStream stream)
    {
        while (true)
        {
            byte tmp0 = 0;
            stream.visit("<ignore>", ref tmp0);
            if (tmp0 == 2)
            {
                stream.visit("<ignore>", ref tmp0);
                if (tmp0 != 0)
                {
                    throw new NotImplementedException("[read_binary] Unexpected delimiter.");
                }
                break;
            }
            var obj = new srCertification.srServiceType();
            srCertification_Visistor.visit(obj, stream);
            if (tmp0 == 1)
            {
                output.Add(obj);
            }
        }
    }
    private static void read_binary(List<srCertification.srOperationType> output, BinaryReaderVisitStream stream)
    {
        while (true)
        {
            byte tmp0 = 1;
            stream.visit("<ignore>", ref tmp0);
            if (tmp0 == 2)
            {
                stream.visit("<ignore>", ref tmp0);
                if (tmp0 != 0)
                {
                    throw new NotImplementedException("[read_binary] Unexpected delimiter.");
                }
                break;
            }
            var obj = new srCertification.srOperationType();
            srCertification_Visistor.visit(obj, stream);
            if (tmp0 == 1)
            {
                output.Add(obj);
            }
        }
    }
    private static void read_binary(List<srCertification.srGlobalService> output, BinaryReaderVisitStream stream)
    {
        while (true)
        {
            byte tmp0 = 0;
            stream.visit("<ignore>", ref tmp0);
            if (tmp0 == 2)
            {
                stream.visit("<ignore>", ref tmp0);
                if (tmp0 != 0)
                {
                    throw new NotImplementedException("[read_binary] Unexpected delimiter.");
                }
                break;
            }
            var obj = new srCertification.srGlobalService();
            srCertification_Visistor.visit(obj, stream);
            if (tmp0 == 1)
            {
                output.Add(obj);
            }
        }
    }
    private static void read_binary(List<srCertification.srGlobalOperation> output, BinaryReaderVisitStream stream)
    {
        while (true)
        {
            byte tmp0 = 0;
            stream.visit("<ignore>", ref tmp0);
            if (tmp0 == 2)
            {
                stream.visit("<ignore>", ref tmp0);
                if (tmp0 != 0)
                {
                    throw new NotImplementedException("[read_binary] Unexpected delimiter.");
                }
                break;
            }
            var obj = new srCertification.srGlobalOperation();
            srCertification_Visistor.visit(obj, stream);
            if (tmp0 == 1)
            {
                output.Add(obj);
            }
        }
    }
    private static void read_binary(List<srCertification.srUnknown> output, BinaryReaderVisitStream stream)
    {
        while (true)
        {
            byte tmp0 = 0;
            stream.visit("<ignore>", ref tmp0);
            if (tmp0 == 2)
            {
                stream.visit("<ignore>", ref tmp0);
                if (tmp0 != 0)
                {
                    throw new NotImplementedException("[read_binary] Unexpected delimiter.");
                }
                break;
            }
            var obj = new srCertification.srUnknown();
            srCertification_Visistor.visit(obj, stream);
            if (tmp0 == 1)
            {
                output.Add(obj);
            }
        }
    }
    private static void read_binary(List<srCertification.srShard> output, BinaryReaderVisitStream stream)
    {
        while (true)
        {
            byte tmp0 = 0;
            stream.visit("<ignore>", ref tmp0);
            if (tmp0 == 2)
            {
                stream.visit("<ignore>", ref tmp0);
                if (tmp0 != 0)
                {
                    throw new NotImplementedException("[read_binary] Unexpected delimiter.");
                }
                break;
            }
            var obj = new srCertification.srShard();
            srCertification_Visistor.visit(obj, stream);
            if (tmp0 == 1)
            {
                output.Add(obj);
            }
        }
    }
    private static void read_binary(List<srCertification.srNodeType> output, BinaryReaderVisitStream stream)
    {
        while (true)
        {
            byte tmp0 = 0;
            stream.visit("<ignore>", ref tmp0);
            if (tmp0 == 2)
            {
                stream.visit("<ignore>", ref tmp0);
                if (tmp0 != 0)
                {
                    throw new NotImplementedException("[read_binary] Unexpected delimiter.");
                }
                break;
            }
            var obj = new srCertification.srNodeType();
            srCertification_Visistor.visit(obj, stream);
            if (tmp0 == 1)
            {
                output.Add(obj);
            }
        }
    }
    private static void read_binary(List<srCertification.srNodeData> output, BinaryReaderVisitStream stream)
    {
        while (true)
        {
            byte tmp0 = 0;
            stream.visit("<ignore>", ref tmp0);
            if (tmp0 == 2)
            {
                stream.visit("<ignore>", ref tmp0);
                if (tmp0 != 0)
                {
                    throw new NotImplementedException("[read_binary] Unexpected delimiter.");
                }
                break;
            }
            var obj = new srCertification.srNodeData();
            srCertification_Visistor.visit(obj, stream);
            if (tmp0 == 1)
            {
                output.Add(obj);
            }
        }
    }
    private static void read_binary(List<srCertification.srNodeLink> output, BinaryReaderVisitStream stream)
    {
        while (true)
        {
            byte tmp0 = 0;
            stream.visit("<ignore>", ref tmp0);
            if (tmp0 == 2)
            {
                stream.visit("<ignore>", ref tmp0);
                if (tmp0 != 0)
                {
                    throw new NotImplementedException("[read_binary] Unexpected delimiter.");
                }
                break;
            }
            var obj = new srCertification.srNodeLink();
            srCertification_Visistor.visit(obj, stream);
            if (tmp0 == 1)
            {
                output.Add(obj);
            }
        }
    }
    public static void read_binary(srCertification certification, BinaryReaderVisitStream stream)
    {
        short header = 0;
        stream.visit("<ignore>", ref header);
        if (header != 1)
        {
            throw new NotImplementedException("Unexpected start of file.");
        }

        srCertification_Processor.read_binary(certification.srServiceTypes, stream);
        srCertification_Processor.read_binary(certification.srOperationTypes, stream);
        srCertification_Processor.read_binary(certification.srGlobalServices, stream);
        srCertification_Processor.read_binary(certification.srGlobalOperations, stream);
        srCertification_Processor.read_binary(certification.srUnknowns, stream);
        srCertification_Processor.read_binary(certification.srShards, stream);
        srCertification_Processor.read_binary(certification.srNodeTypes, stream);
        srCertification_Processor.read_binary(certification.srNodeDatas, stream);
        srCertification_Processor.read_binary(certification.srNodeLinks, stream);
    }

    private static String write_xml_path = "";
    private static void write_xml(List<srCertification.srServiceType> input, XMLWriterVisitStream stream)
    {
        String basename = "srServiceType";
        XElement root_element = new XElement(basename + "s");
        foreach (var obj in input)
        {
            stream.current_element = new XElement(basename);
            srCertification_Visistor.visit(obj, stream);
            root_element.Add(stream.current_element);
            stream.current_element = null;
        }
        root_element.Save(write_xml_path + "\\" + basename + "s.xml");
    }
    private static void write_xml(List<srCertification.srOperationType> input, XMLWriterVisitStream stream)
    {
        String basename = "srOperationType";
        XElement root_element = new XElement(basename + "s");
        foreach (var obj in input)
        {
            stream.current_element = new XElement(basename);
            srCertification_Visistor.visit(obj, stream);
            root_element.Add(stream.current_element);
            stream.current_element = null;
        }
        root_element.Save(write_xml_path + "\\" + basename + "s.xml");
    }
    private static void write_xml(List<srCertification.srGlobalService> input, XMLWriterVisitStream stream)
    {
        String basename = "srGlobalService";
        XElement root_element = new XElement(basename + "s");
        foreach (var obj in input)
        {
            stream.current_element = new XElement(basename);
            srCertification_Visistor.visit(obj, stream);
            root_element.Add(stream.current_element);
            stream.current_element = null;
        }
        root_element.Save(write_xml_path + "\\" + basename + "s.xml");
    }
    private static void write_xml(List<srCertification.srGlobalOperation> input, XMLWriterVisitStream stream)
    {
        String basename = "srGlobalOperation";
        XElement root_element = new XElement(basename + "s");
        foreach (var obj in input)
        {
            stream.current_element = new XElement(basename);
            srCertification_Visistor.visit(obj, stream);
            root_element.Add(stream.current_element);
            stream.current_element = null;
        }
        root_element.Save(write_xml_path + "\\" + basename + "s.xml");
    }
    private static void write_xml(List<srCertification.srUnknown> input, XMLWriterVisitStream stream)
    {
        String basename = "srUnknown";
        XElement root_element = new XElement(basename + "s");
        foreach (var obj in input)
        {
            stream.current_element = new XElement(basename);
            srCertification_Visistor.visit(obj, stream);
            root_element.Add(stream.current_element);
            stream.current_element = null;
        }
        root_element.Save(write_xml_path + "\\" + basename + "s.xml");
    }
    private static void write_xml(List<srCertification.srShard> input, XMLWriterVisitStream stream)
    {
        String basename = "srShard";
        XElement root_element = new XElement(basename + "s");
        foreach (var obj in input)
        {
            stream.current_element = new XElement(basename);
            srCertification_Visistor.visit(obj, stream);
            root_element.Add(stream.current_element);
            stream.current_element = null;
        }
        root_element.Save(write_xml_path + "\\" + basename + "s.xml");
    }
    private static void write_xml(List<srCertification.srNodeType> input, XMLWriterVisitStream stream)
    {
        String basename = "srNodeType";
        XElement root_element = new XElement(basename + "s");
        foreach (var obj in input)
        {
            stream.current_element = new XElement(basename);
            srCertification_Visistor.visit(obj, stream);
            root_element.Add(stream.current_element);
            stream.current_element = null;
        }
        root_element.Save(write_xml_path + "\\" + basename + "s.xml");
    }
    private static void write_xml(List<srCertification.srNodeData> input, XMLWriterVisitStream stream)
    {
        String basename = "srNodeData";
        XElement root_element = new XElement(basename + "s");
        foreach (var obj in input)
        {
            stream.current_element = new XElement(basename);
            srCertification_Visistor.visit(obj, stream);
            root_element.Add(stream.current_element);
            stream.current_element = null;
        }
        root_element.Save(write_xml_path + "\\" + basename + "s.xml");
    }
    private static void write_xml(List<srCertification.srNodeLink> input, XMLWriterVisitStream stream)
    {
        String basename = "srNodeLink";
        XElement root_element = new XElement(basename + "s");
        foreach (var obj in input)
        {
            stream.current_element = new XElement(basename);
            srCertification_Visistor.visit(obj, stream);
            root_element.Add(stream.current_element);
            stream.current_element = null;
        }
        root_element.Save(write_xml_path + "\\" + basename + "s.xml");
    }
    public static void write_xml(srCertification certification, XMLWriterVisitStream stream, String path)
    {
        lock (write_xml_path)
        {
            write_xml_path = path;
            srCertification_Processor.write_xml(certification.srServiceTypes, stream);
            srCertification_Processor.write_xml(certification.srOperationTypes, stream);
            srCertification_Processor.write_xml(certification.srGlobalServices, stream);
            srCertification_Processor.write_xml(certification.srGlobalOperations, stream);
            srCertification_Processor.write_xml(certification.srUnknowns, stream);
            srCertification_Processor.write_xml(certification.srShards, stream);
            srCertification_Processor.write_xml(certification.srNodeTypes, stream);
            srCertification_Processor.write_xml(certification.srNodeDatas, stream);
            srCertification_Processor.write_xml(certification.srNodeLinks, stream);
            write_xml_path = "";
        }
    }

    private static String read_xml_path = "";
    private static void read_xml(List<srCertification.srServiceType> output, XMLReaderVisitStream stream)
    {
        String basename = "srServiceType";
        XElement root_element = XElement.Load(read_xml_path + "\\" + basename + "s.xml");
        foreach (XElement element in root_element.Elements())
        {
            if (element.Name == basename)
            {
                stream.current_element = element;
                var obj = new srCertification.srServiceType();
                srCertification_Visistor.visit(obj, stream);
                output.Add(obj);
                stream.current_element = null;
            }
        }
    }
    private static void read_xml(List<srCertification.srOperationType> output, XMLReaderVisitStream stream)
    {
        String basename = "srOperationType";
        XElement root_element = XElement.Load(read_xml_path + "\\" + basename + "s.xml");
        foreach (XElement element in root_element.Elements())
        {
            if (element.Name == basename)
            {
                stream.current_element = element;
                var obj = new srCertification.srOperationType();
                srCertification_Visistor.visit(obj, stream);
                output.Add(obj);
                stream.current_element = null;
            }
        }
    }
    private static void read_xml(List<srCertification.srGlobalService> output, XMLReaderVisitStream stream)
    {
        String basename = "srGlobalService";
        XElement root_element = XElement.Load(read_xml_path + "\\" + basename + "s.xml");
        foreach (XElement element in root_element.Elements())
        {
            if (element.Name == basename)
            {
                stream.current_element = element;
                var obj = new srCertification.srGlobalService();
                srCertification_Visistor.visit(obj, stream);
                output.Add(obj);
                stream.current_element = null;
            }
        }
    }
    private static void read_xml(List<srCertification.srGlobalOperation> output, XMLReaderVisitStream stream)
    {
        String basename = "srGlobalOperation";
        XElement root_element = XElement.Load(read_xml_path + "\\" + basename + "s.xml");
        foreach (XElement element in root_element.Elements())
        {
            if (element.Name == basename)
            {
                stream.current_element = element;
                var obj = new srCertification.srGlobalOperation();
                srCertification_Visistor.visit(obj, stream);
                output.Add(obj);
                stream.current_element = null;
            }
        }
    }
    private static void read_xml(List<srCertification.srUnknown> output, XMLReaderVisitStream stream)
    {
        String basename = "srUnknown";
        XElement root_element = XElement.Load(read_xml_path + "\\" + basename + "s.xml");
        foreach (XElement element in root_element.Elements())
        {
            if (element.Name == basename)
            {
                stream.current_element = element;
                var obj = new srCertification.srUnknown();
                srCertification_Visistor.visit(obj, stream);
                output.Add(obj);
                stream.current_element = null;
            }
        }
    }
    private static void read_xml(List<srCertification.srShard> output, XMLReaderVisitStream stream)
    {
        String basename = "srShard";
        XElement root_element = XElement.Load(read_xml_path + "\\" + basename + "s.xml");
        foreach (XElement element in root_element.Elements())
        {
            if (element.Name == basename)
            {
                stream.current_element = element;
                var obj = new srCertification.srShard();
                srCertification_Visistor.visit(obj, stream);
                output.Add(obj);
                stream.current_element = null;
            }
        }
    }
    private static void read_xml(List<srCertification.srNodeType> output, XMLReaderVisitStream stream)
    {
        String basename = "srNodeType";
        XElement root_element = XElement.Load(read_xml_path + "\\" + basename + "s.xml");
        foreach (XElement element in root_element.Elements())
        {
            if (element.Name == basename)
            {
                stream.current_element = element;
                var obj = new srCertification.srNodeType();
                srCertification_Visistor.visit(obj, stream);
                output.Add(obj);
                stream.current_element = null;
            }
        }
    }
    private static void read_xml(List<srCertification.srNodeData> output, XMLReaderVisitStream stream)
    {
        String basename = "srNodeData";
        XElement root_element = XElement.Load(read_xml_path + "\\" + basename + "s.xml");
        foreach (XElement element in root_element.Elements())
        {
            if (element.Name == basename)
            {
                stream.current_element = element;
                var obj = new srCertification.srNodeData();
                srCertification_Visistor.visit(obj, stream);
                output.Add(obj);
                stream.current_element = null;
            }
        }
    }
    private static void read_xml(List<srCertification.srNodeLink> output, XMLReaderVisitStream stream)
    {
        String basename = "srNodeLink";
        XElement root_element = XElement.Load(read_xml_path + "\\" + basename + "s.xml");
        foreach (XElement element in root_element.Elements())
        {
            if (element.Name == basename)
            {
                stream.current_element = element;
                var obj = new srCertification.srNodeLink();
                srCertification_Visistor.visit(obj, stream);
                output.Add(obj);
                stream.current_element = null;
            }
        }
    }
    public static void read_xml(srCertification certification, XMLReaderVisitStream stream, String path)
    {
        lock (read_xml_path)
        {
            read_xml_path = path;
            srCertification_Processor.read_xml(certification.srServiceTypes, stream);
            srCertification_Processor.read_xml(certification.srOperationTypes, stream);
            srCertification_Processor.read_xml(certification.srGlobalServices, stream);
            srCertification_Processor.read_xml(certification.srGlobalOperations, stream);
            srCertification_Processor.read_xml(certification.srUnknowns, stream);
            srCertification_Processor.read_xml(certification.srShards, stream);
            srCertification_Processor.read_xml(certification.srNodeTypes, stream);
            srCertification_Processor.read_xml(certification.srNodeDatas, stream);
            srCertification_Processor.read_xml(certification.srNodeLinks, stream);
            read_xml_path = "";
        }
    }

    private static String write_ini_path = "";
    private static void write_ini(List<srCertification.srServiceType> input, INIWriterVisitStream stream)
    {
        String basename = "srServiceType";
        stream.ini = write_ini_path + "\\" + basename + ".ini";
        int idx = 0;
        INIWriterVisitStream.WritePrivateProfileString("global", "count", String.Format("{0}", input.Count), stream.ini);
        foreach (var obj in input)
        {
            stream.section = string.Format("entry{0}", idx++);
            srCertification_Visistor.visit(obj, stream);
        }
    }
    private static void write_ini(List<srCertification.srOperationType> input, INIWriterVisitStream stream)
    {
        String basename = "srOperationType";
        stream.ini = write_ini_path + "\\" + basename + ".ini";
        int idx = 0;
        INIWriterVisitStream.WritePrivateProfileString("global", "count", String.Format("{0}", input.Count), stream.ini);
        foreach (var obj in input)
        {
            stream.section = string.Format("entry{0}", idx++);
            srCertification_Visistor.visit(obj, stream);
        }
    }
    private static void write_ini(List<srCertification.srGlobalService> input, INIWriterVisitStream stream)
    {
        String basename = "srGlobalService";
        stream.ini = write_ini_path + "\\" + basename + ".ini";
        int idx = 0;
        INIWriterVisitStream.WritePrivateProfileString("global", "count", String.Format("{0}", input.Count), stream.ini);
        foreach (var obj in input)
        {
            stream.section = string.Format("entry{0}", idx++);
            srCertification_Visistor.visit(obj, stream);
        }
    }
    private static void write_ini(List<srCertification.srGlobalOperation> input, INIWriterVisitStream stream)
    {
        String basename = "srGlobalOperation";
        stream.ini = write_ini_path + "\\" + basename + ".ini";
        int idx = 0;
        INIWriterVisitStream.WritePrivateProfileString("global", "count", String.Format("{0}", input.Count), stream.ini);
        foreach (var obj in input)
        {
            stream.section = string.Format("entry{0}", idx++);
            srCertification_Visistor.visit(obj, stream);
        }
    }
    private static void write_ini(List<srCertification.srUnknown> input, INIWriterVisitStream stream)
    {
        String basename = "srUnknown";
        stream.ini = write_ini_path + "\\" + basename + ".ini";
        int idx = 0;
        INIWriterVisitStream.WritePrivateProfileString("global", "count", String.Format("{0}", input.Count), stream.ini);
        foreach (var obj in input)
        {
            stream.section = string.Format("entry{0}", idx++);
            srCertification_Visistor.visit(obj, stream);
        }
    }
    private static void write_ini(List<srCertification.srShard> input, INIWriterVisitStream stream)
    {
        String basename = "srShard";
        stream.ini = write_ini_path + "\\" + basename + ".ini";
        int idx = 0;
        INIWriterVisitStream.WritePrivateProfileString("global", "count", String.Format("{0}", input.Count), stream.ini);
        foreach (var obj in input)
        {
            stream.section = string.Format("entry{0}", idx++);
            srCertification_Visistor.visit(obj, stream);
        }
    }
    private static void write_ini(List<srCertification.srNodeType> input, INIWriterVisitStream stream)
    {
        String basename = "srNodeType";
        stream.ini = write_ini_path + "\\" + basename + ".ini";
        int idx = 0;
        INIWriterVisitStream.WritePrivateProfileString("global", "count", String.Format("{0}", input.Count), stream.ini);
        foreach (var obj in input)
        {
            stream.section = string.Format("entry{0}", idx++);
            srCertification_Visistor.visit(obj, stream);
        }
    }
    private static void write_ini(List<srCertification.srNodeData> input, INIWriterVisitStream stream)
    {
        String basename = "srNodeData";
        stream.ini = write_ini_path + "\\" + basename + ".ini";
        int idx = 0;
        INIWriterVisitStream.WritePrivateProfileString("global", "count", String.Format("{0}", input.Count), stream.ini);
        foreach (var obj in input)
        {
            stream.section = string.Format("entry{0}", idx++);
            srCertification_Visistor.visit(obj, stream);
        }
    }
    private static void write_ini(List<srCertification.srNodeLink> input, INIWriterVisitStream stream)
    {
        String basename = "srNodeLink";
        stream.ini = write_ini_path + "\\" + basename + ".ini";
        int idx = 0;
        INIWriterVisitStream.WritePrivateProfileString("global", "count", String.Format("{0}", input.Count), stream.ini);
        foreach (var obj in input)
        {
            stream.section = string.Format("entry{0}", idx++);
            srCertification_Visistor.visit(obj, stream);
        }
    }
    public static void write_ini(srCertification certification, INIWriterVisitStream stream, String path)
    {
        lock (write_ini_path)
        {
            write_ini_path = path;
            srCertification_Processor.write_ini(certification.srServiceTypes, stream);
            srCertification_Processor.write_ini(certification.srOperationTypes, stream);
            srCertification_Processor.write_ini(certification.srGlobalServices, stream);
            srCertification_Processor.write_ini(certification.srGlobalOperations, stream);
            srCertification_Processor.write_ini(certification.srUnknowns, stream);
            srCertification_Processor.write_ini(certification.srShards, stream);
            srCertification_Processor.write_ini(certification.srNodeTypes, stream);
            srCertification_Processor.write_ini(certification.srNodeDatas, stream);
            srCertification_Processor.write_ini(certification.srNodeLinks, stream);
            write_ini_path = "";
        }
    }

    private static String read_ini_path = "";
    private static void read_ini(List<srCertification.srServiceType> output, INIReaderVisitStream stream)
    {
        String basename = "srServiceType";
        stream.ini = read_ini_path + "\\" + basename + ".ini";
        int count = 0;
        StringBuilder sb = new StringBuilder(256); INIReaderVisitStream.GetPrivateProfileString("global", "count", "", sb, sb.Capacity, stream.ini); count = Int32.Parse(sb.ToString());
        for (int x = 0; x < count; ++x)
        {
            stream.section = string.Format("entry{0}", x);
            var obj = new srCertification.srServiceType();
            srCertification_Visistor.visit(obj, stream);
            output.Add(obj);
        }
    }
    private static void read_ini(List<srCertification.srOperationType> output, INIReaderVisitStream stream)
    {
        String basename = "srOperationType";
        stream.ini = read_ini_path + "\\" + basename + ".ini";
        int count = 0;
        StringBuilder sb = new StringBuilder(256); INIReaderVisitStream.GetPrivateProfileString("global", "count", "", sb, sb.Capacity, stream.ini); count = Int32.Parse(sb.ToString());
        for (int x = 0; x < count; ++x)
        {
            stream.section = string.Format("entry{0}", x);
            var obj = new srCertification.srOperationType();
            srCertification_Visistor.visit(obj, stream);
            output.Add(obj);
        }
    }
    private static void read_ini(List<srCertification.srGlobalService> output, INIReaderVisitStream stream)
    {
        String basename = "srGlobalService";
        stream.ini = read_ini_path + "\\" + basename + ".ini";
        int count = 0;
        StringBuilder sb = new StringBuilder(256); INIReaderVisitStream.GetPrivateProfileString("global", "count", "", sb, sb.Capacity, stream.ini); count = Int32.Parse(sb.ToString());
        for (int x = 0; x < count; ++x)
        {
            stream.section = string.Format("entry{0}", x);
            var obj = new srCertification.srGlobalService();
            srCertification_Visistor.visit(obj, stream);
            output.Add(obj);
        }
    }
    private static void read_ini(List<srCertification.srGlobalOperation> output, INIReaderVisitStream stream)
    {
        String basename = "srGlobalOperation";
        stream.ini = read_ini_path + "\\" + basename + ".ini";
        int count = 0;
        StringBuilder sb = new StringBuilder(256); INIReaderVisitStream.GetPrivateProfileString("global", "count", "", sb, sb.Capacity, stream.ini); count = Int32.Parse(sb.ToString());
        for (int x = 0; x < count; ++x)
        {
            stream.section = string.Format("entry{0}", x);
            var obj = new srCertification.srGlobalOperation();
            srCertification_Visistor.visit(obj, stream);
            output.Add(obj);
        }
    }
    private static void read_ini(List<srCertification.srUnknown> output, INIReaderVisitStream stream)
    {
        String basename = "srUnknown";
        stream.ini = read_ini_path + "\\" + basename + ".ini";
        int count = 0;
        StringBuilder sb = new StringBuilder(256); INIReaderVisitStream.GetPrivateProfileString("global", "count", "", sb, sb.Capacity, stream.ini); count = Int32.Parse(sb.ToString());
        for (int x = 0; x < count; ++x)
        {
            stream.section = string.Format("entry{0}", x);
            var obj = new srCertification.srUnknown();
            srCertification_Visistor.visit(obj, stream);
            output.Add(obj);
        }
    }
    private static void read_ini(List<srCertification.srShard> output, INIReaderVisitStream stream)
    {
        String basename = "srShard";
        stream.ini = read_ini_path + "\\" + basename + ".ini";
        int count = 0;
        StringBuilder sb = new StringBuilder(256); INIReaderVisitStream.GetPrivateProfileString("global", "count", "", sb, sb.Capacity, stream.ini); count = Int32.Parse(sb.ToString());
        for (int x = 0; x < count; ++x)
        {
            stream.section = string.Format("entry{0}", x);
            var obj = new srCertification.srShard();
            srCertification_Visistor.visit(obj, stream);
            output.Add(obj);
        }
    }
    private static void read_ini(List<srCertification.srNodeType> output, INIReaderVisitStream stream)
    {
        String basename = "srNodeType";
        stream.ini = read_ini_path + "\\" + basename + ".ini";
        int count = 0;
        StringBuilder sb = new StringBuilder(256); INIReaderVisitStream.GetPrivateProfileString("global", "count", "", sb, sb.Capacity, stream.ini); count = Int32.Parse(sb.ToString());
        for (int x = 0; x < count; ++x)
        {
            stream.section = string.Format("entry{0}", x);
            var obj = new srCertification.srNodeType();
            srCertification_Visistor.visit(obj, stream);
            output.Add(obj);
        }
    }
    private static void read_ini(List<srCertification.srNodeData> output, INIReaderVisitStream stream)
    {
        String basename = "srNodeData";
        stream.ini = read_ini_path + "\\" + basename + ".ini";
        int count = 0;
        StringBuilder sb = new StringBuilder(256); INIReaderVisitStream.GetPrivateProfileString("global", "count", "", sb, sb.Capacity, stream.ini); count = Int32.Parse(sb.ToString());
        for (int x = 0; x < count; ++x)
        {
            stream.section = string.Format("entry{0}", x);
            var obj = new srCertification.srNodeData();
            srCertification_Visistor.visit(obj, stream);
            output.Add(obj);
        }
    }
    private static void read_ini(List<srCertification.srNodeLink> output, INIReaderVisitStream stream)
    {
        String basename = "srNodeLink";
        stream.ini = read_ini_path + "\\" + basename + ".ini";
        int count = 0;
        StringBuilder sb = new StringBuilder(256); INIReaderVisitStream.GetPrivateProfileString("global", "count", "", sb, sb.Capacity, stream.ini); count = Int32.Parse(sb.ToString());
        for (int x = 0; x < count; ++x)
        {
            stream.section = string.Format("entry{0}", x);
            var obj = new srCertification.srNodeLink();
            srCertification_Visistor.visit(obj, stream);
            output.Add(obj);
        }
    }
    public static void read_ini(srCertification certification, INIReaderVisitStream stream, String path)
    {
        lock (read_ini_path)
        {
            read_ini_path = path;
            srCertification_Processor.read_ini(certification.srServiceTypes, stream);
            srCertification_Processor.read_ini(certification.srOperationTypes, stream);
            srCertification_Processor.read_ini(certification.srGlobalServices, stream);
            srCertification_Processor.read_ini(certification.srGlobalOperations, stream);
            srCertification_Processor.read_ini(certification.srUnknowns, stream);
            srCertification_Processor.read_ini(certification.srShards, stream);
            srCertification_Processor.read_ini(certification.srNodeTypes, stream);
            srCertification_Processor.read_ini(certification.srNodeDatas, stream);
            srCertification_Processor.read_ini(certification.srNodeLinks, stream);
            read_ini_path = "";
        }
    }
}
