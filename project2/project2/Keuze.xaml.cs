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

        public Keuze()
        {
            InitializeComponent();
            LoadStatement(currentStatementId);
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
            }
        }

        private void oneens_Click(object sender, RoutedEventArgs e)
        {
            if (currentStatementId < 16)
            {
                currentStatementId++;
                LoadStatement(currentStatementId);
            }
        }

        private void eens_Click(object sender, RoutedEventArgs e)
        {
            if (currentStatementId < 16)
            {
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

        private void mainMenubtn(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}