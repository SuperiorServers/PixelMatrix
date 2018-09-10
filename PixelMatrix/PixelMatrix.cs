using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using ImageMagick;

namespace PixelMatrix
{
    public class PixelMatrix
    {
        public FileInfo SourceFile { get; set; }

        public int Width { get; internal set; }
        public int Height { get; internal set; }
        public string Rows { get; internal set; }

        public byte[] IcoData { get; internal set; }

        public void Open()
        {
            Console.WriteLine(SourceFile.FullName);

            using (MagickImage image = new MagickImage(SourceFile.FullName))
            {
                Width = image.Width;
                Height = image.Height;

                IPixelCollection pixels = image.GetPixels();

                for (int x = 0; x < image.Height; x++)
                {
                    string row = $"\"row{x}\" \"";

                    for (int y = 0; y < image.Width; y++)
                    {
                        Color color = pixels.GetPixel(x, y).ToColor().ToColor();
   
                        decimal r = color.R;
                        decimal g = color.G;
                        decimal b = color.B;

                        decimal value = ((r / 255) + (g / 255) + (b / 255)) / 3;

                        row += $"{(y == 0 ? "" : " ")}{Math.Round(value, 3)}";
                    }

                    Rows += $"{row}\"\n";
                }

                IcoData = image.ToByteArray(MagickFormat.Ico); 
            }
        }

        public void Save()
        {
            Console.WriteLine(SourceFile.Extension);

            string ico = SourceFile.FullName.Replace(SourceFile.Extension, ".ico"); // try not to have .extension in your file names i guess?
            string txt = SourceFile.FullName.Replace(SourceFile.Extension, ".txt");

            File.WriteAllBytes(ico, IcoData);

            string output = File.ReadAllText($"{AppDomain.CurrentDomain.BaseDirectory}template.txt");

            File.WriteAllText(txt, output.Replace("_WIDTH_", Width.ToString())
                .Replace("_HEIGHT_", Height.ToString())
                .Replace("_ICON_NAME_", ico)
                .Replace("_ROWS_", Rows)
            );

        }
    }
}
