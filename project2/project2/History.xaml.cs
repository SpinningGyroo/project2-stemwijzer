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
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Window
    {
        public History()
        {
            InitializeComponent();
            partycreator();
        }

        private void partycreator()
        {
            for (int row = 1; row < 5; row++)
            {
                for (int col = 1; col < 5; col++)
                {
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri("images/bluecircle.png", UriKind.Relative));

                    Rectangle rectangle = new Rectangle();
                    rectangle.Fill = imageBrush;
                    rectangle.Margin = new Thickness(4);
                    Grid.SetRow(rectangle, row);
                    Grid.SetColumn(rectangle, col);
                    redGrid.Children.Add(rectangle);
                }
            }
        }
    }
}
