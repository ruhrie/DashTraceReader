//using Ionic.Zlib;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace znet
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputFile = "";
            string outputFile = "";
            for(int i = 0; i < args.Length; i++)
            {
                switch(args[i])
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
            if(inputFile == "")
            {
                Console.WriteLine("Input file name is required");
                return;
            }
            bool write = outputFile != "";
            Stream writeStream = null;
            if(write)
            {
                writeStream = File.OpenWrite(outputFile);
            }

            using (Stream input = File.OpenRead(inputFile))
            {
                if (input.ReadByte() != 0x78 || input.ReadByte() != 0xDA)
                {
                    throw new Exception("Incorrect header");
                }                    
                using (var deflateStream = new DeflateStream(input, CompressionMode.Decompress))
                {
                    int read = deflateStream.ReadByte();
                    while (read != -1)
                    {
                        byte dataByte = (byte)read;
                        if(write)
                        {
                            writeStream.WriteByte(dataByte);
                            writeStream.Flush();
                        }
                        else
                        {
                            Console.Write(Encoding.ASCII.GetString(new byte[] { dataByte }));
                        }
                        read = deflateStream.ReadByte();
                    }
                }
            }
        }
    }
}
