using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

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
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color color = image.GetPixel(x, y);
                    if (result.ContainsKey(color))
                    {
                        result[color]++;
                    }
                    else
                    {
                        result[color] = 1;
                    }
                }
            }
            return result;
        }
    }
}
