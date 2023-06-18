using System;
using System.Net;
using ZXing;
using ZXing.QrCode;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using SixLabors.ImageSharp;
using System.Globalization;

namespace QRCodeReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string imageUrl = "";
            if (args.Count() != 1)
            {
                Console.WriteLine("Called incorrectly. Call like qrcodereader <filename>");
                return;
            }
            else
            {
                imageUrl = args[0];
            }


            var image = SixLabors.ImageSharp.Image.Load<Rgba32>(imageUrl);
            var reader = new BarcodeReader<Image<Rgba32>>((bitmap) => new ImageFileLuminanceSource(image));
            var result = reader.Decode(image);
            Console.WriteLine(result.Text);

        }
    }

    class ImageFileLuminanceSource : LuminanceSource
    {
        private readonly byte[] luminances;

        public ImageFileLuminanceSource(Image<Rgba32> image) : base(image.Width, image.Height)
        {
            int width = image.Width;
            int height = image.Height;

            // Convert the image to grayscale and extract the luminance data
            luminances = new byte[width * height];
            int index = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var pixel = image[x, y];
                    byte luminance = (byte)((pixel.R + pixel.G + pixel.B) / 3);
                    luminances[index++] = luminance;
                }
            }
        }

        public override byte[] getRow(int y, byte[] row)
        {
            int width = Width;
            if (row == null || row.Length < width)
            {
                row = new byte[width];
            }

            int offset = y * width;
            Array.Copy(luminances, offset, row, 0, width);
            return row;
        }

        public override byte[] Matrix
        {
            get { return luminances; }
        }
    }
}