﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace project2.classes
{
    class DatabaseHandler
    {
        MySqlConnection _connection = new MySqlConnection("Server=localhost;Database=dbstellingen;UID=root;Pwd=;");

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
    }
}
