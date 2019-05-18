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

namespace _11_Password_Generator
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        string[] words = { "password", "Password", "admin", "Admin", "user", "User", "login", "Login", "1234", "security", "pro", "unhackable", "pass",
            "qwerty", "abc", "letmein", "111" };

        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string password = "";
            if (randomWords.IsChecked == true)
            {
                for (int i = random.Next(2, 4); i > 0; i--)
                    password += words[random.Next(words.Length)];
            } else
            {
                for (int i = random.Next(6, 12); i > 0; i--) 
                {
                    char character = (char)(random.Next(61) + 48);
                    if (character > 57)
                        character = (char)((int)(character) + 7);
                    if (character > 90)
                        character = (char)((int)(character) + 7);
                    password += character;
                }
            }
            passwordBox.Text = password;
            CheckStrengh(password);
        }

        private void CheckStrengh(string password)
        {
            int score = ComputeStrengh(password);

            Strengh.Value = score;

            if (score <= 8)                        // 0 - 8
            {
                Strengh.Foreground = Brushes.Red;
                StrengText.Text = "Very weak";
            } else if (score <= 12)                 // 9 - 12
            {
                Strengh.Foreground = Brushes.Orange;
                StrengText.Text = "Weak";
            }
            else if (score <= 16)                   // 13 - 16
            {
                Strengh.Foreground = Brushes.Yellow;
                StrengText.Text = "Average";
            }
            else if (score <= 24)                   // 27 - 22
            {
                Strengh.Foreground = Brushes.GreenYellow;
                StrengText.Text = "Strong";
            }
            else if (score <= 27)                   // 24 - 27
            {
                Strengh.Foreground = Brushes.Green;
                StrengText.Text = "Very strong";
            }
            else                                    // 28 - 30
            {
                Strengh.Foreground = Brushes.ForestGreen;
                StrengText.Text = "Saitama";
            }
        }

        private int ComputeStrengh(string password)
        {
            char[] passArray = password.ToCharArray();
            int score = 0;
            int digitAmount = 0;
            int lowerAmount = 0;
            int upperAmount = 0;
            
            for (int i=0;i<passArray.Length;i++)
            {
                if (char.IsNumber(passArray[i]))
                    digitAmount++;
                else if (char.IsLower(passArray[i]))
                    lowerAmount++;
                else if (char.IsUpper(passArray[i]))
                    upperAmount++;
            }

            if (digitAmount > 3)
                digitAmount = 3;
            if (lowerAmount > 3)
                lowerAmount = 3;
            if (upperAmount > 3)
                upperAmount = 3;

            score += passArray.Length;
            if (score > 12)
                score = 12;

            score += digitAmount * 3;
            score += lowerAmount * 1;
            score += upperAmount * 2;

            if (score > 16 && (digitAmount == 0 || lowerAmount == 0 || upperAmount == 0))
                score = 16;

            return score;
        }
    }
}
