using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Threading;
using project2.classes;
using System.IO;
namespace project2
{
        /// <summary>
        /// Interaction logic for Keuze.xaml
        /// </summary>
        public partial class Keuze : Window
        {
        private DatabaseHandler dbHandler;
        private int currentStatementId = 1;
        private int loggedInUserId;
        private Dictionary<string, int> partyValues = new Dictionary<string, int>();
        private DispatcherTimer timer;
        int timePassed = 0;

        public Keuze(int loggedInUserId)
        {
            InitializeComponent();
            dbHandler = new DatabaseHandler();
            this.loggedInUserId = loggedInUserId;
            LoadStatement(currentStatementId);

            List<string> partyNames = dbHandler.GetAllPartyNames();

            partyValues = new Dictionary<string, int>();

            foreach (string party in partyNames)
            {
                partyValues[party] = 160;
            }

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timePassed++;
        }

        private void LoadStatement(int statementId)
            {
                DataTable statementData = dbHandler.GetStatementById(statementId);

            if (statementData.Rows.Count > 0)
            {
                DataRow statementRow = statementData.Rows[0];
                stellingTitel.Text = statementRow["titel"].ToString();
                stellingTB.Text = statementRow["stelling"].ToString();
                pageCounter.Text = $"{currentStatementId}/16";
            }
            else
            {
                if (timePassed >= 90)
                { 
                    timer.Stop();
                    gridKeuze.Visibility = Visibility.Hidden;
                    gridConfirmed.Visibility = Visibility.Visible;
                }
                else
                {
                    timer.Stop();
                    gridKeuze.Visibility = Visibility.Hidden;
                    gridTooFast.Visibility = Visibility.Visible;
                }
            }

        }

            private void oneens_Click(object sender, RoutedEventArgs e)
            {
                if (currentStatementId < 17)
                {
                    AdjustPartyValues("oneens");
                    currentStatementId++;
                    LoadStatement(currentStatementId);
                }
            }

            private void eens_Click(object sender, RoutedEventArgs e)
            {
                if (currentStatementId < 17)
                {
                    AdjustPartyValues("eens");
                    currentStatementId++;
                    LoadStatement(currentStatementId);
                }
            }

        private void AdjustPartyValues(string opinion)
        {
            Dictionary<string, int> adjustments = new Dictionary<string, int>();
            var partyOpinions = dbHandler.GetPartyOpinionsForStatement(currentStatementId);

            if (partyOpinions != null)
            {
                foreach (var party in partyValues.Keys)
                {
                    if (partyOpinions.ContainsKey(party))
                    {
                        if ((opinion == "eens" && partyOpinions[party] == "eens") ||
                            (opinion == "oneens" && partyOpinions[party] == "oneens"))
                        {
                            adjustments[party] = 10;
                        }
                        else
                        {
                            adjustments[party] = -10;
                        }
                    }
                    else
                    {
                        adjustments[party] = 0;
                    }
                }
            }

            foreach (var adjustment in adjustments)
            {
                partyValues[adjustment.Key] += adjustment.Value;
            }
        }

        private void mainMenubtn(object sender, MouseButtonEventArgs e)
            {
                this.Close();
            }

        private void resultaat_click(object sender, RoutedEventArgs e)
        {
            gridConfirmed.Visibility = Visibility.Hidden;
            gridResult.Visibility = Visibility.Visible;
            gridYippie.Visibility = Visibility.Visible;
            dbHandler.SaveUserScores(loggedInUserId, partyValues);

            DataTable resultData = dbHandler.GetTopPartyScoresForUser(loggedInUserId);

            if (resultData.Rows.Count > 0)
            {
                DataView dv = resultData.DefaultView;
                dv.Sort = "party_score DESC";
                DataTable sortedResult = dv.ToTable();

                for (int i = 0; i < Math.Min(sortedResult.Rows.Count, 3); i++)
                {
                    DataRow row = sortedResult.Rows[i];
                    string partyName = row["party_name"].ToString();
                    int partyScore = Convert.ToInt32(row["party_score"]);

                    // Use the GetPartyLogoByName method to retrieve partij_logo data
                    byte[] partyLogoData = dbHandler.GetPartyLogoByName(partyName);

                    ImageBrush imageBrush = new ImageBrush();
                    if (partyLogoData != null && partyLogoData.Length > 0)
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = new MemoryStream(partyLogoData);
                        bitmapImage.EndInit();
                        imageBrush.ImageSource = bitmapImage;
                    }
                    else
                    {
                        // Provide a default image source if partij_logo data is not available
                        imageBrush.ImageSource = new BitmapImage(new Uri("images/default.png", UriKind.Relative));
                    }

                    // Calculate percentage
                    double percentage = CalculatePercentage(partyScore, 320); // Assuming 320 is the maximum score

                    // Set the image brush and percentage based on the score
                    switch (i)
                    {
                        case 0:
                            topScore.Value = percentage;
                            topPartyName.Text = $"{partyName}:  {percentage}%";
                            topCircle.Fill = imageBrush;
                            break;
                        case 1:
                            midScore.Value = percentage;
                            midPartyName.Text = $"{partyName}:  {percentage}%)";
                            midCircle.Fill = imageBrush;
                            break;
                        case 2:
                            bottomScore.Value = percentage;
                            bottomPartyName.Text = $"{partyName}:  {percentage}%";
                            bottomCircle.Fill = imageBrush;
                            break;
                    }
                }

                // Additional logic if you want to show or process other information
            }
            else
            {
                MessageBox.Show("No results found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private double CalculatePercentage(int score, int maxScore)
        {
            return (score / (double)maxScore) * 100.0;
        }




        private void backToStart_click(object sender, MouseButtonEventArgs e)
        {
            ResetState();
            currentStatementId = 1;
            timePassed = 0;
            timer.Start();
            LoadStatement(currentStatementId);
        }
        private void ResetState()
        {
            partyValues.Clear();

            List<string> partyNames = dbHandler.GetAllPartyNames();

            foreach (string party in partyNames)
            {
                partyValues[party] = 160;
            }

            gridTooFast.Visibility = Visibility.Hidden;
            gridKeuze.Visibility = Visibility.Visible;
        }

    }
    }