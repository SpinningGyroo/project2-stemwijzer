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

        public Keuze()
        {
            InitializeComponent();
            LoadStatement(currentStatementId);

            List<string> partyNames = dbHandler.GetAllPartyNames();

            foreach (string party in partyNames)
            {
                partyValues[party] = 160; // Set initial value of 160 for each party
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
                if (currentStatementId < 16)
                {
                    AdjustPartyValues("oneens");
                    currentStatementId++;
                    LoadStatement(currentStatementId);
                }
            }

            private void eens_Click(object sender, RoutedEventArgs e)
            {
                if (currentStatementId < 16)
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
                    currentStatementId--;
                    LoadStatement(currentStatementId);
                }
            }

            private void AdjustPartyValues(string opinion)
            {
            Dictionary<string, int> adjustments = new Dictionary<string, int>();
            var partyOpinions = dbHandler.GetPartyOpinionsForStatement(currentStatementId);

            if (partyOpinions != null) // Check if party opinions are available for the statement
            {
                foreach (var party in partyValues.Keys)
                {
                    // Check if the party exists in the opinions for the current statement
                    if (partyOpinions.ContainsKey(party))
                    {
                        if ((opinion == "eens" && partyOpinions[party] == "eens") ||
                            (opinion == "oneens" && partyOpinions[party] == "eens"))
                        {
                            adjustments[party] = 10;  // Add 10 to parties with the same opinion
                        }
                        else
                        {
                            adjustments[party] = -10;  // Subtract 10 from parties with the opposite opinion
                        }
                    }
                    else
                    {
                        adjustments[party] = 0; // No opinion found, set adjustment to 0 or handle as needed
                    }
                }
            }

            // Apply adjustments to the party values
            foreach (var adjustment in adjustments)
            {
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