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

                    command.Parameters.Add("@postdate", SqlDbType.DateTimeOffset);
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
        
        public IEnumerable<ReadableTweet> GetPostsBatchForUser(long id, DateTimeOffset date)
        {
            int count = 5;
            List<ReadableTweet> list = new List<ReadableTweet>();
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select top (@count) postid,posts.userid,login,message,postdate from posts 
                                            join users on users.userid = posts.userid  where 
                                            postdate < @date and (posts.userid in 
                                            (select SubscribedId from subscriptions where SubscriberId = @id) 
                                            or posts.userid = @id) order by postdate desc";
                    command.Parameters.Add("@id", SqlDbType.BigInt);
                    command.Parameters["@id"].Value = id;

                    command.Parameters.Add("@count", SqlDbType.Int);
                    command.Parameters["@count"].Value = count;

                    command.Parameters.Add("@date", SqlDbType.DateTimeOffset);
                    command.Parameters["@date"].Value = date;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            list.Add(new ReadableTweet(
                                (long)dataReader["Postid"],
                                (long)dataReader["userid"],
                                (string)dataReader["login"],
                                (string)dataReader["message"],
                                (long)dataReader["userid"]==id ? true : false,
                                (DateTimeOffset)dataReader["postdate"]));
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
                            list.Add(new ReadableTweet(
                                (long)dataReader["Postid"],
                                (long)dataReader["userid"],
                                (string)dataReader["login"],
                                (string)dataReader["message"],
                                (long)dataReader["userid"]==id ? true : false,
                                (DateTimeOffset)dataReader["postdate"]));
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
        public Int32 GetUserPostsCount(long id)
        {
            Int32 qlt;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select count(*) from posts where posts.userid = @id";
                    command.Parameters.Add("@id", SqlDbType.BigInt);
                    command.Parameters["@id"].Value = id;

                    qlt = (Int32)command.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return qlt;
        }

        public IEnumerable<ReadableTweet> GetUserPosts(long id,bool otherUser)
        {
            List<ReadableTweet> list = new List<ReadableTweet>();
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select postid,posts.userid,login,message,postdate from posts join users on users.userid = posts.userid where posts.userid = @id order by postdate desc";
                    command.Parameters.Add("@id", SqlDbType.BigInt);
                    command.Parameters["@id"].Value = id;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            list.Add(new ReadableTweet((long)dataReader["Postid"],
                            (long)dataReader["userid"], 
                            (string)dataReader["login"],
                            (string)dataReader["message"],
                            otherUser,
                            (DateTimeOffset)dataReader["postdate"]));
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

        public int Like(long postId, long userId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"INSERT INTO likes(PostId, UserId) VALUES(@post_id, @user_id)";

                    command.Parameters.Add("@post_id", SqlDbType.BigInt);
                    command.Parameters["@post_id"].Value = postId;

                    command.Parameters.Add("@user_id", SqlDbType.BigInt);
                    command.Parameters["@user_id"].Value = userId;


                    return command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public int Dislike(long postId, long userId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"DELETE FROM likes WHERE (PostId = @post_id AND UserId = @user_id)";

                    command.Parameters.Add("@post_id", SqlDbType.BigInt);
                    command.Parameters["@post_id"].Value = postId;

                    command.Parameters.Add("@user_id", SqlDbType.BigInt);
                    command.Parameters["@user_id"].Value = userId;


                    return command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public int GetNumOfLikes(long postId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"SELECT COUNT(*) FROM likes WHERE PostId = @post_id";

                    command.Parameters.Add("@post_id", SqlDbType.BigInt);
                    command.Parameters["@post_id"].Value = postId;

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public bool CheckIfUserLiked(long postId, long userId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"SELECT COUNT(*) FROM likes WHERE PostId = @post_id AND UserId = @user_id";

                    command.Parameters.Add("@post_id", SqlDbType.BigInt);
                    command.Parameters["@post_id"].Value = postId;

                    command.Parameters.Add("@user_id", SqlDbType.BigInt);
                    command.Parameters["@user_id"].Value = userId;

                    return Convert.ToInt32(command.ExecuteScalar()) == 1 ? true : false;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public bool CheckIfUserHasTweet(long postId, long userId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"SELECT COUNT(*) FROM posts WHERE PostId = @post_id AND UserId = @user_id";

                    command.Parameters.Add("@post_id", SqlDbType.BigInt);
                    command.Parameters["@post_id"].Value = postId;

                    command.Parameters.Add("@user_id", SqlDbType.BigInt);
                    command.Parameters["@user_id"].Value = userId;

                    return Convert.ToInt32(command.ExecuteScalar()) == 1 ? true : false;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void AddComment(long tweetId, long userId, string message, DateTime sendingDateTime)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"insert into comments (postid,userid,message,postdate) values (@tweetid, @userid, @message,@postdate)";
                    command.Parameters.Add("@tweetid", SqlDbType.BigInt);
                    command.Parameters["@tweetid"].Value = tweetId;

                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = userId;

                    command.Parameters.Add("@message", SqlDbType.VarChar);
                    command.Parameters["@message"].Value = message;

                    command.Parameters.Add("@postdate", SqlDbType.DateTimeOffset);
                    command.Parameters["@postdate"].Value = sendingDateTime;

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void DeleteComment(long commentId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"delete from comments where commentid = @commentId ";
                    command.Parameters.Add("@commentId", SqlDbType.BigInt);
                    command.Parameters["@commentId"].Value = commentId;

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public IEnumerable<Comment> GetComments(long tweetId,long userid)
        {
            List<Comment> list = new List<Comment>();
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select comments.commentid,comments.postid,comments.userid,users.login,comments.message,comments.postdate 
                                            from comments join users on users.userid=comments.userid
                                            where postId = @tweetid order by postdate desc";
                    command.Parameters.Add("@tweetid", SqlDbType.BigInt);
                    command.Parameters["@tweetid"].Value = tweetId;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            long uid = (long)dataReader["userid"];
                            list.Add(new Comment((long)dataReader["commentId"],
                                (long)dataReader["postid"],
                            uid,
                            (string)dataReader["login"],
                            (string)dataReader["message"],
                            (DateTimeOffset)dataReader["postdate"],
                            uid==userid ? true:false));
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

        public Comment GetComment(long commentId)
        {
            Comment item = null;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select comments.commentid,comments.postid,comments.userid,users.login,comments.message,comments.postdate 
                                            from comments join users on users.userid=comments.userid
                                            where commentid = @commentId";
                    command.Parameters.Add("@commentId", SqlDbType.BigInt);
                    command.Parameters["@commentId"].Value = commentId;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            item=new Comment((long)dataReader["commentId"],
                            (long)dataReader["postid"],
                             (long)dataReader["userid"],
                            (string)dataReader["login"],
                            (string)dataReader["message"],
                            (DateTimeOffset)dataReader["postdate"],
                            null);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return item;
        }
    }
}
