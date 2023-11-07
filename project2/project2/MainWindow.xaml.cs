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
using MySql.Data.MySqlClient;
using System.Configuration;
using project2.classes;
using System.Data;
using Microsoft.Win32;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace project2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string loggedInUsername;
        Rectangle ProfileRectangle;
        public MainWindow()
        {
            InitializeComponent();
            partycreator();
            LoggedIn.Visibility = Visibility.Hidden;
            SignUp.Visibility = Visibility.Hidden;

            ImageBrush ProfileImage = new ImageBrush();
            ProfileImage.ImageSource = new BitmapImage(new Uri("images/profileimage.png", UriKind.Relative));

            ProfileRectangle = new Rectangle();
            ProfileRectangle.Margin = new Thickness(4);
            ProfileRectangle.Fill = ProfileImage; // Make sure ProfileImage is defined in your code
            Grid.SetRow(ProfileRectangle, 1);
            Grid.SetColumn(ProfileRectangle, 1);
            Grid.SetColumnSpan(ProfileRectangle, 2);
            Grid.SetRowSpan(ProfileRectangle, 4);

            gridLoggedIn.Children.Add(ProfileRectangle);
        }


        private void partycreator()
        {
            DatabaseHandler dbHandler = new DatabaseHandler();
            DataTable partiesTable = dbHandler.GetParties();

            int row = 1;
            int col = 1;

            for (int i = 0; i < partiesTable.Rows.Count - 1; i++)
            {
                DataRow partyRow = partiesTable.Rows[i];
                int partyId = Convert.ToInt32(partyRow["ID"]);
                byte[] partyLogo = (byte[])partyRow["partij_logo"];
                string partyName = partyRow["naam"].ToString();
                string partyInfo = partyRow["info_tekst"].ToString();
                string partyUrl = partyRow["url"].ToString();

                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(partyLogo);
                bitmapImage.EndInit();
                imageBrush.ImageSource = bitmapImage;

                Rectangle rectangle = new Rectangle();
                rectangle.Fill = imageBrush;
                rectangle.Margin = new Thickness(4);
                Grid.SetRow(rectangle, row);
                Grid.SetColumn(rectangle, col);

                rectangle.MouseLeftButtonDown += (sender, e) =>
                {
                    MessageBoxResult result = MessageBox.Show($"{partyName}, {partyInfo} Wil je door naar de website?", "Partij Info", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(partyUrl);
                    }
                };

                gridPartijen.Children.Add(rectangle);

                col++;

                if (col > 3)
                {
                    col = 1;
                    row++;
                }
            }

            // Add the final party logo to the bottom middle
            if (partiesTable.Rows.Count > 0)
            {
                DataRow finalPartyRow = partiesTable.Rows[partiesTable.Rows.Count - 1];
                byte[] finalPartyLogo = (byte[])finalPartyRow["partij_logo"];
                string finalPartyName = finalPartyRow["naam"].ToString();
                string finalPartyInfo = finalPartyRow["info_tekst"].ToString();
                string finalPartyUrl = finalPartyRow["url"].ToString();

                ImageBrush finalImageBrush = new ImageBrush();
                BitmapImage finalBitmapImage = new BitmapImage();
                finalBitmapImage.BeginInit();
                finalBitmapImage.StreamSource = new MemoryStream(finalPartyLogo);
                finalBitmapImage.EndInit();
                finalImageBrush.ImageSource = finalBitmapImage;

                Rectangle finalRectangle = new Rectangle();
                finalRectangle.Fill = finalImageBrush;
                finalRectangle.Margin = new Thickness(4);
                Grid.SetRow(finalRectangle, 6);
                Grid.SetColumn(finalRectangle, 2);
                gridPartijen.Children.Add(finalRectangle);

                finalRectangle.MouseLeftButtonDown += (sender, e) =>
                {
                    MessageBoxResult result = MessageBox.Show($"{finalPartyName}, {finalPartyInfo} Wil je door naar de website?", "Partij Info", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(finalPartyUrl);
                    }
                };
            }
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

        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameCreate.Text;
            string password = txtPasswordCreate.Password.ToString();
            string email = txtEmailCreate.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))
            {
                System.Windows.MessageBox.Show("Fill all three boxes.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!IsValidEmail(email))
            {
                System.Windows.MessageBox.Show("Use an existing email.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // All validations passed, proceed with registration.
            DatabaseHandler dbHandler = new DatabaseHandler();
            dbHandler.RegisterUser(username, password, email);

            // Optionally, you can show a message to indicate successful registration.
            System.Windows.MessageBox.Show("User registered successfully.");

            txtUsernameCreate.Text = "";
            txtPasswordCreate.Password = "";
            txtEmailCreate.Text = "";

            // Add the following lines to hide the SignUp controls and show the MainWindow controls
            SignUp.Visibility = Visibility.Hidden;
            MainWindowBorder.Visibility = Visibility.Visible;
        }

        private bool IsValidEmail(string email)
        {
            // Basic email format validation using a regular expression.
            // You might want to use a more sophisticated validation method in a real application.
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");
        }



        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            string username = txtUsername.Text;
            string password = txtPassword.Password.ToString();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                System.Windows.MessageBox.Show("Fill in both username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DatabaseHandler dbHandler = new DatabaseHandler();
            DataTable userInfo = dbHandler.GetUserInfo(username);

            if (userInfo.Rows.Count == 0)
            {
                System.Windows.MessageBox.Show("Wrong Username or Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string storedPassword = userInfo.Rows[0]["password"].ToString();

            if (password != storedPassword)
            {
                System.Windows.MessageBox.Show("Wrong Password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Successfully logged in. You can add your code for what happens after login here.
            // For example, navigate to the main window or show some user-specific content.
            System.Windows.MessageBox.Show("Login successful!");

            loggedInUsername = username;

            // Clear the textboxes
            txtUsername.Text = "";
            txtPassword.Password = "";

            byte[] profileImage = null;
            object profileImageObject = userInfo.Rows[0]["profile_image"];

            if (profileImageObject != DBNull.Value)
            {
                profileImage = (byte[])profileImageObject;
            }

            // Optionally, update the profile image in the UI
            if (profileImage != null && profileImage.Length > 0)
            {
                using (MemoryStream stream = new MemoryStream(profileImage))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();

                    ImageBrush profileBrush = new ImageBrush();
                    profileBrush.ImageSource = image;

                    // Assuming ProfileRectangle is already defined
                    ProfileRectangle.Fill = profileBrush;
                }
            }
            else
            {
                ImageBrush ProfileImage = new ImageBrush();
                ProfileImage.ImageSource = new BitmapImage(new Uri("images/profileimage.png", UriKind.Relative));


                ProfileRectangle.Fill = ProfileImage; // Make sure ProfileImage is defined in your code

            }

            MainWindowBorder.Visibility = Visibility.Hidden;
            LoggedIn.Visibility = Visibility.Visible;


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

        private void startClick(object sender, RoutedEventArgs e)
        {
            DatabaseHandler dbHandler = new DatabaseHandler();

            if (!string.IsNullOrEmpty(loggedInUsername))
            {
                Keuze screen = new Keuze();
                screen.Show();
            }
            else
            {
                MessageBox.Show("You need to be logged in.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void btnUploadImage_Click(object sender, RoutedEventArgs e)
{
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files |*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;

                // Convert the image to a byte array
                byte[] imageData = File.ReadAllBytes(imagePath);

                // Update the profile image in the database
                DatabaseHandler dbHandler = new DatabaseHandler();
                dbHandler.UpdateProfileImage(loggedInUsername, imageData);

                // Optionally, update the profile image in the UI
                ImageBrush newProfileImage = new ImageBrush();
                newProfileImage.ImageSource = new BitmapImage(new Uri(imagePath));
                ProfileRectangle.Fill = newProfileImage;
            }
        }
    }
}
