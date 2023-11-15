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

        public partial class Keuze : Window
        {
        private DatabaseHandler dbHandler;
        private int currentStatementId = 1;//currentStatmentId staat op 1, want de stellingen beginnen bij Id 1 en gaan tot Id 16
        private int loggedInUserId;//private int voor de logged in user met Id
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
                if (timePassed >= 60)
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

                    byte[] partyLogoData = dbHandler.GetPartyLogoByName(partyName);// met de GetPartyLogoByName om informatie te krijgen, de naam en partij_logo

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
                        
                        imageBrush.ImageSource = new BitmapImage(new Uri("images/default.png", UriKind.Relative));//een default image als er toevallig uit het niet een error komt of er iets mist
                    }

                    // Calculate percentage
                    double percentage = CalculatePercentage(partyScore, 320); //het percenagte uit rekenen, het maximum is 320, want je start bij 160 en er zijn 16 partijen per 10

                    
                    switch (i)//switch voor de partij score in % met de naam en imageBrush als partij_logo voor de top 3
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

             
            }
            else
            {
                MessageBox.Show("No results found.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);//error handeling voor als er informartie mist
            }
        }

        private double CalculatePercentage(int score, int maxScore)//de score als percentage berekenen
        {
            return (score / (double)maxScore) * 100.0;
        }




        private void backToStart_click(object sender, MouseButtonEventArgs e)//back to start voor de timer, het rest de timer en currentStatementId
        {
            ResetState();//cleared de partij values
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