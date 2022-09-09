using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageProcessing
{
    public static class BitmapExtensions
    {
        public static Bitmap Clone(this Bitmap img, PixelFormat pixelFormat)
        {
            return img.Clone(new Rectangle(0, 0, img.Width, img.Height), pixelFormat);
        }

        public static Color DominantColor(this Bitmap image)
        {
            Color dominant = Color.Transparent;
            int max = -1;
            foreach (KeyValuePair<Color, int> pair in ColorsUsed(image))
            {
                if (pair.Value > max)
                {
                    max = pair.Value;
                    dominant = pair.Key;
                }
            }
            return dominant;
        }

        public static Dictionary<Color, int> ColorsUsed(this Bitmap image)
        {
            Dictionary<Color, int> result = new Dictionary<Color, int>();

            BitmapData bmpData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap. 
            // int numBytes = bmp.Width * bmp.Height * 3; 
            int numBytes = bmpData.Stride * image.Height;
            byte[] rgbValues = new byte[numBytes];

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbValues, 0, numBytes);

            for (int i = 0; i < rgbValues.Length; i += 4)
            {
                byte b = rgbValues[i];
                byte g = rgbValues[i + 1];
                byte r = rgbValues[i + 2];
                byte a = rgbValues[i + 3];
                Color color = Color.FromArgb(a, r, g, b);
                if (result.ContainsKey(color))
                {
                    result[color]++;
                }
                else
                {
                    result[color] = 1;
                }
            }

            // Copy the RGB values back to the bitmap
            Marshal.Copy(rgbValues, 0, ptr, numBytes);
            
            image.UnlockBits(bmpData);
            return result;
        }
    }
}
