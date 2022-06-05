using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace ImageToDMM
{
    class Conversion
    {
        private Dictionary<int, string> RGBMap = new Dictionary<int, string>();

        public int[,] BitmapToArray(Bitmap image)
        {
            int imageHeight = image.Height; // ---
            int imageWidth = image.Width;  // |||
            int[,] returnArray = new int[imageHeight, imageWidth];

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

        public string ListToMapTile(List<string> paths)
        {
            string returnString = "";
            StringBuilder sb = new StringBuilder();
            sb.Append("(\n");
            sb.Append(string.Join(",\n", paths.Where(s => !string.IsNullOrEmpty(s))));
            sb.Append(")");
            returnString = sb.ToString();

            return returnString;
        }

        public string[,] ObjectArrayToMapDataArray(List<string>[,] objectArray, Dictionary<List<string>, string> ListToIdentifier)
        {
            int arrayHeight = objectArray.GetLength(0);
            int arrayWidth = objectArray.GetLength(1);
            string[,] returnArray = new string[arrayHeight, arrayWidth];

            for (int currentHeight = 0; currentHeight < arrayHeight; currentHeight++)
            {
                for (int currentWidth = 0; currentWidth < arrayWidth; currentWidth++)
                {
                    foreach (KeyValuePair<List<string>, string> DictionaryEntry in ListToIdentifier)
                    {
                        if (objectArray[currentHeight, currentWidth].SequenceEqual(DictionaryEntry.Key))
                        {
                            returnArray[currentHeight, currentWidth] = DictionaryEntry.Value;
                        }
                    }
                }
            }
            return returnArray;
        }

        public string MapDataArrayToDMM (string[,] mapDataArray)
        {
            int arrayHeight = mapDataArray.GetLength(0);
            int arrayWidth = mapDataArray.GetLength(1);
            StringBuilder sb = new StringBuilder();

            for (int currentHeight = 0; currentHeight < arrayHeight; currentHeight++)
            {
                sb.Append("(" + (currentHeight+1) + ",1,1) = {\"\n");
                for (int currentWidth = 0; currentWidth < arrayWidth; currentWidth++)
                {
                    sb.Append(mapDataArray[currentHeight,currentWidth] + "\n");
                }
                sb.Append("\"}\n");
            }

            return sb.ToString();
        }

        public Bitmap BitmapSourceToBitmap (System.Windows.Media.Imaging.BitmapSource bitmapSource)
        {
            Bitmap bitmap;
            using (MemoryStream outStream = new MemoryStream())
            {
                System.Windows.Media.Imaging.BitmapEncoder encoder = new System.Windows.Media.Imaging.BmpBitmapEncoder();

                encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bitmapSource));
                encoder.Save(outStream);
                bitmap = new Bitmap(outStream);
            }
            return bitmap;
        }
    }
}
