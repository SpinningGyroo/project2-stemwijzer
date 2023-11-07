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
    using project2.classes;

    namespace project2
    {
        /// <summary>
        /// Interaction logic for Keuze.xaml
        /// </summary>
        public partial class Keuze : Window
        {
        private DatabaseHandler dbHandler = new DatabaseHandler();
        private int currentStatementId = 1;
        private Dictionary<string, int> partyValues = new Dictionary<string, int>();
        private Dictionary<string, int> previousPartyValues;

        public Keuze()
        {
            InitializeComponent();
            LoadStatement(currentStatementId);

            List<string> partyNames = dbHandler.GetAllPartyNames();

            partyValues = new Dictionary<string, int>();
            previousPartyValues = new Dictionary<string, int>();

            foreach (string party in partyNames)
            {
                partyValues[party] = 160;
                previousPartyValues[party] = 160;
            }
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
                    // Handle case where no statement was found with the given ID.
                    gridKeuze.Visibility = Visibility.Hidden;
                    gridConfirmed.Visibility = Visibility.Visible;
                }
            }

            private void ShowScores()
            {
                StringBuilder scoreInfo = new StringBuilder("Party Scores:\n");

                foreach (var party in partyValues)
                {
                    scoreInfo.AppendLine($"{party.Key}: {party.Value}");
                }

                MessageBox.Show(scoreInfo.ToString(), "Current Party Scores");
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

        private void backAction(object sender, MouseButtonEventArgs e)
        {
            if (currentStatementId > 1)
            {
                // Create a copy of the party names
                List<string> partyNames = new List<string>(partyValues.Keys);

                // Revert partyValues to previousPartyValues
                foreach (var party in partyNames)
                {
                    partyValues[party] = previousPartyValues[party];
                }

                currentStatementId--;
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

            // Update both partyValues and previousPartyValues
            foreach (var adjustment in adjustments)
            {
                previousPartyValues[adjustment.Key] = partyValues[adjustment.Key];
                partyValues[adjustment.Key] += adjustment.Value;
            }

            ShowScores();
        }

        private void mainMenubtn(object sender, MouseButtonEventArgs e)
            {
                this.Close();
            }
        }
    }