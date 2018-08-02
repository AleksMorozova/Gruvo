using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Gruvo.Models;

namespace Gruvo.DAL
{
    class MSSQLPostDAO : IPostDAO
    {
        private SqlConnection connection;

        public MSSQLPostDAO(SqlConnection connection) => this.connection = connection;

        public void AddPost(long userId, string message, DateTime postDateTime)
        {
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"insert into posts (userid,message,postdate) values (@userid, @message,@postdate)";
                command.Parameters.Add("@userid", SqlDbType.BigInt);
                command.Parameters["@userid"].Value = userId;

                command.Parameters.Add("@message", SqlDbType.VarChar);
                command.Parameters["@message"].Value = message;

                command.Parameters.Add("@postdate", SqlDbType.Date);
                command.Parameters["@postdate"].Value = postDateTime;

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
}
        
        public void DeletePost(long id)
        {
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"delete from posts where postid = @postid ";
                command.Parameters.Add("@postid", SqlDbType.BigInt);
                command.Parameters["@postid"].Value = id;

                command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public Post GetPost(long id)
        {
            Post post = null;
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"select * from posts where postid = @id ";
                command.Parameters.Add("@id", SqlDbType.BigInt);
                command.Parameters["@id"].Value = id;

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        post = new Post((long)dataReader["Postid"], (long)dataReader["userid"], (string)dataReader["message"], (DateTime)dataReader["postdate"]);
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
            return post;
        }

        public IEnumerable<Post> GetPostsForUser(long id)
        {
            List<Post> list = new List<Post>();
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"select * from posts where userid in (select SubscribedId from subscriptions where SubscriberId = @id)  ";
                command.Parameters.Add("@id", SqlDbType.BigInt);
                command.Parameters["@id"].Value = id;

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(new Post((long)dataReader["Postid"], (long)dataReader["userid"], (string)dataReader["message"], (DateTime)dataReader["postdate"]));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        public IEnumerable<Post> GetUserPosts(long id)
        {
            List<Post> list = new List<Post>();
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"select * from posts where userid = @id";
                command.Parameters.Add("@id", SqlDbType.BigInt);
                command.Parameters["@id"].Value = id;

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        list.Add(new Post((long)dataReader["Postid"], (long)dataReader["userid"], (string)dataReader["message"], (DateTime)dataReader["postdate"]));
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            finally
            {
                connection.Close();
            }
            return list;
        }
    }
}
