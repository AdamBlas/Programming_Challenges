using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace _50_Tetris
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        Page1 gameBoardPage = new Page1();
        Page currentPage = null;
        public MainWindow()
        {
            InitializeComponent();
            LoadGameBoard();
        }

        private void LoadGameBoard()
        {
            frame.NavigationService.Navigate(gameBoardPage);
            currentPage = gameBoardPage;
        }

        public new void KeyDown(object sender, KeyEventArgs e)
        {
            gameBoardPage.KeyDown(sender, e);
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            gameBoardPage.childLoopThread.Abort();
        }
    }
}
