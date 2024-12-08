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
        public static Bitmap ApplyVignette(Bitmap image, float intensity)
        {
            int width = image.Width;
            int height = image.Height;
            float centerX = width / 2f;
            float centerY = height / 2f;
            float maxDistance = (float)Math.Sqrt(centerX * centerX + centerY * centerY);

            Bitmap output = new Bitmap(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    float dx = x - centerX;
                    float dy = y - centerY;
                    float normalizedDistance = (float)Math.Sqrt(dx * dx + dy * dy) / maxDistance;

                    // Współczynnik winiety z funkcją Gaussa
                    float vignetteFactor = (float)Math.Exp(-normalizedDistance * normalizedDistance * intensity);

                    Color originalColor = image.GetPixel(x, y);
                    int r = (int)(originalColor.R * vignetteFactor);
                    int g = (int)(originalColor.G * vignetteFactor);
                    int b = (int)(originalColor.B * vignetteFactor);

                    // Zachowanie kanału alfa
                    int a = originalColor.A;

                    output.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }

            return output;
        }

    }
}
