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
using System.Windows.Threading;
using System.Threading;

namespace _12_Internet_Time
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread childLoopThread;

        public MainWindow()
        {
            InitializeComponent();
            ThreadStart loopThread = new ThreadStart(UpdateClocksLooped);
            childLoopThread = new Thread(loopThread);
            childLoopThread.Start();
        }
        private void UpdateClocksLooped()
        {
            while (true)
            {
                UpdateClocks();
                System.Threading.Thread.Sleep(100);
            }
        }
        private void UpdateClocks()
        {
            DateTime currentTime = DateTime.Now;

            DateTime UTCTime = currentTime.ToUniversalTime();
            UTCTime = UTCTime.AddHours(1);

            string currentTimeString = "";
            if (currentTime.TimeOfDay.Hours < 10)
                currentTimeString += "0";
            currentTimeString += currentTime.TimeOfDay.Hours + ":";
            if (currentTime.TimeOfDay.Minutes < 10)
                currentTimeString += "0";
            currentTimeString += currentTime.TimeOfDay.Minutes + ":";
            if (currentTime.TimeOfDay.Seconds < 10)
                currentTimeString += "0";
            currentTimeString += currentTime.TimeOfDay.Seconds.ToString();

            string UTCTimeString = "";
            if (UTCTime.TimeOfDay.Hours < 10)
                UTCTimeString += "0";
            UTCTimeString += UTCTime.TimeOfDay.Hours + ":";
            if (UTCTime.TimeOfDay.Minutes < 10)
                UTCTimeString += "0";
            UTCTimeString += UTCTime.TimeOfDay.Minutes + ":";
            if (UTCTime.TimeOfDay.Seconds < 10)
                UTCTimeString += "0";
            UTCTimeString += UTCTime.TimeOfDay.Seconds.ToString();

            string internetTime = "@" + Math.Round((UTCTime.TimeOfDay.TotalSeconds * 10 / 864), 3);
            while (internetTime.Length < 8)
                internetTime += "0";

            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate () {
                CurrentClock.Text = currentTimeString;
                UTCClock.Text = UTCTimeString;
                InternetClock.Text = internetTime;
            });
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            childLoopThread.Abort();
        }
    }
}
