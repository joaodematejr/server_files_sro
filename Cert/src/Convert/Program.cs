using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Convert
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                srCertification certification = new srCertification();

                if (args.Length != 4)
                {
                    Console.WriteLine("Usage: convert <dat|xml|ini> <input> <dat|xml|ini> <output>");
                    Console.ReadLine();
                    return;
                }

                args[0] = args[0].ToLower();
                args[2] = args[2].ToLower();
                if (args[1].IndexOf(':') == -1 && args[1].IndexOf("\\\\") == -1)
                {
                    args[1] = Directory.GetCurrentDirectory() + "\\" + args[1];
                }
                if (args[3].IndexOf(':') == -1 && args[3].IndexOf("\\\\") == -1)
                {
                    args[3] = Directory.GetCurrentDirectory() + "\\" + args[3];
                }

                Console.WriteLine("Status: Input Format => {0}", args[0]);
                Console.WriteLine("Status: Input => {0}", args[1]);
                Console.WriteLine("Status: Output Format => {0}", args[2]);
                Console.WriteLine("Status: Output => {0}", args[3]);

                if (args[0] == "dat")
                {
                    using (BinaryReader br = new BinaryReader(new FileStream(args[1], FileMode.Open)))
                    {
                        BinaryReaderVisitStream brvs = new BinaryReaderVisitStream(br);
                        srCertification_Processor.read_binary(certification, brvs);
                    }
                }
                else if (args[0] == "xml")
                {
                    XMLReaderVisitStream xrvs = new XMLReaderVisitStream();
                    srCertification_Processor.read_xml(certification, xrvs, args[1]);
                }
                else if (args[0] == "ini")
                {
                    INIReaderVisitStream irvs = new INIReaderVisitStream();
                    srCertification_Processor.read_ini(certification, irvs, args[1]);
                }
                else
                {
                    throw new NotImplementedException(String.Format("Unknown input format [{0}].", args[0]));
                }

                if (args[2] == "dat")
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(args[3], FileMode.Create)))
                    {
                        BinaryWriterVisitStream bwvs = new BinaryWriterVisitStream(bw);
                        Directory.CreateDirectory(Path.GetDirectoryName(args[3]));
                        srCertification_Processor.write_binary(certification, bwvs);
                    }
                }
                else if (args[2] == "xml")
                {
                    XMLWriterVisitStream xwvs = new XMLWriterVisitStream();
                    Directory.CreateDirectory(Path.GetDirectoryName(args[3]));
                    srCertification_Processor.write_xml(certification, xwvs, args[3]);
                }
                else if (args[2] == "ini")
                {
                    INIWriterVisitStream iwvs = new INIWriterVisitStream();
                    Directory.CreateDirectory(args[3] + "\\");
                    srCertification_Processor.write_ini(certification, iwvs, args[3]);
                }
                else
                {
                    throw new NotImplementedException(String.Format("Unknown output format [{0}].", args[2]));
                }

                Console.WriteLine("Status: The process has completed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            Console.WriteLine("Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
