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

                    command.CommandText = @"select UserId,Login,Email,RegDate, DateOfBirth, About from users where Userid = @id ";

                    command.Parameters.Add("@id", SqlDbType.BigInt);
                    command.Parameters["@id"].Value = id;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            DateTime? bday;
                            string about;
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
                            user = new UserInfo((long)dataReader["UserId"], (string)dataReader["login"], (string)dataReader["email"], (DateTime)dataReader["RegDate"], bday, about);
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

                    command.CommandText = @"select UserId,Login,Email,RegDate from users where Login = @login ";
                    command.Parameters.Add("@login", SqlDbType.VarChar);
                    command.Parameters["@login"].Value = login;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            user = new UserInfo((long)dataReader["UserId"], (string)dataReader["login"], null, (DateTime)dataReader["RegDate"]);
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
                            user = new UserInfo((long)dataReader["UserId"], (string)dataReader["login"], null, (DateTime)dataReader["RegDate"]);
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

        public UserInfo GetUserByEmail (string email)
        {
            UserInfo user = null;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select UserId,Login,Email,RegDate from users where Email = @email";
                    command.Parameters.Add("@email", SqlDbType.VarChar);
                    command.Parameters["@email"].Value = email;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            user = new UserInfo((long)dataReader["UserId"], (string)dataReader["login"], null, (DateTime)dataReader["RegDate"]);
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


        public UserInfo GetUserByLogin(string login)
        {
            UserInfo user = null;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select UserId,Login,Email,RegDate from users where Login = @login";
                    command.Parameters.Add("@login", SqlDbType.VarChar);
                    command.Parameters["@login"].Value = login;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            user = new UserInfo((long)dataReader["UserId"], (string)dataReader["login"], null, (DateTime)dataReader["RegDate"]);
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
                            list.Add(new UserInfo((long)dataReader["UserId"], (string)dataReader["login"], null, (DateTime)dataReader["RegDate"]));
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

        public IEnumerable<UserInfo> GetSubscriptions(long id)
        {
            List<UserInfo> list = new List<UserInfo>();
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select userid, login, email, Regdate from users join subscriptions on userid = subscribedid where subscriberid = @userid";
                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = id;

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new UserInfo((long)dr["userid"], (string)dr["login"], null, (DateTime)dr["Regdate"]));
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

        public Int32 GetSubscriptionsCount(long id)
        {
            Int32 qlt;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select count(*) from users join subscriptions on userid = subscribedid where subscriberid = @userid";
                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = id;

                    qlt = (Int32) command.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return qlt;
        }

        public IEnumerable<UserInfo> GetSubscribers(long id)
        {
            List<UserInfo> list = new List<UserInfo>();
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select userid, login, users.email, Regdate from users join subscriptions on userid = subscriberid where subscribedid = @userid";
                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = id;

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            list.Add(new UserInfo((long)dr["userid"], (string)dr["login"], null, (DateTime)dr["Regdate"]));
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
            Int32 qlt;
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select count(*) from users join subscriptions on userid = subscriberid where subscribedid = @userid"; ;
                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = id;

                    qlt = (Int32)command.ExecuteScalar();
                }
            }
            catch (Exception)
            {
                throw;
            }
            return qlt;
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

        public IEnumerable<UserInfo> GetRandomUsers(int count)
        {
            List<UserInfo> list = new List<UserInfo>();
            try
            {
                using (var connection = new SqlConnection(_connectionStr))
                using (var command = connection.CreateCommand())
                {
                    connection.Open();

                    command.CommandText = @"select top (@count) * from users order by newid()";
                    command.Parameters.Add("@count", SqlDbType.Int);
                    command.Parameters["@count"].Value = count;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            list.Add(new UserInfo((long)dataReader["UserId"], (string)dataReader["login"], null, (DateTime)dataReader["RegDate"]));
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
    }
}
