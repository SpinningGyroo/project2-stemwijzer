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
        int partycounter = 0;
        public MainWindow()
        {
            InitializeComponent();
            partycreator();
            btnCreateUser.Visibility = Visibility.Hidden;
            btnCreateUser.IsEnabled = false;
        }

        private void partycreator()
        {
             for (int row = 1; row < 6; row++)
             {
                 partycounter++;
                 for (int col = 1; col < 4; col++)
                 {
                     ImageBrush imageBrush = new ImageBrush();
                     imageBrush.ImageSource = new BitmapImage(new Uri("images/bluecircle.png", UriKind.Relative));

                     Rectangle rectangle = new Rectangle();
                     rectangle.Fill = imageBrush;
                     rectangle.Margin = new Thickness(4);
                     Grid.SetRow(rectangle, row);
                     Grid.SetColumn(rectangle, col);
                     gridPartijen.Children.Add(rectangle);
                }
              }

            ImageBrush finalImageBrush = new ImageBrush();
            finalImageBrush.ImageSource = new BitmapImage(new Uri("images/bluecircle.png", UriKind.Relative));

            Rectangle finalRectangle = new Rectangle();
            finalRectangle.Fill = finalImageBrush;
            finalRectangle.Margin = new Thickness(4);
            Grid.SetRow(finalRectangle, 6);
            Grid.SetColumn(finalRectangle, 2);
            gridPartijen.Children.Add(finalRectangle);
        }

        private void temp_Click(object sender, RoutedEventArgs e)
        {
            History screen = new History();
            screen.Show();
        }

        private void signupClick(object sender, RoutedEventArgs e)
        {
            btnLogin.Visibility = Visibility.Hidden;
            btnLogin.IsEnabled = false;
            btnSignup.Visibility = Visibility.Hidden;
            btnSignup.IsEnabled = false;

            btnCreateUser.Visibility = Visibility.Visible;
            btnCreateUser.IsEnabled = true;
        }

        private void createuserClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
