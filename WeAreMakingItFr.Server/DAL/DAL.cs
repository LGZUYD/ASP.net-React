using Azure.Messaging;
using Microsoft.Data.SqlClient;
using PleaseAPI.Models;

namespace PleaseAPI.DAL
{
    static public class DAL
    {
        private static readonly string connString = "Data Source=LUCAS;Initial Catalog=Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public static class MessageDAL
        {
            static List<Message> allMessages = new List<Message>();
            public static List<Message> GetMessages()
            {
                allMessages.Clear();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    string query = "Select * from [Messages]";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string messageContent = reader.GetString(1);
                                Message msgToAdd = new Message(id, messageContent);
                                allMessages.Add(msgToAdd);
                            }
                        }
                    }
                }
                return allMessages;
            }
            public static void CreateNewPost(Message MessageToInsert)
            {                
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    string query = "INSERT INTO [Messages](MessageContent) VALUES(@MessageContent)";

                    using (SqlCommand dbCommand = new SqlCommand(query, connection))
                    {
                        dbCommand.Parameters.AddWithValue("@MessageContent", MessageToInsert.MessageContent);
                        dbCommand.ExecuteNonQuery();
                    }
                }

            }
            public static void UpdatePost(Message MessageToUpdate) //UpdatedMessage beter
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    string query = "UPDATE [Messages] SET MessageContent = @MessageContent WHERE Id = @Id";

                    using (SqlCommand dbCommand = new SqlCommand(query, connection))
                    {
                        dbCommand.Parameters.AddWithValue("@MessageContent", MessageToUpdate.MessageContent);
                        dbCommand.Parameters.AddWithValue("@Id", MessageToUpdate.MessageId);
                        dbCommand.ExecuteNonQuery();
                    }
                }

            }




            public static void DeletePost(int idToDelete)
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    string query = $"DELETE FROM [Messages] WHERE Id = @Id;";

                    using (SqlCommand dbCommand = new SqlCommand(query, connection))
                    {
                        dbCommand.Parameters.AddWithValue("@Id", idToDelete);
                        dbCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
