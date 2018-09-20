using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Gruvo.Models;

namespace Gruvo.DAL
{
    class MSSQLUserDAO : IUserDAO
    {
        private string _connectionStr;

        public MSSQLUserDAO(string connectionString) => _connectionStr = connectionString;

        public void AddUser(string login, string password, string email, DateTime regDate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"insert into users (Login,Password,Email,RegDate) values (@login,@password ,@email,@regdate)";
                    command.Parameters.Add("@login", SqlDbType.VarChar);
                    command.Parameters["@login"].Value = login;

                    command.Parameters.Add("@password", SqlDbType.VarChar);
                    command.Parameters["@password"].Value = password;

                    command.Parameters.Add("@email", SqlDbType.VarChar);
                    command.Parameters["@email"].Value = email;

                    command.Parameters.Add("@regdate", SqlDbType.Date);
                    command.Parameters["@regdate"].Value = regDate;

                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void DeleteUser(long id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"delete from users where Userid = @id ";
                    command.Parameters.Add("@id", SqlDbType.BigInt);
                    command.Parameters["@id"].Value = id;

                    command.ExecuteNonQuery();
                }
            }

            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public void DeleteUser(string login)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"delete from users where Login = @login ";
                    command.Parameters.Add("@login", SqlDbType.VarChar);
                    command.Parameters["@login"].Value = login;

                    command.ExecuteNonQuery();
                }
            }            
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public UserInfo GetUser(long id)
        {
            UserInfo user = null;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"select users.UserId,Login,Email,RegDate, DateOfBirth, About, photos.name
                                            from users left join photos on photos.userid=users.userid where users.Userid = @id ";

                    command.Parameters.Add("@id", SqlDbType.BigInt);
                    command.Parameters["@id"].Value = id;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            DateTime? bday;
                            string about;
                            string imgPath;
                            if (dataReader["DateOfBirth"] is DBNull)
                            {
                                bday = null;
                            }
                            else
                            {
                                bday = (DateTime?)dataReader["DateOfBirth"];
                            }

                            if (dataReader["about"] is DBNull)
                            {
                                about = null;
                            }
                            else
                            {
                                about = (string)dataReader["about"];
                            }
                            if (dataReader["name"] is DBNull)
                            {
                                imgPath = null;
                            }
                            else
                            {
                                imgPath = (string)dataReader["name"];
                            }

                            user = new UserInfo((long)dataReader["UserId"], (string)dataReader["login"], imgPath, (string)dataReader["email"], (DateTime)dataReader["RegDate"], bday, about);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return user;
        }

        public UserInfo GetUser(string login)
        {
            UserInfo user = null;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select users.UserId,Login,Email,RegDate, photos.name  from users 
                                            join photos on photos.userid=users.userid where users.Userid = @id where Login = @login ";
                    command.Parameters.Add("@login", SqlDbType.VarChar);
                    command.Parameters["@login"].Value = login;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            user = new UserInfo((long)dataReader["UserId"], (string)dataReader["login"], (string)dataReader["name"], null, (DateTime)dataReader["RegDate"]);
                        }
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public UserInfo GetUserByEmailAndPwd(string email, string password)
        {
            UserInfo user = null;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select UserId,Login,Email,RegDate from users where Email = @email and Password = @pwd";
                    command.Parameters.Add("@email", SqlDbType.VarChar);
                    command.Parameters["@email"].Value = email;
                    command.Parameters.Add("@pwd", SqlDbType.VarChar);
                    command.Parameters["@pwd"].Value = password;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            user = new UserInfo((long)dataReader["UserId"], (string)dataReader["login"],null, null, (DateTime)dataReader["RegDate"]);
                        }
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public string GetUserPassword(long id)
        {
            string pwd = null;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select Password from users where Userid = @id";
                    
                    command.Parameters.Add("@id", SqlDbType.BigInt);
                    command.Parameters["@id"].Value = id;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            pwd= (string)dataReader["Password"];
                        }
                    }
                }
            }

            catch (Exception)
            {
                throw;
            }
            return pwd;
        }

        public IEnumerable<UserInfo> GetUsers()
        {
            List<UserInfo> list = new List<UserInfo>();
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = "select userId,login,email,regDate from users";
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            list.Add(new UserInfo((long)dataReader["UserId"], (string)dataReader["login"],null, null, (DateTime)dataReader["RegDate"]));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public void UpdatePassword(long id, string password)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"update users set Password = @password where userid = @userid";
                    
                    command.Parameters.Add("@password", SqlDbType.VarChar);
                    command.Parameters["@password"].Value = password;

                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = id;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateUserInfo(long id, string login, string email, string about, DateTime? bday)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"update users set Login=@login, Email=@email, DateOfBirth=@bday, about = @about where userid = @userid";

                    command.Parameters.Add("@login", SqlDbType.VarChar);
                    command.Parameters["@login"].Value = login;

                    command.Parameters.Add("@email", SqlDbType.VarChar);
                    command.Parameters["@email"].Value = email;

                    command.Parameters.Add("@bday", SqlDbType.Date);
                    if (bday == null)
                    {
                        command.Parameters["@bday"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["@bday"].Value = bday;
                    }

                    command.Parameters.Add("@about", SqlDbType.VarChar);

                    if (about==null)
                    {
                        command.Parameters["@about"].Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters["@about"].Value = about;
                    }
                    
                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = id;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Subscribe(long userId1, long userId2, DateTime subdate)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"insert into subscriptions values (@userid1, @userid2,@subdate)";
                    command.Parameters.Add("@userid1", SqlDbType.BigInt);
                    command.Parameters["@userid1"].Value = userId1;

                    command.Parameters.Add("@userid2", SqlDbType.BigInt);
                    command.Parameters["@userid2"].Value = userId2;

                    command.Parameters.Add("@subdate", SqlDbType.Date);
                    command.Parameters["@subdate"].Value = subdate;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<UserInfo> GetSubscriptions(long userId, long? subscriptionId, int numOfSubsToReturn)
        {
            List<UserInfo> list = new List<UserInfo>();
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    if (subscriptionId.HasValue)
                    {
                        command.CommandText = @"SELECT TOP(@num) UserId, Login, RegDate 
                                                FROM 
                                                (SELECT * FROM users 
                                                 JOIN subscriptions ON UserId = SubscribedId 
                                                 WHERE SubscriberId = @userid AND SubscribedId > @subscriptionId
                                                 ) AS followings";

                        command.Parameters.Add("@subscriptionId", SqlDbType.BigInt);
                        command.Parameters["@subscriptionId"].Value = subscriptionId;
                    }
                    else
                    {
                        command.CommandText = @"SELECT TOP(@num) userid, login, Regdate 
                                                from users 
                                                join subscriptions on userid = subscribedid 
                                                where subscriberid = @userid";
                    }

                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = userId;

                    command.Parameters.Add("@num", SqlDbType.BigInt);
                    command.Parameters["@num"].Value = numOfSubsToReturn;

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new UserInfo((long)dr["userid"],
                                (string)dr["login"],
                                null, 
                                null,
                                (DateTime)dr["Regdate"]));
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public int GetSubscriptionsCount(long id)
        {
            int numOfSubscriptions;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select count(*) from Subscriptions where SubscriberId = @userid";
                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = id;

                    numOfSubscriptions = (int) command.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return numOfSubscriptions;
        }

        public IEnumerable<UserInfo> GetSubscribers(long userId, long? subscriberId, int numOfSubsToReturn)
        {
            List<UserInfo> list = new List<UserInfo>();
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();


                    if (subscriberId.HasValue)
                    {
                        command.CommandText = @"SELECT TOP (@num) UserId, Login, RegDate FROM 
                                                (select * 
                                                 from users 
                                                 join subscriptions on userid = subscriberid 
                                                 where subscribedid = @userid AND subscriberid > @subscriberId
                                                ) AS followers";

                        command.Parameters.Add("@subscriberId", SqlDbType.BigInt);
                        command.Parameters["@subscriberId"].Value = subscriberId;
                    }
                    else
                    {
                        command.CommandText = @"select TOP (@num) userid, login, Regdate 
                                                from users 
                                                join subscriptions on userid = subscriberid 
                                                where subscribedid = @userid";
                    }

                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = userId;

                    command.Parameters.Add("@num", SqlDbType.BigInt);
                    command.Parameters["@num"].Value = numOfSubsToReturn;

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new UserInfo((long)dr["userid"], (string)dr["login"], null, null, (DateTime)dr["Regdate"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return list;
        }

        public Int32 GetSubscribersCount(long id)
        {
            int numOfSubscribers;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select count(*) from Subscriptions where SubscribedId = @userid"; ;
                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = id;

                    numOfSubscribers = (int)command.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return numOfSubscribers;
        }

        public void Unsubscribe(long userId1, long userId2)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"delete from subscriptions where subscriberid = @userid1 and subscribedid = @userid2";
                    command.Parameters.Add("@userid1", SqlDbType.BigInt);
                    command.Parameters["@userid1"].Value = userId1;

                    command.Parameters.Add("@userid2", SqlDbType.BigInt);
                    command.Parameters["@userid2"].Value = userId2;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<UserInfo> GetRecommendations(long id)
        {
            int usersToDisplay = 3;
            int postsToConsider = 100;    
            
            List<UserInfo> list = new List<UserInfo>();
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select top (@count) * from
                                        ((select top (@count) * from users where userid in 
                                        (select subscribedid from subscriptions where SubscriberId in 
                                        (select top (@nposts) SubscribedId from subscriptions where SubscriberId=@userid)
                                        and SubscribedId not in 
                                        (select top (@nposts) SubscribedId from subscriptions where SubscriberId=@userid)
                                        and SubscribedId <> @userid))
                                        union 
                                        (SELECT top (@count) * FROM users where 
                                        (ABS(CAST( (BINARY_CHECKSUM(*) *  RAND()) as int)) % 100) < 25
                                        and userid not in 
                                        (select top (@nposts) SubscribedId from subscriptions where SubscriberId=@userid)
                                        and userid <> @userid
                                        ))as t order by newid();";

                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = id;

                    command.Parameters.Add("@nposts", SqlDbType.Int);
                    command.Parameters["@nposts"].Value = postsToConsider;

                    command.Parameters.Add("@count", SqlDbType.Int);
                    command.Parameters["@count"].Value = usersToDisplay;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            list.Add(new UserInfo((long)dataReader["UserId"], (string)dataReader["login"],null, null, (DateTime)dataReader["RegDate"]));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return list;
        }

        public bool IsSubscribed(long userId1, long userId2)
        {
            bool result = false;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select * from subscriptions where SubscriberId =@userid1 and SubscribedId = @userid2";
                    command.Parameters.Add("@userid1", SqlDbType.BigInt);
                    command.Parameters["@userid1"].Value = userId1;

                    command.Parameters.Add("@userid2", SqlDbType.BigInt);
                    command.Parameters["@userid2"].Value = userId2;

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return result;
        }

        public void UpdatePhoto(long id, string path, int? x, int? y, int? radius)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"begin tran
                                            if exists (select * from photos where userid = @userid)
                                            begin
                                               update photos set name = @name
                                               where userid = @userid
                                            end
                                            else
                                            begin
                                               insert into photos (userid, name, x, y,radius) 
                                                values (@userid, @name, @x, @y,@radius)
                                            end
                                            commit tran";


                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = id;

                    command.Parameters.Add("@name", SqlDbType.VarChar);
                    command.Parameters["@name"].Value = path;

                    command.Parameters.Add("@x", SqlDbType.Int);
                    command.Parameters["@x"].Value = x;

                    command.Parameters.Add("@y", SqlDbType.Int);
                    command.Parameters["@y"].Value = y;

                    command.Parameters.Add("@radius", SqlDbType.Int);
                    command.Parameters["@radius"].Value = radius;

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public string getPhoto(long userId)
        {
            string path = null;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select name from photos where userid = @userid";
                    
                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = userId;

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            path = (string)dr["name"];
                        }
                    }
                }
                return path;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
