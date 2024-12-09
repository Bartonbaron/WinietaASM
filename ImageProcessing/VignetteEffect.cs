using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageProcessing
{
    public class VignetteEffect
    {
        public Bitmap ApplyVignette(Bitmap image, float intensity)
        {
            Bitmap result = new Bitmap(image.Width, image.Height);

            int centerX = image.Width / 2;
            int centerY = image.Height / 2;
            double maxDistance = Math.Sqrt(centerX * centerX + centerY * centerY);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixelColor = image.GetPixel(x, y);

                    // Oblicz odległość od środka
                    double dx = x - centerX;
                    double dy = y - centerY;
                    double distance = Math.Sqrt(dx * dx + dy * dy) / maxDistance;

                    // Wyznacz współczynnik winiety na podstawie intensywności
                    double vignetteFactor = 1 - (distance * intensity);
                    vignetteFactor = Math.Max(0, Math.Min(1, vignetteFactor));

                    // Modyfikacja koloru piksela
                    int r = (int)(pixelColor.R * vignetteFactor);
                    int g = (int)(pixelColor.G * vignetteFactor);
                    int b = (int)(pixelColor.B * vignetteFactor);

                    result.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return result;
        }


    }
}
