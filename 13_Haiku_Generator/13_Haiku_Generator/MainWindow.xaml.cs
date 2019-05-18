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
using System.Windows.Forms;

namespace _13_Haiku_Generator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();

        string[][] nouns = { new string[] { " dog", " man", " house", " meal", " sword", " tree" },
                             new string[] { " apple", " mother", " swordman", " hunger", " monster", " beauty" },
                             new string[] { " energy", " memory", " medicine", " illusion" }};
        string[][] adjectives = { new string[] { " small", " shy", " weak", " strnong", " giant" },
                                  new string[] { " careful", " silver", " angry", " empty", " gentle" },
                                  new string[] { " powerful", " enormous", " meaningless", " limitless", " priceless" } };
        string[][] verbs = { new string[] { " cleans", " thinks", " loves", " is", " has" },
                             new string[] { " opens", " fastens", " studies", " begins", " survives" },
                             new string[] { " rearrange", " empowers", " encounters", " abandons", " assembles" } };
        string[][] adverbs = { new string[] { " ERROR" },
                               new string[] { " bravely", " blindly", " quickly", " calmly" },
                               new string[] { " restlessly", " without hope", " with mercy", " certainly", " truthfully" } };


        public MainWindow()
        {
            InitializeComponent();
        }

        private void HewHaiku_Click(object sender, RoutedEventArgs e)
        {
            string text1 = "A";
            string text2 = "";
            string text3 = "";

            int syllabes = random.Next(3);
            int index = random.Next(adjectives[syllabes].Length);
            if ("aeiouy".IndexOf((adjectives[syllabes][index])[1]) >= 0)
                text1 += "n";
            text1 += adjectives[syllabes][index];
            text1 += nouns[2 - syllabes][random.Next(nouns[2 - syllabes].Length)];


            syllabes = random.Next(3);
            int syllabes2;
            syllabes2 = random.Next((2 - syllabes), 3);
            
            text2 += verbs[syllabes][random.Next(verbs[syllabes].Length)];
            text2 = text2.Remove(0,1);
            text2 += adjectives[syllabes2][random.Next(adjectives[syllabes2].Length)];
            text2 += nouns[4 - syllabes - syllabes2][random.Next(nouns[4 - syllabes - syllabes2].Length)];
            text2 += ",";

            syllabes = random.Next(1, 3);
            text3 += verbs[syllabes][random.Next(verbs[syllabes].Length)];
            text3 = text3.Remove(0, 1);
            text3 += adverbs[3 - syllabes][random.Next(adverbs[3 - syllabes].Length)];
            text3 += ".";

            Text1.Text = text1;
            Text2.Text = text2;
            Text3.Text = text3;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Text file|*.txt";
            save.Title = "Save Haiku to file";
            save.ShowDialog();

            if (save.FileName != "")
            {
                System.IO.FileStream stream = (System.IO.FileStream)save.OpenFile();
                stream.Write(new UTF8Encoding(true).GetBytes(Text1.Text), 0, Text1.Text.Length);
                stream.Write(new UTF8Encoding(true).GetBytes("\n"), 0, 1);
                stream.Write(new UTF8Encoding(true).GetBytes(Text2.Text), 0, Text2.Text.Length);
                stream.Write(new UTF8Encoding(true).GetBytes("\n"), 0, 1);
                stream.Write(new UTF8Encoding(true).GetBytes(Text3.Text), 0, Text3.Text.Length);
                stream.Close();
            }
        }
    }

    
}
