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

namespace project2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            partycreator();
        }

        private void partycreator()
        {
            for (int row = 1; row < 6; row++) 
            {
                for (int col = 1; col < 4; col++) 
                {
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri("assets/bluecircle.png", UriKind.Relative));

                    Rectangle rectangle = new Rectangle();
                    rectangle.Fill = imageBrush;
                    Grid.SetRow(rectangle, row);
                    Grid.SetColumn(rectangle, col);
                    gridPartijen.Children.Add(rectangle); 
                }
            }
        }
    }
}
