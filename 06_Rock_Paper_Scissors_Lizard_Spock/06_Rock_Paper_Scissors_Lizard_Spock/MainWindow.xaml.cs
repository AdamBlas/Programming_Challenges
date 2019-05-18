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
using System.Drawing;

namespace _06_Rock_Paper_Scissors_Lizard_Spock
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum Option
        {
            Scissors, Paper, Rock, Lizard, Spock
        }
        ImageSource rock = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/rock.png"));
        ImageSource paper = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/paper.png"));
        ImageSource scissors = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/scissors.png"));
        ImageSource lizard = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/lizard.png"));
        ImageSource spock = new BitmapImage(new Uri(System.IO.Directory.GetCurrentDirectory() + "/../../Images/spock.png"));
        Option player;
        Option rival;
        Random random = new Random();
        private int _score;
        private int score
        {
            get
            {
                return _score;
            }
            set
            {
                if (_score < value)
                {
                    Score.Foreground = System.Windows.Media.Brushes.Green;
                }
                else if (_score > value)
                {
                    Score.Foreground = System.Windows.Media.Brushes.Red;
                }
                else
                {
                    Score.Foreground = System.Windows.Media.Brushes.Gray;
                }
                _score = value;
                Score.Text = _score.ToString();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            score = 0;
            Image2.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);
            ScaleTransform flip = new ScaleTransform
            {
                ScaleX = -1
            };
            Image2.RenderTransform = flip;
        }

        private void Rock_Click(object sender, RoutedEventArgs e)
        {
            Image1.Source = rock;
            player = Option.Rock;
            StartGame();
        }
        private void Paper_Click(object sender, RoutedEventArgs e)
        {
            Image1.Source = paper;
            player = Option.Paper;
            StartGame();
        }
        private void Scissors_Click(object sender, RoutedEventArgs e)
        {
            Image1.Source = scissors;
            player = Option.Scissors;
            StartGame();
        }
        private void Lizard_Click(object sender, RoutedEventArgs e)
        {
            Image1.Source = lizard;
            player = Option.Lizard;
            StartGame();
        }
        private void Spock_Click(object sender, RoutedEventArgs e)
        {
            Image1.Source = spock;
            player = Option.Spock;
            StartGame();
        }
        private void StartGame()
        {
            rival = RandomiseRival();

            if (((int)player + 1) % 5 == (int)rival || ((int)player + 3) % 5 == (int)rival) 
            {
                score++;
                PlayerTitle.Text = "VICTORY";
                RivalTitle.Text = "LOSE";
                PlayerTitle.Foreground = System.Windows.Media.Brushes.Green;
                RivalTitle.Foreground = System.Windows.Media.Brushes.Red;

            }
            else if (((int)rival + 1) % 5 == (int)player || ((int)rival + 3) % 5 == (int)player)
            {
                score--;
                PlayerTitle.Text = "LOSE";
                RivalTitle.Text = "VICTORY";
                PlayerTitle.Foreground = System.Windows.Media.Brushes.Red;
                RivalTitle.Foreground = System.Windows.Media.Brushes.Green;
            }
            else
            {
                score = score;
                PlayerTitle.Text = "DRAW";
                RivalTitle.Text = "DRAW";
                PlayerTitle.Foreground = System.Windows.Media.Brushes.Gray;
                RivalTitle.Foreground = System.Windows.Media.Brushes.Gray;
            }
        }
        private Option RandomiseRival()
        {
            Option value = (Option)random.Next(5);
            if (value == Option.Rock)
            {
                Image2.Source = rock;
            }
            else if (value == Option.Paper)
            {
                Image2.Source = paper;
            }
            else if (value == Option.Scissors)
            {
                Image2.Source = scissors;
            }
            else if (value == Option.Lizard)
            {
                Image2.Source = lizard;
            }
            else if (value == Option.Spock)
            {
                Image2.Source = spock;
            }
            
            return value;
        }

    }
}
