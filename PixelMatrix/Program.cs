using System;
using System.IO;

namespace PixelMatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Drag a file onto PixelMatrix.exe or run PixelMatrix.exe <file>");
                Console.ReadLine();
                return;
            }

            if (!File.Exists($"{AppDomain.CurrentDomain.BaseDirectory}template.txt"))
            {
                Console.WriteLine("Missing template.txt!");
                Console.ReadLine();
                return;
            }

            string file = args[0];

            if (!File.Exists(file))
            {
                Console.WriteLine("File does not exist!");
                Console.ReadLine();
                return;
            }

            if (!file.EndsWith(".png"))
            {
                Console.WriteLine("Warning supplied file is not a PNG file! Stuff may break!\n\n Press any key to continue..");
                Console.ReadLine();
            }

            PixelMatrix matrix = new PixelMatrix
            {
                SourceFile = new FileInfo(file)
            };

            matrix.Open();
            matrix.Save();
        }
    }
}
