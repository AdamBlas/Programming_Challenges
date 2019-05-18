using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _09_Love_Calculator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BitmapImage heart1 = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/heart1.png"));
        BitmapImage heart2 = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/heart2.png"));
        BitmapImage heart3 = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/heart3.png"));
        BitmapImage heart4 = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/heart4.png"));


        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            int hisSum = 0;
            int herSum = 0;
            char[] herName = HerName.Text.ToCharArray();
            char[] hisName = HisName.Text.ToCharArray();

            for (int i=0;i<herName.Length;i++)
                herSum += herName[i];
            for (int i = 0; i < hisName.Length; i++)
                hisSum += hisName[i];

            int outcome = 100 - (Math.Abs(herSum - hisSum) % 100);
            Percentage.Text = outcome.ToString() + "%";
            SetHeart(outcome);
        }

        private void SetHeart(int outcome)
        {
            if (outcome <= 25)
            {
                HeartImage.Source = heart1;
            } else if (outcome > 25 && outcome <= 50)
            {
                HeartImage.Source = heart2;
            } else if (outcome > 50 && outcome <= 75)
            {
                HeartImage.Source = heart3;
            } else
            {
                HeartImage.Source = heart4;
            }
        }
    }
}
