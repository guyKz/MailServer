using Interfaces;
using NetworkShared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Server
{
    public class ServerDB
    {
        public ServerDB()
        {
            string connetionString;
            //connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nadav\source\repos\Server\Server\ServerDB.mdf;Integrated Security=True";
            connetionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Nadav\OneDrive\Desktop\Server\Server\Server\ServerDB.mdf;Integrated Security=True";
            _sqlConnection = new SqlConnection(connetionString);
            _sqlConnection.Open();
        }

        public void Close()
        {
            _sqlConnection?.Close();
        }

        public bool IsUserAllowed(string username, string password)
        {
            try
            {
                var cmd = new SqlCommand($"SELECT * FROM USERS WHERE Username = @Username", _sqlConnection);
                cmd.Parameters.AddWithValue("@Username", username);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {

                        // You can access the columns by their names or indexes
                        int db_id = reader.GetInt32(reader.GetOrdinal("Id"));
                        string db_username = reader.GetString(reader.GetOrdinal("Username"));
                        string db_password = reader.GetString(reader.GetOrdinal("Password"));

                        if (String.Equals(db_password, password))
                        {
                            return true;
                        }
                    }
                    return false;
                }

            } catch (Exception Ex)
            {
                return false;
            }
        }

        public bool AddUser(string username, string password)
        {
            try
            {
                string insertQuery = "INSERT INTO USERS (Username, Password, INBOX) VALUES (@Username, @Password, @INBOX)";

                using (SqlCommand command = new SqlCommand(insertQuery, _sqlConnection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    command.Parameters.AddWithValue("@INBOX", "");

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                return false;
            } catch (Exception Ex)
            {
                return false;
            }
        }

        public bool Compose(ComposeRequest email)
        {
            try
            {
                string insertQuery = "INSERT INTO Emails (Date, sender, receiver, data, subject) VALUES (@Date, @sender, @receiver, @data, @subject); SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(insertQuery, _sqlConnection))
                {
                    command.Parameters.AddWithValue("@Date", email.Date.ToString());
                    command.Parameters.AddWithValue("@sender", email.from);
                    command.Parameters.AddWithValue("@receiver", email.to) ;
                    command.Parameters.AddWithValue("@data", email.data);
                    command.Parameters.AddWithValue("@subject", email.subject);

                    //int rowsAffected = command.ExecuteNonQuery();

         
                    object result = command.ExecuteScalar();
                    int primaryKeyValue = Convert.ToInt32(result);

                    string? user_inbox = GetInboxByUsername(email.to);
                    if (user_inbox == null)
                    {
                        return false;
                    }

                    if (UpdateInboxByUsername(email.to, $"{user_inbox};{primaryKeyValue}"))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception Ex)
            {
                Console.WriteLine("Unable to write compse due to internal error");
                return false;
            }
        }

        string? GetInboxByUsername(string username)
        {
            string inbox = null;

            string selectQuery = "SELECT INBOX FROM USERS WHERE Username = @Username";

            using (SqlCommand command = new SqlCommand(selectQuery, _sqlConnection))
            {
                command.Parameters.AddWithValue("@Username", username);

                object result = command.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    inbox = result as string;
                }
            }

            return inbox;
        }


        bool UpdateInboxByUsername(string username, string messageToAdd)
        {
            string selectQuery = "SELECT INBOX FROM USERS WHERE Username = @Username";

            using (SqlCommand command = new SqlCommand(selectQuery, _sqlConnection))
            {
                command.Parameters.AddWithValue("@Username", username);

                object result = command.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    string currentInbox = result as string;
                    string updatedInbox = currentInbox + messageToAdd;

                    string updateQuery = "UPDATE USERS SET INBOX = @UpdatedInbox WHERE Username = @Username";

                    using (SqlCommand updateCommand = new SqlCommand(updateQuery, _sqlConnection))
                    {
                        updateCommand.Parameters.AddWithValue("@Username", username);
                        updateCommand.Parameters.AddWithValue("@UpdatedInbox", updatedInbox);

                        int rowsAffected = updateCommand.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
                else
                {
                    return false; // User not found
                }
            }
            
        }

        public List<emailData>? GetAllMails(string username)
        {
            List<emailData> emails = new List<emailData>();

            string? inbox_ref = GetInboxByUsername(username);

            if (inbox_ref == null)
            {
                return null;
            }

            string[] ids = inbox_ref.Split(";");
            foreach (var word in ids)
            {
                bool isNumber = int.TryParse(word, out int id);
                if (!isNumber)
                {
                    continue;
                }

                emails.Add(GetEmailById(id));
                //System.Console.WriteLine($"<{word}>");
            }

            return emails;
        }


        emailData GetEmailById(int emailId)
        {
            emailData email = null;

            //string insertQuery = "INSERT INTO Emails (Date, sender, receiver, data, subject) VALUES (@Date, @sender, @receiver, @data, @subject); SELECT SCOPE_IDENTITY();";

            string selectQuery = "SELECT * FROM Emails WHERE Id = @EmailId";

            using (SqlCommand command = new SqlCommand(selectQuery, _sqlConnection))
            {
                command.Parameters.AddWithValue("@EmailId", emailId);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        email = new emailData
                        {
                            subject = reader["subject"] as string,
                            to = reader["Receiver"] as string,
                        };

                        byte[] v= reader["data"] as byte[];
                        email.atr = Encoding.ASCII.GetString(v);
                        //email.atr = Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(BitConverter.ToString(v).Replace("-", "")));
                        //email.atr = reader["data"] as string;


                    }
                }
            }
            

            return email;
        }

        private SqlConnection? _sqlConnection;
    }
}
