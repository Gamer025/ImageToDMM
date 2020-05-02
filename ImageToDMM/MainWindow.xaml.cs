using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Collections.Generic;

namespace ImageToDMM
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Conversion conversion = new Conversion();
        string programPath = AppDomain.CurrentDomain.BaseDirectory;

        public MainWindow()
        {
            InitializeComponent();
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
                List<int>[,] mergedRGBArray = conversion.mergeArrays(arrayList);
                List<string>[,] mergedObjectArray = conversion.RGBArraytoObjectArrrays(mergedRGBArray);
            }
            else if (GIFMode.IsChecked == true)
            {

            }
        }
    }
}
