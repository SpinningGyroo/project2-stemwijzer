using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;
using System.Windows;

namespace project2.classes
{
    class DatabaseHandler
    {
        private static MySqlConnection _connection = new MySqlConnection("Server=localhost;Database=dbstellingen;UID=root;Pwd=;");

        public void RegisterUser(string username, string password, string email, byte[] profileImage = null)
        {
            try
            {

                _connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO user (username, password, email, profile_image) VALUES (@username, @password, @email, @profileImage)", _connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@profileImage", profileImage);
                command.ExecuteNonQuery();

                System.Windows.MessageBox.Show("User registered successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public static bool UsernameExists(string username)
        {
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM user WHERE username = @username", _connection);
                command.Parameters.AddWithValue("@username", username);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return false; // Assuming an error means username doesn't exist (for simplicity)
            }
            finally
            {
                _connection.Close();
            }
        }


        public void UpdateProfileImage(string username, byte[] profileImage)
        {
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("UPDATE user SET profile_image = @profileImage WHERE username = @username", _connection);
                command.Parameters.AddWithValue("@profileImage", profileImage);
                command.Parameters.AddWithValue("@username", username);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        public byte[] GetProfileImage(string username)
        {
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT profile_image FROM user WHERE username = @username", _connection);
                command.Parameters.AddWithValue("@username", username);
                byte[] profileImage = (byte[])command.ExecuteScalar();
                return profileImage;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public bool LoginUser(string username, string password)
        {
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT COUNT(*) FROM user WHERE username = @username AND password = @password", _connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public DataTable GetUserInfo(string username)
        {
            DataTable result = new DataTable();
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM user WHERE username = @username", _connection);
                command.Parameters.AddWithValue("@username", username);
                MySqlDataReader reader = command.ExecuteReader();
                result.Load(reader);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public bool HasProfileImage(string username)
        {
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT profile_image FROM user WHERE username = @username", _connection);
                command.Parameters.AddWithValue("@username", username);
                byte[] profileImage = (byte[])command.ExecuteScalar();

                // Check if profileImage is not null and has data
                return profileImage != null && profileImage.Length > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public DataTable GetParties()
        {
            DataTable result = new DataTable();
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT ID, naam, info_tekst, url, partij_logo FROM partijen", _connection);
                MySqlDataReader reader = command.ExecuteReader();
                result.Load(reader);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }


        public DataTable GetStatementById(int id)
        {
            DataTable result = new DataTable();
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM stellingen WHERE id = @id", _connection);
                command.Parameters.AddWithValue("@id", id);
                MySqlDataReader reader = command.ExecuteReader();
                result.Load(reader);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }

        public Dictionary<string, string> GetPartyOpinionsForStatement(int statementID)
        {
            Dictionary<string, string> partyOpinions = new Dictionary<string, string>();

            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand(
                    "SELECT po.Partij, po.Mening " +
                    "FROM party_opinions po " +
                    "INNER JOIN stellingen s ON po.MeningID = s.ID " +
                    "WHERE po.MeningID = @statementID", _connection);
                command.Parameters.AddWithValue("@statementID", statementID);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string party = reader["Partij"].ToString();
                    string opinion = reader["Mening"].ToString();
                    partyOpinions.Add(party, opinion);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            finally
            {
                _connection.Close();
            }

            return partyOpinions;
        }

        public List<string> GetAllPartyNames()
        {
            List<string> partyNames = new List<string>();

            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT naam FROM partijen", _connection);
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string partyName = reader["naam"].ToString();
                    partyNames.Add(partyName);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            finally
            {
                _connection.Close();
            }

            return partyNames;
        }



        public int GetUserIdByUsername(string username)
        {
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT id FROM user WHERE username = @username", _connection);
                command.Parameters.AddWithValue("@username", username);
                object result = command.ExecuteScalar();

                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return 0;
            }
            finally
            {
                _connection.Close();
            }
        }
        public void SaveUserScores(int userId, Dictionary<string, int> partyValues)
        {
            try
            {
                _connection.Open();

                // Check if the user already has a row in the user_scores table
                MySqlCommand checkCommand = new MySqlCommand("SELECT * FROM user_scores WHERE user_id = @user_id", _connection);
                checkCommand.Parameters.AddWithValue("@user_id", userId);
                object existingRow = checkCommand.ExecuteScalar();

                if (existingRow != null)
                {
                    // User already has a row, update the existing row
                    MySqlCommand updateCommand = new MySqlCommand(
                        "UPDATE user_scores SET " +
                        "VVD = @VVD, PVV = @PVV, CDA = @CDA, D66 = @D66, " +
                        "GroenLinksPvda = @GroenLinksPvda, SP = @SP, Splinter = @Splinter, " +
                        "SGP = @SGP, FVD = @FVD, JA21 = @JA21, Volt = @Volt, " +
                        "Piratenpartij = @Piratenpartij, LP = @LP, BBB = @BBB, " +
                        "Partij_voor_de_Dieren = @Partij_voor_de_Dieren, ChristenUnie = @ChristenUnie " +
                        "WHERE user_id = @user_id", _connection);

                    SetPartyValueParameters(updateCommand, partyValues);
                    updateCommand.Parameters.AddWithValue("@user_id", userId);

                    int rowsUpdated = updateCommand.ExecuteNonQuery();

                    Console.WriteLine("Rows Updated: " + rowsUpdated);
                }
                else
                {
                    // User doesn't have a row, insert a new row
                    MySqlCommand insertCommand = new MySqlCommand(
                        "INSERT INTO user_scores (user_id, VVD, PVV, CDA, D66, " +
                        "GroenLinksPvda, SP, Splinter, SGP, FVD, JA21, Volt, " +
                        "Piratenpartij, LP, BBB, Partij_voor_de_Dieren, ChristenUnie) " +
                        "VALUES (@user_id, @VVD, @PVV, @CDA, @D66, @GroenLinksPvda, @SP, @Splinter, " +
                        "@SGP, @FVD, @JA21, @Volt, @Piratenpartij, @LP, @BBB, @Partij_voor_de_Dieren, @ChristenUnie)", _connection);

                    SetPartyValueParameters(insertCommand, partyValues);
                    insertCommand.Parameters.AddWithValue("@user_id", userId);

                    int rowsInserted = insertCommand.ExecuteNonQuery();

                    Console.WriteLine("Rows Inserted: " + rowsInserted);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        private void SetPartyValueParameters(MySqlCommand command, Dictionary<string, int> partyValues)
        {
            foreach (var partyValue in partyValues)
            {
                command.Parameters.AddWithValue("@" + partyValue.Key, partyValue.Value);
            }

            command.Parameters.AddWithValue("@Partij_voor_de_Dieren", partyValues["Partij voor de Dieren"]);
        }

        public DataTable GetTopPartyScoresForUser(int userId)
        {
            DataTable result = new DataTable();
            try
            {
                _connection.Open();

                MySqlCommand command = new MySqlCommand(
                    "SELECT user_id, party_name, party_score " +
                    "FROM (" +
                    "    SELECT user_id, 'VVD' AS party_name, VVD AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'PVV' AS party_name, PVV AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'CDA' AS party_name, CDA AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'D66' AS party_name, D66 AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'GroenLinksPvda' AS party_name, GroenLinksPvda AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'SP' AS party_name, SP AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'Splinter' AS party_name, Splinter AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'SGP' AS party_name, SGP AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'FVD' AS party_name, FVD AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'JA21' AS party_name, JA21 AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'Volt' AS party_name, Volt AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'Piratenpartij' AS party_name, Piratenpartij AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'LP' AS party_name, LP AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'BBB' AS party_name, BBB AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'Partij_voor_de_Dieren' AS party_name, Partij_voor_de_Dieren AS party_score FROM user_scores " +
                    "    UNION " +
                    "    SELECT user_id, 'ChristenUnie' AS party_name, ChristenUnie AS party_score FROM user_scores " +
                    ") AS all_parties " +
                    "WHERE user_id = @userId AND party_score IS NOT NULL " +
                    "ORDER BY party_score DESC " +
                    "LIMIT 3", _connection);

                command.Parameters.AddWithValue("@userId", userId);

                MySqlDataReader reader = command.ExecuteReader();
                result.Load(reader);
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            finally
            {
                _connection.Close();
            }
            return result;
        }


    }
}
