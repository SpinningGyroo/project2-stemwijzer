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

namespace project2
{
    /// <summary>
    /// Interaction logic for Keuze.xaml
    /// </summary>
    public partial class Keuze : Window
    {
        public Keuze()
        {
            InitializeComponent();
        }

        private void backAction(object sender, MouseButtonEventArgs e)
        {

        }

        private void mainMenubtn(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
