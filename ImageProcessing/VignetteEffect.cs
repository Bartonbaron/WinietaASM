using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageProcessing
{
    public class VignetteEffect
    {
        public void ApplyVignette(byte[] imageData, int width, int height, int stride, float intensity, int threadCount)
        {
            // Środek obrazu
            int centerX = width / 2;
            int centerY = height / 2;
            double maxDistance = Math.Sqrt(centerX * centerX + centerY * centerY);

            // Liczba linii na jeden kawałek (chunk)
            int chunkSize = Math.Max(1, height / (threadCount * 4)); // Dzielimy obraz na mniejsze części (4x więcej niż wątków)

            // Użycie Parallel.ForEach z dynamicznym podziałem na kawałki (range)
            var ranges = Enumerable.Range(0, height).GroupBy(y => y / chunkSize);

            Parallel.ForEach(ranges, new ParallelOptions { MaxDegreeOfParallelism = threadCount }, range =>
            {
                foreach (int y in range)
                {
                    for (int x = 0; x < width; x++)
                    {
                        // Wyznaczenie odległości piksela od środka
                        double dx = x - centerX;
                        double dy = y - centerY;
                        double distance = Math.Sqrt(dx * dx + dy * dy) / maxDistance;

                        // Wyliczenie współczynnika winiety
                        double vignetteFactor = 1 - (distance * intensity);
                        vignetteFactor = Math.Max(0, Math.Min(1, vignetteFactor));

                        // Indeks piksela w tablicy bajtów (przesunięcie w buforze)
                        int pixelIndex = y * stride + x * 3; // Każdy piksel to 3 bajty: R, G, B

                        // Modyfikacja wartości kanałów koloru
                        imageData[pixelIndex] = (byte)(imageData[pixelIndex] * vignetteFactor);       // Blue
                        imageData[pixelIndex + 1] = (byte)(imageData[pixelIndex + 1] * vignetteFactor); // Green
                        imageData[pixelIndex + 2] = (byte)(imageData[pixelIndex + 2] * vignetteFactor); // Red
                    }
                }
            });
        }

    }
}
