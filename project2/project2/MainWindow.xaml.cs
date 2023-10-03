﻿using System;
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

        private void loginClick(object sender, RoutedEventArgs e)
        {
            MainWindowBorder.Visibility = Visibility.Hidden;
            LoggedIn.Visibility = Visibility.Visible;

            ImageBrush ProfileImage = new ImageBrush();
            ProfileImage.ImageSource = new BitmapImage(new Uri("images/uta.jpg", UriKind.Relative));

            Rectangle ProfileRectangle = new Rectangle();
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

            Border CanvasBorder = new Border();
            SolidColorBrush brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0057FF"));
            CanvasBorder.BorderBrush = brush;
            CanvasBorder.BorderThickness = new Thickness(12);
            Grid.SetRow(CanvasBorder, 0);
            Grid.SetColumn(CanvasBorder, 0);
            Grid.SetColumnSpan(CanvasBorder, 3);
            Grid.SetRowSpan(CanvasBorder, 5);




            gridLoggedIn.Children.Add(CanvasRectangle);
            gridLoggedIn.Children.Add(CanvasBorder);

        }
    }
}
