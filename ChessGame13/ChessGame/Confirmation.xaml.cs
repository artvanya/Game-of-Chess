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
using System.Windows.Shapes;

namespace ChessGame
{
    /// <summary>
    /// Логика взаимодействия для Confirmation.xaml
    /// </summary>
    public partial class Confirmation : Window
    {
        public int result;
        public Confirmation()
        {
            InitializeComponent();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            result = 1;
            this.Close();
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            result = 2;
            this.Close();
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            result = 3;
            this.Close();
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            result = 4;
            this.Close();
        }
    }
}
