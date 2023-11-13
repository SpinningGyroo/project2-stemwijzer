using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using project2.classes;

namespace project2
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History : Window
    {
        private DatabaseHandler dbHandler;
        private int loggedInUserId;
        public History(int loggedInUserId)
        {
            InitializeComponent();
            dbHandler = new DatabaseHandler();
            this.loggedInUserId = loggedInUserId;
            partycreator();
        }

        private void partycreator()
        {
            DatabaseHandler dbHandler = new DatabaseHandler();
            DataTable partiesTable = dbHandler.GetParties();
            int index = 0;

            for (int row = 1; row < 9; row += 2)
            {
                for (int col = 2; col < 9; col += 2)
                {
                    Rectangle rectangle = new Rectangle();

                    if (index < partiesTable.Rows.Count)
                    {
                        DataRow partyRow = partiesTable.Rows[index];
                        byte[] partyLogo = (byte[])partyRow["partij_logo"];
                        string partyName = partyRow["naam"].ToString();

                        ImageBrush imageBrush = new ImageBrush();
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new MemoryStream(partyLogo);
                        bitmapImage.EndInit();
                        imageBrush.ImageSource = bitmapImage;

                        rectangle.Fill = imageBrush;
                        rectangle.MouseDown += (sender, e) => historySubShow(partyName, 0, partyLogo); // Assuming score is not relevant here
                        rectangle.Margin = new Thickness(4);
                        Grid.SetRow(rectangle, row);
                        Grid.SetColumn(rectangle, col);
                        redGrid.Children.Add(rectangle);
                    }

                    index++;
                }
            }

            ImageBrush crossImageBrush = new ImageBrush();
            crossImageBrush.ImageSource = new BitmapImage(new Uri("images/cross.png", UriKind.Relative));

            Rectangle crossRectangle = new Rectangle();
            crossRectangle.Fill = crossImageBrush;
            crossRectangle.MouseDown += (sender, e) => historySubShow("Cross", 0, null); // You can adjust the parameters accordingly
            Grid.SetRow(crossRectangle, 0);
            Grid.SetColumn(crossRectangle, 4);
            historySub.Children.Add(crossRectangle);
            historySub.Visibility = Visibility.Hidden;
        }

        private void historySubShow(string selectedPartyName, int selectedPartyScore, byte[] selectedPartyLogoData)
        {
            if (historySub.Visibility == Visibility.Visible)
            {
                historySub.Visibility = Visibility.Hidden;
            }
            else
            {
                historySub.Visibility = Visibility.Visible;

                ImageBrush imageBrush = new ImageBrush();
                if (selectedPartyLogoData != null && selectedPartyLogoData.Length > 0)
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(selectedPartyLogoData);
                    bitmapImage.EndInit();
                    imageBrush.ImageSource = bitmapImage;
                }
                else
                {
                    // Provide a default image source if partij_logo data is not available
                    imageBrush.ImageSource = new BitmapImage(new Uri("images/default.png", UriKind.Relative));
                }

                // Retrieve the score from user_scores table
                int partyScoreFromUserScores = dbHandler.GetPartyScoreForUser(loggedInUserId, selectedPartyName);

                double percentage = CalculatePercentage(partyScoreFromUserScores, 320); // Assuming 320 is the maximum score

                HistoryScore.Value = percentage;
                HistoryPartyName.Text = $"{selectedPartyName}: {percentage}%";
                scoreCircle.Fill = imageBrush;
            }
        }

        private double CalculatePercentage(int score, int maxScore)
        {
            return (score / (double)maxScore) * 100.0;
        }

        private void historyMainMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

