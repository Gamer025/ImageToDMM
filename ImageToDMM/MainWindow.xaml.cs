using System.Windows;
using System.IO;
using System;
using System.Drawing;

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
            if (PNGMode.IsChecked == true)
            {
                if (!File.Exists(programPath + "turf.png"))
                {
                    //Log to RichTextBox
                    return;
                }
                int[,] turfs = conversion.BitmapToArray(new Bitmap(programPath + "turf.png"));
            }
            else if (GIFMode.IsChecked == true)
            {

            }
        }
    }
}
