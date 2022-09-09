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
            int dominant = 0;
            int max = -1;
            foreach (KeyValuePair<int, int> pair in ColorsUsed(image))
            {
                if (pair.Value > max)
                {
                    max = pair.Value;
                    dominant = pair.Key;
                }
            }
            return Color.FromArgb(dominant);
        }

        public static Dictionary<int, int> ColorsUsed(this Bitmap bmp)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();

            BitmapData data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* ptr = (byte*)data.Scan0;
                int height = bmp.Height;
                int width = bmp.Width;
                for (int y = 0; y < height; y++)
                {
                    int* ptr2 = (int*)ptr;
                    for (int x = 0; x < width; x++)
                    {
                        int color = *(ptr2++);
                        if (result.ContainsKey(color))
                        {
                            result[color]++;
                        }
                        else
                        {
                            result[color] = 1;
                        }
                    }
                    ptr += data.Stride;
                }
            }

            bmp.UnlockBits(data);
            return result;
        }
    }
}
