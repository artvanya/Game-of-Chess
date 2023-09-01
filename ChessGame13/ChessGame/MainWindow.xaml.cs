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

namespace ChessGame
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //this.Width = 1200;
            //this.Height = 900;

            Board board = new Board();


            gridPanel.Width = 900;
            gridPanel.Height = 800;

            ColumnDefinition[] columns = new ColumnDefinition[8];
            RowDefinition[] rows = new RowDefinition[8];

            for (int i = 0; i < columns.Length; i++)
            {
                columns[i] = new ColumnDefinition();
                gridPanel.ColumnDefinitions.Add(columns[i]);
            }
            for (int i = 0; i < rows.Length; i++)
            {
                rows[i] = new RowDefinition();
                gridPanel.RowDefinitions.Add(rows[i]);
            }


            for (int i = 0; i < rows.Length; i++)
            {
                for (int j = 0; j < columns.Length; j++)
                {
                    Cell btn = board.arrBoard[i, j];
                    Grid.SetRow(btn, i);
                    Grid.SetColumn(btn, j);
                    gridPanel.Children.Add(btn);
                }
            }
            btnStepBack.Click += (s, e) => board.BackStep(true);

            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            Rules_of_Chess RulesWindow = new Rules_of_Chess();
            RulesWindow.ShowDialog(); //wait until the window closes
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

    }
}
