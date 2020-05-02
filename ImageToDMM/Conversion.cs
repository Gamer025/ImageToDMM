using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ImageToDMM
{
    class Conversion
    {
        private Dictionary<int, string> RGBMap = new Dictionary<int, string>();

        public int[,] BitmapToArray(Bitmap image)
        {
            int imageHeight = image.Height; // ---
            int imageWidth = image.Width;  // |||
            int[,] returnArray = new int[imageWidth, imageHeight];

            for (int currentHeight = 0; currentHeight < imageHeight; currentHeight++)
            {
                for (int currentWidth = 0; currentWidth < imageWidth; currentWidth++)
                {
                    Color currentPixel = image.GetPixel(currentWidth, currentHeight);
                    returnArray[currentHeight, currentWidth] = Convert.ToInt32(currentPixel.R.ToString() + currentPixel.G.ToString() + currentPixel.B.ToString());
                }
            }

            return returnArray;
        }

        public void updateRGBMap(string configFile)
        {
            RGBMap.Clear(); //Empty the Map incase it has already been popluated by an earlier run

            string[] lines = File.ReadAllLines(configFile);

            foreach (string line in lines)
            {
                if (!line.StartsWith("#"))
                {
                    string[] splitLine = line.Split('=');
                    RGBMap.Add(Convert.ToInt32(splitLine[0].Trim().Replace(",", "")), splitLine[1].Trim()); //Remove trailing and leading spaces and then comma from RGB definiton, convert it to int and add it to the dictionary together with the text with trimmed spaces after the =
                }
            }
        }

        public string RGBToObject(int RGB)
        {
            string returnString = "";
            RGBMap.TryGetValue(RGB, out returnString);
            return returnString;
        }

        public List<int>[,] mergeArrays(List<int[,]> arrays)
        {
            int arrayHeight = arrays[0].GetLength(0);
            int arrayWidth = arrays[0].GetLength(1);
            List<int>[,] returnArray = new List<int>[arrayHeight, arrayWidth];
            foreach (int[,] currentArray in arrays)
            {
                for (int currentHeight = 0; currentHeight < arrayHeight; currentHeight++)
                {
                    for (int currentWidth = 0; currentWidth < arrayWidth; currentWidth++)
                    {
                        if (returnArray[currentHeight, currentWidth] == null)
                            returnArray[currentHeight, currentWidth] = new List<int>();
                        returnArray[currentHeight, currentWidth].Add(currentArray[currentHeight, currentWidth]);
                    }
                }
            }
            return returnArray;
        }

        public List<string>[,] RGBArraytoObjectArrrays(List<int>[,] RGBArray)
        {
            int arrayHeight = RGBArray.GetLength(0);
            int arrayWidth = RGBArray.GetLength(1);
            List<string>[,] returnArray = new List<string>[arrayHeight, arrayWidth];

            for (int currentHeight = 0; currentHeight < arrayHeight; currentHeight++)
            {
                for (int currentWidth = 0; currentWidth < arrayWidth; currentWidth++)
                {
                    List<string> currentList = new List<string>();
                    foreach (int currentInt in RGBArray[currentHeight, currentWidth])
                    {
                        currentList.Add(RGBToObject(currentInt));
                    }
                    returnArray[currentHeight, currentWidth] = new List<string>(currentList);
                }
            }
            return returnArray;
        }
    }
}
