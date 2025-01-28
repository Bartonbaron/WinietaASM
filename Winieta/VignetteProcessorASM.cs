using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Menu;

namespace Winieta
{
    public static class VignetteProcessorASM
    {
        // Import funkcji z DLL
        [DllImport(@"C:\Users\Bartek\Documents\GitHub\WinietaASM\Winieta\bin\Debug\x64\ImageProcessingASM.dll")]
        private static extern void ApplyVignetteASM(
            byte[] imageData, // rcx - wskaźnik na dane obrazu
            int width,        // rdx - szerokość obrazu
            int height,       // r8  - wysokość obrazu
            int stride,       // r9  - stride
            float intensity,  // [rsp+40] - intensity
            int threadCount   // [rsp+48] - threadCount
        );

        public static void ApplyVignette(byte[] imageData, int width, int height, int stride, float intensity, int threadCount)
        {
            if (imageData == null || imageData.Length == 0)
            {
                throw new ArgumentException("Dane obrazu są puste.");
            }

            ApplyVignetteASM(imageData, width, height, stride, intensity, threadCount);
        }
    }
}
