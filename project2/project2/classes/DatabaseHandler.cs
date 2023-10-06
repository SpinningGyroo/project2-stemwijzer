using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace project2.classes
{
    class DatabaseHandler
    {
        MySqlConnection _connection = new MySqlConnection("Server=localhost;Database=dbstellingen;UID=root;Pwd=;");

        public void RegisterUser(string username, string password, string email)
        {
            try
            {
                _connection.Open();
                MySqlCommand command = new MySqlCommand("INSERT INTO user (username, password, email) VALUES (@username, @password, @email)", _connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@email", email);
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
    }
}
