using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageToDMM
{
    class Conversion
    {
        public int[,] BitmapToArray (Bitmap image)
        {
            int imageHeight = image.Height; // ---
            int imageWidth = image.Width;  // |||
            int[,] returnArray = new int[imageWidth, imageHeight];

            for (int currentHeight = 0; currentHeight < imageHeight; currentHeight++)
            {
                for (int currentWidth = 0; currentWidth < imageHeight; currentWidth++)
                {
                    Color currentPixel = image.GetPixel(currentWidth, currentHeight);
                    returnArray[currentHeight, currentWidth] = Convert.ToInt32(currentPixel.R.ToString() + currentPixel.G.ToString() + currentPixel.B.ToString());
                }
            }

            return returnArray;
        }

    }
}
