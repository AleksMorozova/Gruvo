using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Gruvo.Models;
using Gruvo.DTL;

namespace Gruvo.DAL
{
    class MSSQLTweetDAO : ITweetDAO
    {
        private string _connectionStr;

        public MSSQLTweetDAO(string connectionString) => this._connectionStr = connectionString;

        public void AddPost(long userId, string message, DateTime postDateTime)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"insert into posts (userid,message,postdate) values (@userid, @message,@postdate)";
                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = userId;

                    command.Parameters.Add("@message", SqlDbType.VarChar);
                    command.Parameters["@message"].Value = message;

                    command.Parameters.Add("@postdate", SqlDbType.Date);
                    command.Parameters["@postdate"].Value = postDateTime;

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
        
        public void DeletePost(long id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"delete from posts where postid = @postid ";
                    command.Parameters.Add("@postid", SqlDbType.BigInt);
                    command.Parameters["@postid"].Value = id;

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public ReadableTweet GetPost(long id)
        {
            ReadableTweet tweet = null;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select postid,posts.userid,login,message,postdate from posts join users on users.userid = posts.userid where postid = @id ";
                    command.Parameters.Add("@id", SqlDbType.BigInt);
                    command.Parameters["@id"].Value = id;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            tweet = new ReadableTweet((long)dataReader["Postid"], (long)dataReader["userid"], (string)dataReader["login"], (string)dataReader["message"], (DateTimeOffset)dataReader["postdate"]);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return tweet;
        }

        public IEnumerable<ReadableTweet> GetPostsForUser(long id)
        {
            List<ReadableTweet> list = new List<ReadableTweet>();
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select postid,posts.userid,login,message,postdate from posts join users on users.userid = posts.userid  where posts.userid in (select SubscribedId from subscriptions where SubscriberId = @id) or posts.userid = @id order by postdate desc";
                    command.Parameters.Add("@id", SqlDbType.BigInt);
                    command.Parameters["@id"].Value = id;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            list.Add(new ReadableTweet((long)dataReader["Postid"], (long)dataReader["userid"], (string)dataReader["login"], (string)dataReader["message"], (DateTimeOffset)dataReader["postdate"]));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return list;
        }

        public IEnumerable<ReadableTweet> GetUserPosts(long id)
        {
            List<ReadableTweet> list = new List<ReadableTweet>();
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select postid,posts.userid,login,message,postdate from posts join users on users.userid = posts.userid where posts.userid = @id by postdate desc";
                    command.Parameters.Add("@id", SqlDbType.BigInt);
                    command.Parameters["@id"].Value = id;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            list.Add(new ReadableTweet((long)dataReader["Postid"], (long)dataReader["userid"], (string)dataReader["login"], (string)dataReader["message"], (DateTimeOffset)dataReader["postdate"]));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return list;
        }
    }
}
