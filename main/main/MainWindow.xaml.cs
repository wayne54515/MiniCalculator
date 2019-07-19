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

namespace main
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        

        private void Zero_Click(object sender, RoutedEventArgs e)
        {
            input_text.AppendText("0");
        }

        private void One_Click(object sender, RoutedEventArgs e)
        {
            
            input_text.AppendText("1");
        }

        private void Two_Click(object sender, RoutedEventArgs e)
        {
            input_text.AppendText("2");
        }

        private void Three_Click(object sender, RoutedEventArgs e)
        {
            input_text.AppendText("3");
        }

        private void Four_Click(object sender, RoutedEventArgs e)
        {
            input_text.AppendText("4");
        }

        private void Five_Click(object sender, RoutedEventArgs e)
        {
            input_text.AppendText("5");
        }

        private void Six_Click(object sender, RoutedEventArgs e)
        {
            input_text.AppendText("6");
        }

        private void Seven_Click(object sender, RoutedEventArgs e)
        {
            input_text.AppendText("7");
        }

        private void Eight_Click(object sender, RoutedEventArgs e)
        {
            input_text.AppendText("8");
        }

        private void Nine_Click(object sender, RoutedEventArgs e)
        {
            input_text.AppendText("9");
        }

        private void Divide_Click(object sender, RoutedEventArgs e)
        {
            input_text.AppendText("/");
        }

        private void Multiple_Click(object sender, RoutedEventArgs e)
        {
            input_text.AppendText("*");
        }

        private void Sub_Click(object sender, RoutedEventArgs e)
        {
            input_text.AppendText("-");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            input_text.AppendText("+");
        }

        private void Backspace_Click(object sender, RoutedEventArgs e)
        {
            input_text.Undo();
        }

        private void Ac_Click(object sender, RoutedEventArgs e)
        {
            input_text.Clear();
        }

    }
}
