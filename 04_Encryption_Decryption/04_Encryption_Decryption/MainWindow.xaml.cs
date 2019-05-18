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

namespace _04_Encryption_Decryption
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string oldOffset = "";
        char[] message;
        string msg = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(OffsetBox.Text, out int offset))
                OffsetBox.Text = "1";
            message = MessageBox.Text.ToCharArray();
            for (int i = 0; i < message.Length; i++)
            {
                int charAsInt = (int)(message[i]) - offset;
                message[i] = (char)(charAsInt);
                msg += message[i];
            }
            OutcomeBox.Text = msg;
            msg = "";
        }
        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(OffsetBox.Text, out int offset))
                OffsetBox.Text = "1";
            message = MessageBox.Text.ToCharArray();
            for (int i = 0; i < message.Length; i++)
            {
                int charAsInt = (int)(message[i]) + offset;
                message[i] = (char)(charAsInt);
                msg += message[i];
            }
            OutcomeBox.Text = msg;
            msg = "";
        }

        private void OffsetBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (OffsetBox.Text != "" && OffsetBox.Text != "-" && !int.TryParse(OffsetBox.Text, out int offset) && offset > 32)
            {
                OffsetBox.Text = oldOffset;
            } else
            {
                oldOffset = OffsetBox.Text;
            }
        }

        
    }
}
