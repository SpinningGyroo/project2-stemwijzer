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
            for (int row = 1; row < 9; row += 2)
            {
                for (int col = 2; col < 9; col += 2)
                {
                    ImageBrush imageBrush = new ImageBrush();
                    imageBrush.ImageSource = new BitmapImage(new Uri("images/bluecircle.png", UriKind.Relative));

                    Rectangle rectangle = new Rectangle();
                    rectangle.Fill = imageBrush;
                    rectangle.MouseDown += historySubShow;
                    rectangle.Margin = new Thickness(4);
                    Grid.SetRow(rectangle, row);
                    Grid.SetColumn(rectangle, col);
                    redGrid.Children.Add(rectangle);
                }


            }
            ImageBrush imageBrushh = new ImageBrush();
            imageBrushh.ImageSource = new BitmapImage(new Uri("images/redcircle.png", UriKind.Relative));

            Rectangle rectanglee = new Rectangle();
            rectanglee.Fill = imageBrushh;
            rectanglee.MouseDown += historySubShow;
            Grid.SetRow(rectanglee, 0);
            Grid.SetColumn(rectanglee, 4);
            historySub.Children.Add(rectanglee);
            historySub.Visibility = Visibility.Hidden;
        }

        private void historySubShow(object sender, MouseButtonEventArgs e)
        {
            if (historySub.Visibility == Visibility.Visible)
            {
                historySub.Visibility = Visibility.Hidden;
            }

            else
            {
                historySub.Visibility = Visibility.Visible;
            }
        }
    }
}
