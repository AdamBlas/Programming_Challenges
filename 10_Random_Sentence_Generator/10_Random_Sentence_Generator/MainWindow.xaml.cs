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

namespace _10_Random_Sentence_Generator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();

        string[] sentence1 =
        {
            "Random man once said:",
            "If you want to be strong,",
            "Remember,",
            "Space is infinite, yet",
            "Education is important, but",
            "You can retake exam, but",
            "That feeling when",
            "Sit down and write:",
            "Man fears one thing,"
        };
        string[] sentence2 =
        {
            "you can't kill mosquito which sat on your balls.",
            "dont lose.",
            "Braum lives another day.",
            "stupidness is limitless.",
            "beer is importanter.",
            "you can't retake a party.",
            "your mom friendzoned you.",
            "I am fabulous.",
            "there are women with penises"
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Text1.Text = sentence1[random.Next(sentence1.Length)];
            Text2.Text = sentence2[random.Next(sentence2.Length)];
        }
    }
}
