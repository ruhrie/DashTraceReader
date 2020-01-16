//using Ionic.Zlib;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace znet
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "";
            string outputFile = "";
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-i":
                        {
                            inputFile = args[++i];
                            break;
                        }
                    case "-o":
                        {
                            outputFile = args[++i];
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Not recognized parameter");
                            return;
                        }
                }
            }
            if (inputFile == "")
            {
                Console.WriteLine("Input file name is required");
                return;
            }
            bool write = outputFile != "";
            Stream writeStream = null;
            if (write)
            {
                writeStream = File.OpenWrite(outputFile);
            }

            //ICSharpCode.SharpZipLib.Zip.Compression.Streams.
            InflaterInputBuffer buff = new InflaterInputBuffer(File.OpenRead(inputFile));
            buff.Fill();
            while (buff.Available != 0)
            {
                int length = buff.RawLength;
                writeStream.Write(buff.RawData, 0, length);
                buff.Fill();
            }

            //var inputStream = InflaterInputStream


            
        }
    }
}
