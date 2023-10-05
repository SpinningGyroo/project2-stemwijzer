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
            LoggedIn.Visibility = Visibility.Hidden;
            SignUp.Visibility = Visibility.Hidden;
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

        private void HistoryClick(object sender, RoutedEventArgs e)
        {
            History screen = new History();
            screen.Show();
        }

        private void signupClick(object sender, RoutedEventArgs e)
        {
            


            MainWindowBorder.Visibility = Visibility.Hidden;
            SignUp.Visibility = Visibility.Visible;
        }

        private void createuserClick(object sender, RoutedEventArgs e)
        {
            SignUp.Visibility = Visibility.Hidden;
            MainWindowBorder.Visibility = Visibility.Visible;
        }

        private void loginClick(object sender, RoutedEventArgs e)
        {
            MainWindowBorder.Visibility = Visibility.Hidden;
            LoggedIn.Visibility = Visibility.Visible;

            ImageBrush ProfileImage = new ImageBrush();
            ProfileImage.ImageSource = new BitmapImage(new Uri("images/uta.jpg", UriKind.Relative));

            Rectangle ProfileRectangle = new Rectangle();
            ProfileRectangle.Margin = new Thickness(4);
            ProfileRectangle.Fill = ProfileImage;
            Grid.SetRow(ProfileRectangle, 1);
            Grid.SetColumn(ProfileRectangle, 1);
            Grid.SetColumnSpan(ProfileRectangle, 2);
            Grid.SetRowSpan(ProfileRectangle, 4);

            gridLoggedIn.Children.Add(ProfileRectangle);

            ImageBrush ProfileCanvas = new ImageBrush();
            ProfileCanvas.ImageSource = new BitmapImage(new Uri("images/profileimage-canvas.png", UriKind.Relative));

            Rectangle CanvasRectangle = new Rectangle();
            CanvasRectangle.Fill = ProfileCanvas;
            Grid.SetRow(CanvasRectangle, 1);
            Grid.SetColumn(CanvasRectangle, 1);
            Grid.SetColumnSpan(CanvasRectangle, 2);
            Grid.SetRowSpan(CanvasRectangle, 4);

            gridLoggedIn.Children.Add(CanvasRectangle);
        }

        private void GoBackClick(object sender, RoutedEventArgs e)
        {
            SignUp.Visibility = Visibility.Hidden;
            MainWindowBorder.Visibility = Visibility.Visible;
        }

        private void logoutClick(object sender, RoutedEventArgs e)
        {
            LoggedIn.Visibility = Visibility.Hidden;
            MainWindowBorder.Visibility = Visibility.Visible;
        }
    }
}
