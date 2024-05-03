using Azure.Messaging;
using Microsoft.Data.SqlClient;
using PleaseAPI.Models;
using System.Data.Common;
using WeAreMakingItFr.Server.Models;

namespace PleaseAPI.DAL
{
    public class DAL
    {
        private static readonly string connString = "Data Source=LUCAS;Initial Catalog=Project;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

        public class MessageDAL
        {
            private List<Message> allMessages = new List<Message>();
            public List<Message> GetMessages()
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
                                CommentsDAL newCommentDAL = new CommentsDAL();
                                List<Comment> allComments = newCommentDAL.getCommentsByMessageId(id);

                                Message msgToAdd = new Message(id, messageContent, allComments);
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

        public class CommentsDAL
        {
            private List<Comment> allComments = new List<Comment>();
            public List<Comment> getCommentsByMessageId(int messageId)
            {
                allComments.Clear();
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    string query = $"Select * from [Comments] where messageId = @messageId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@messageId", messageId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                int id = reader.GetInt32(0);
                                string commentContent = reader.GetString(2);
                                Comment commentToAdd = new Comment(id, messageId, commentContent);
                                //System.Diagnostics.Debug.WriteLine(messageId);
                                //System.Diagnostics.Debug.WriteLine("------------------");
                                //System.Diagnostics.Debug.WriteLine(commentToAdd.CommentContent);
                                //System.Diagnostics.Debug.WriteLine(commentToAdd.MessageId);
                                //System.Diagnostics.Debug.WriteLine(commentToAdd.CommentId);
                                //System.Diagnostics.Debug.WriteLine("==================");
                                allComments.Add(commentToAdd);
                            }
                        }
                    }
                }
                return allComments;
            }

            public static void CreateNewComment(Comment CommentToInsert)
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    string query = "INSERT INTO [Comments](messageId, commentContent) VALUES(@messageId, @commentContent)";

                    using (SqlCommand dbCommand = new SqlCommand(query, connection))
                    {
                        dbCommand.Parameters.AddWithValue("@messageId", CommentToInsert.MessageId);
                        dbCommand.Parameters.AddWithValue("@commentContent", CommentToInsert.CommentContent);
                        dbCommand.ExecuteNonQuery();
                    }
                }

            }

        }

        public class UserDAL
        {
            public static void CreateNewUser(User userToCreate)
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    string query = "INSERT INTO [Users](userName, userPassword) VALUES(@userName, @userPassword)";

                    using (SqlCommand dbCommand = new SqlCommand(query, connection))
                    {
                        dbCommand.Parameters.AddWithValue("@userName", userToCreate.Username);
                        dbCommand.Parameters.AddWithValue("@userPassword", userToCreate.Password);
                        dbCommand.ExecuteNonQuery();
                    }
                }
            }
            public static bool CheckUserAvailable(User userToCheck)
            {

                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    string query = $"Select * from [Users] where userName = @userName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userName", userToCheck.Username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            public static bool CheckUserLogin(User userToCheck)
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    connection.Open();

                    string query = $"Select * from [Users] where userName = @userName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@userName", userToCheck.Username);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    if (reader[2].ToString() == userToCheck.Password)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }
                            else
                            {                
                                return false;
                            }
                        }
                    }
                }                
                return false;
            }


        }
    }
}


