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

    public partial class MainWindow : Window
    {
        
        private string loggedInUsername; // de private logged in user int, dus dan weet de applicatie wie er is ingelogd 
        Rectangle ProfileRectangle; // de default profile image, voor een user die nog geen profile image heeft
        public MainWindow()
        {
            InitializeComponent();
            partycreator(); // de partycreator maakt de partijen logo's aan in "gridPartijen"
            LoggedIn.Visibility = Visibility.Hidden; // de logged in grid hidden
            SignUp.Visibility = Visibility.Hidden; //sign up grid wordt hidden

            ImageBrush ProfileImage = new ImageBrush(); // nieuwe brush met de deafult profile image
            ProfileImage.ImageSource = new BitmapImage(new Uri("images/profileimage.png", UriKind.Relative)); 

            ProfileRectangle = new Rectangle(); // een rectangle wordt aan gemaakt met de default profile image als brush
            ProfileRectangle.Margin = new Thickness(4);
            ProfileRectangle.Fill = ProfileImage;
            Grid.SetRow(ProfileRectangle, 1);
            Grid.SetColumn(ProfileRectangle, 1);
            Grid.SetColumnSpan(ProfileRectangle, 2);
            Grid.SetRowSpan(ProfileRectangle, 4);

            gridLoggedIn.Children.Add(ProfileRectangle);// de rectangle wordt toegevoegd in de logged in grid "gridLoggedIn"
        }


        private void partycreator() // de partycreator voor de partij logo's
        {
            DatabaseHandler dbHandler = new DatabaseHandler();// connectie met de database via GetParties, voor de logo's in blob
            DataTable partiesTable = dbHandler.GetParties();

            int row = 1;// hoeveel rows en colums, dit bepaald de positie van de logo's
            int col = 1;

            for (int i = 0; i < partiesTable.Rows.Count - 1; i++) // een for loop dat gaat door alle partijen heen
            {
                DataRow partyRow = partiesTable.Rows[i];// alle ints worden aangemaakt naar string via de databasehandler
                int partyId = Convert.ToInt32(partyRow["ID"]);
                byte[] partyLogo = (byte[])partyRow["partij_logo"];
                string partyName = partyRow["naam"].ToString();
                string partyInfo = partyRow["info_tekst"].ToString();
                string partyUrl = partyRow["url"].ToString();

                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(partyLogo);// bitimage stream, Hij streamt dan de image uit de memory als blob
                bitmapImage.EndInit();
                imageBrush.ImageSource = bitmapImage;

                Rectangle rectangle = new Rectangle();
                rectangle.Fill = imageBrush;
                rectangle.Margin = new Thickness(4);
                Grid.SetRow(rectangle, row);
                Grid.SetColumn(rectangle, col);

                rectangle.MouseLeftButtonDown += (sender, e) => // de mousdown function op de logo's, alles komt uit de database via  GetParties
                {
                    MessageBoxResult result = MessageBox.Show($"{partyName}, {partyInfo} Wil je door naar de website?", "Partij Info", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        System.Diagnostics.Process.Start(partyUrl); // hij start de url voor de website
                    }
                };

                gridPartijen.Children.Add(rectangle);// alle log's toevegen aan de gridPartijen

                col++; //grid positie updaten

                if (col > 3)
                {
                    col = 1;
                    row++;
                }
            }

            
            if (partiesTable.Rows.Count > 0)// we doen alles nog een keer alleen dan voor de final logo, zodat hij netjes onderaan zit
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

            DatabaseHandler dbHandler = new DatabaseHandler();// de user id van de username ophalen uit de database via GetUserIdByUsername
            int userId = dbHandler.GetUserIdByUsername(loggedInUsername);
            History screen = new History(userId);// hij opend de history window met de userId int, deze is gecombineerd met GetUserIdByUsername en de private logged in user int
            screen.Show();
        }

        private void signupClick(object sender, RoutedEventArgs e)
        {
            MainWindowBorder.Visibility = Visibility.Hidden;// de logdin sectie hidden maken, de grid staat in een border, daarom maken we de grid hidden
            SignUp.Visibility = Visibility.Visible;// nu is de sign up grid visible 

        }

        private void btnCreateUser_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsernameCreate.Text;//username uit de textbox naar string
            string password = txtPasswordCreate.Password.ToString();// password naar string, we maken van een SecureString een normale string, vanwege .Password.
            string email = txtEmailCreate.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(email))//als alle boxes leeg zijn
            {
                System.Windows.MessageBox.Show("Fill all three boxes.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (!IsValidEmail(email))//als het een geldige email is
            {
                System.Windows.MessageBox.Show("Use an existing email.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DatabaseHandler dbHandler = new DatabaseHandler();

            if (DatabaseHandler.UsernameExists(username))//als de username bestaat en dus UsernameExists waar is
            {
                System.Windows.MessageBox.Show("Username already exists. Please choose a different username.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                txtUsernameCreate.Text = "";
                txtPasswordCreate.Password = "";
                txtEmailCreate.Text = "";
            }

            if (!DatabaseHandler.UsernameExists(username))// als de username niet bestaat, dus als UsernameExists niet waar is
            {

                dbHandler.RegisterUser(username, password, email);

                txtUsernameCreate.Text = "";
                txtPasswordCreate.Password = "";
                txtEmailCreate.Text = "";
                SignUp.Visibility = Visibility.Hidden;
                MainWindowBorder.Visibility = Visibility.Visible;
            }

        }

        private bool IsValidEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$");//checked voor de juiste characters and letter
        }



        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            string username = txtUsername.Text;//username naar string
            string password = txtPassword.Password.ToString();//password van SecureString naar normale string

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))//als de boxes leeg zijn
            {
                System.Windows.MessageBox.Show("Fill in both username and password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DatabaseHandler dbHandler = new DatabaseHandler();
            DataTable userInfo = dbHandler.GetUserInfo(username);//de user info ophalen gecombineerd met username, via GetUserInfo

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

        private void GoBackClick(object sender, RoutedEventArgs e)//de Sign Up grid hidden en de loggin weer visible maken
        {
            SignUp.Visibility = Visibility.Hidden;
            MainWindowBorder.Visibility = Visibility.Visible;
        }

        private void logoutClick(object sender, RoutedEventArgs e)//uitloggen van je user, logged in grid hidden en loggin weer visible
        {
            LoggedIn.Visibility = Visibility.Hidden;
            MainWindowBorder.Visibility = Visibility.Visible;
            loggedInUsername = null;// LoggedInUsername is weer null, zodat je de stemWzijer niet weer opnieuw kunt doen als je loggedout bent
        }

        private void startClick(object sender, RoutedEventArgs e)
        {
            DatabaseHandler dbHandler = new DatabaseHandler();

            if (!string.IsNullOrEmpty(loggedInUsername))//als LoggedInUsername niet null or empty is
            {
                int userId = dbHandler.GetUserIdByUsername(loggedInUsername);// de user id van de username ophalen uit de database via GetUserIdByUsername
                Keuze screen = new Keuze(userId);
                screen.Show();
            }
            else//is LoggedInUsername wel null of empty 
            {
                MessageBox.Show("You need to be logged in.", "Access Denied", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void btnUploadImage_Click(object sender, RoutedEventArgs e)//upload de image van je eigen systeem
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();// maakt een nieuwe OpenFilelog aan
            openFileDialog.Filter = "Image files |*.jpg;*.jpeg;*.gif;*.bmp";// een filter zodat je alleen maar jpg's, jpeg, gif's en bmp's kan pakken

            if (openFileDialog.ShowDialog() == true)//als OpenFilelog show waar is
            {
                string imagePath = openFileDialog.FileName;

                
                byte[] imageData = File.ReadAllBytes(imagePath);//Convert de image naar byte array

                
                DatabaseHandler dbHandler = new DatabaseHandler();
                dbHandler.UpdateProfileImage(loggedInUsername, imageData);//Update de profile image in the database


                ImageBrush newProfileImage = new ImageBrush();
                newProfileImage.ImageSource = new BitmapImage(new Uri(imagePath));//imagePath string als profile image brush
                ProfileRectangle.Fill = newProfileImage;
            }
        }
    }
}
