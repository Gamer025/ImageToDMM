using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using System.Text;

namespace ImageToDMM
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Conversion conversion = new Conversion();
        Helpers helpers = new Helpers();
        string programPath = AppDomain.CurrentDomain.BaseDirectory;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Create_DMM(List<string>[,] mapArray)
        {
            StringBuilder sb = new StringBuilder();
            List<List<string>> uniqueTiles = helpers.ArrayUniqueLists(mapArray);
            //Unique Tiles in DMM format
            string mapTilesDMM;
            //Ints for keeping track of next letter for generating tile identifier (aaa,aab ...), 97 - 122 is lowercase alphabet in ASCII table
            int firstDigit = 97;
            int secondDigit = 97;
            int thirdDigit = 97;
            //A Dictionary to map the unique string lists to the identifier they got assigned
            Dictionary<List<string>, string> ListToIdentifier = new Dictionary<List<string>, string>();

            //Generate TileData of DMM
            foreach (List<string> mapTile in uniqueTiles)
            {
                string identifier = "";
                identifier += ((char)firstDigit).ToString();
                identifier += ((char)secondDigit).ToString();
                identifier += ((char)thirdDigit).ToString();
                if (firstDigit < 122)
                    firstDigit++;
                else if (secondDigit < 122)
                    secondDigit++;
                else
                    thirdDigit++;
                sb.Append("\"" + identifier + "\"" + " = ");    //"aaa" = 
                sb.Append(conversion.ListToMapTile(mapTile));   //(typepaths,...)
                sb.Append("\n");                                //NewLine

                ListToIdentifier.Add(mapTile, identifier);
            }
            mapTilesDMM = sb.ToString();
            string mapDataDMM = conversion.MapDataArrayToDMM(conversion.ObjectArrayToMapDataArray(mapArray, ListToIdentifier));

            File.WriteAllText(programPath+"map.dmm", mapTilesDMM+mapDataDMM);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            conversion.updateRGBMap(programPath + "colorMap.txt");
            List<int[,]> arrayList = new List<int[,]>();
            if (PNGMode.IsChecked == true)
            {
                bool breakLoop = true;

                for (int i = 0; breakLoop; i++)
                {
                    string imagePath = programPath + "layer" + i + ".png";

                    if (!File.Exists(imagePath))
                    {
                        //Log to RichTextBox
                        breakLoop = false;
                        break;
                    }
                    arrayList.Add(conversion.BitmapToArray(new Bitmap(imagePath)));
                }
                Create_DMM(conversion.RGBArraytoObjectArrrays(helpers.mergeArrays(arrayList)));
                
            }
            else if (GIFMode.IsChecked == true)
            {

            }
        }
    }
}
