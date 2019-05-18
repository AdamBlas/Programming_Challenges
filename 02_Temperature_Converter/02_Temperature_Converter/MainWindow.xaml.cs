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

namespace _02_Temperature_Converter
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string Box1PreChange = "0";
        private double input;
        private double output;
        List<string> options = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            PrepareOptions();
            ComboBoxInit();
        }

        private void PrepareOptions()
        {
            options.Add("Celcius Degree");
            options.Add("Kelvin");
            options.Add("Farenheit Degree");

            // Add new options here, if you do, remember to modify convert algorithm

        }
        private void ComboBoxInit()
        {
            foreach (string s in options)
            {
                ComboInput.Items.Add(s);
                ComboOutput.Items.Add(s);
            }
            ComboInput.SelectedItem = options[0];
            ComboOutput.SelectedItem = options[0];
        }
        private void Box1_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Box1.Text == "" || Box1.Text == "-" || double.TryParse(Box1.Text, out double value))
            {
                Box1PreChange = Box1.Text;
            }
            else
            {
                int foundPosition = FindPositionOfIllegalCharacter(Box1.Text);
                Box1.Text = Box1PreChange;
                Box1.SelectionStart = foundPosition;
            }
            if (double.TryParse(Box1.Text, out value))
            {
                ConvertInput();
                ConvertOutput();
            }
            SetOutputTextBox();
        }
        private int FindPositionOfIllegalCharacter(string text)
        {
            char[] textAsChar = text.ToCharArray();
            for (int i = 0; i < text.Length; i++)
                if (textAsChar[i] != '-' && textAsChar[i] != ',' && (textAsChar[i] < 48 || textAsChar[i] > 57))
                    return i;
            return text.Length;
        }
        private void ComboInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConvertInput();
            ConvertOutput();
            SetOutputTextBox();
        }
        private void ComboOutput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ConvertOutput();
            SetOutputTextBox();
        }
        private void ConvertInput()
        {
            if (ComboInput != null)
            {
                if (ComboInput.SelectedItem.Equals(options[0]))
                    input = double.Parse(Box1.Text) + 273.15;
                else if (ComboInput.SelectedItem.Equals(options[1]))
                    input = double.Parse(Box1.Text);
                else if (ComboInput.SelectedItem.Equals(options[2]))
                    input = (double.Parse(Box1.Text) + 459.67) * 5 / 9;
                else
                    throw new ConversionNotImplementedException();
                input = Math.Round(input, 2);
            }
        }
        private void ConvertOutput()
        {
            if (ComboOutput != null && ComboOutput.SelectedItem != null)
            {
                if (ComboOutput.SelectedItem.Equals(options[0]))
                    output = input - 273.15;
                else if (ComboOutput.SelectedItem.Equals(options[1]))
                    output = input;
                else if (ComboOutput.SelectedItem.Equals(options[2]))
                    output = (input * 9 / 5) - 459.67;
                else
                    throw new ConversionNotImplementedException();
                output = Math.Round(output, 2);
            }
        }
        private void SetOutputTextBox()
        {
            if (Box1.Text == "" || Box1.Text == "-")
                Box2.Text = "";
            else if (Box2 != null)
                Box2.Text = output.ToString();
        }
    }
    public class ConversionNotImplementedException : Exception
    {
        // Exception thrown while trying converting temperature without conversion algorithm
        // (Option is added to ComboBox, but not implemented in converter)
    }
}
