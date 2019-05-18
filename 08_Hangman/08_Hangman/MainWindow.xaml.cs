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

namespace _08_Hangman
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string[] wordPool = { "acres", "adult", "advice", "arrangement", "attempt", "August", "Autumn", "border", "breeze", "brick", "calm", "canal", "Casey", "cast", "chose", "claws", "coach", "constantly", "contrast", "cookies", "customs", "damage", "Danny", "deeply", "depth", "discussion", "doll", "donkey", "Egypt", "Ellen", "essential", "exchange", "exist", "explanation", "facing", "film", "finest", "fireplace", "floating", "folks", "fort", "garage", "grabbed", "grandmother", "habit", "happily", "Harry", "heading", "hunter", "Illinois", "image", "independent", "instant", "January", "kids", "label", "Lee", "lungs", "manufacturing", "Martin", "mathematics", "melted", "memory", "mill", "mission", "monkey", "Mount", "mysterious", "neighborhood", "Norway", "nuts", "occasionally", "official", "ourselves", "palace", "Pennsylvania", "Philadelphia", "plates", "poetry", "policeman", "positive", "possibly", "practical", "pride", "promised", "recall", "relationship", "remarkable", "require", "rhyme", "rocky", "rubbed", "rush", "sale", "satellites", "satisfied", "scared", "selection", "shake", "shaking", "shallow", "shout", "silly", "simplest", "slight", "slip", "slope", "soap", "solar", "species", "spin", "stiff", "swung", "tales", "thumb", "tobacco", "toy", "trap", "treated", "tune", "University", "vapor", "vessels", "wealth", "wolf", "zoo" };
        string word;
        string censoredWord;
        int error = 0;
        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            InitImages();
            NewWordButton_Click(new object(), new RoutedEventArgs());
        }
        private void InitImages()
        {
            Hangman1.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/hangman1.png"));
            Hangman2.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/hangman2.png"));
            Hangman3.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/hangman3.png"));
            Hangman4.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/hangman4.png"));
            Hangman5.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/hangman5.png"));
            Hangman6.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/hangman6.png"));
            Hangman7.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/hangman7.png"));
            Hangman8.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/hangman8.png"));
            Hangman9.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/hangman9.png"));
            Hangman10.Source = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/hangman10.png"));
        }

        private void NewWordButton_Click(object sender, RoutedEventArgs e)
        {
            word = wordPool[random.Next(wordPool.Length)].ToUpper();
            censoredWord = "";
            for (int i = 0; i < word.Length; i++)
                censoredWord += "_ ";
            Word.Text = censoredWord;
            Word.Background = Brushes.White;
            foreach (Button b in KeyboardGrid.Children)
                b.IsEnabled = true;
            foreach (Image i in HangmanGrid.Children)
                i.Opacity = 0;
            error = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ((Button)sender).IsEnabled = false;
            char letter = ((string)((Button)sender).Content)[0];
            char[] censoredWordAsString = censoredWord.ToCharArray();
            char[] wordAsString = word.ToCharArray();
            if (!word.Contains(letter))
            {
                error++;
                for (int i=0;i<error;i++)
                {
                    HangmanGrid.Children[i].Opacity = 1;
                }
                if (error == 10)
                {
                    char[] wordAsChar = new char[word.Length * 2];
                    for (int i=0;i<wordAsChar.Length;i+=2)
                    {
                        wordAsChar[i] = word[i / 2];
                        wordAsChar[i + 1] = ' ';
                    }
                    Word.Background = Brushes.LightPink;
                    Word.Text = new string(wordAsChar);
                    foreach (Button b in KeyboardGrid.Children)
                        b.IsEnabled = false;
                }
            }
            else
            {
                for (int i = 0; i < wordAsString.Length; i++)
                {
                    if (wordAsString[i] == letter)
                    {
                        censoredWordAsString[i * 2] = letter;
                    }
                }
                censoredWord = new string(censoredWordAsString);
                Word.Text = censoredWord;

                if (!censoredWord.Contains("_"))
                {
                    Word.Background = Brushes.LightGreen;
                    foreach (Button b in KeyboardGrid.Children)
                        b.IsEnabled = false;
                }
            }
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key >= Key.A && e.Key <= Key.Z)
            {
                foreach (Button b in KeyboardGrid.Children)
                    if (((string)(b.Content))[0] == e.Key.ToString().ToCharArray()[0])
                    {
                        Button_Click(b, e);
                        break;
                    }
            }
        }
    }
}
