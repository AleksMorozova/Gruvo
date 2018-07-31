using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Gruvo.Models;

namespace Gruvo.DAL
{
    class MSSQLUserDAO : IUserDAO
    {
        private SqlConnection connection;

        public MSSQLUserDAO(SqlConnection connection) => this.connection = connection;

        public void AddUser(string login, string password, string email, DateTime regDate)
        {
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

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

        public void DeleteUser(int id)
        {
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"delete from users where Userid = @id ";
                command.Parameters.Add("@id", SqlDbType.BigInt);
                command.Parameters["@id"].Value = id;

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

        public void DeleteUser(string login)
        {
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"delete from users where Login = @login ";
                command.Parameters.Add("@login", SqlDbType.VarChar);
                command.Parameters["@login"].Value = login;

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

        public UserInfo GetUser(int id)
        {
            UserInfo user = null;
            try
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = @"select UserId,Login,Email,RegDate from users where Userid = @id ";
                command.Parameters.Add("@id", SqlDbType.BigInt);
                command.Parameters["@id"].Value = id;

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    user = new UserInfo((long)dataReader["UserId"], (string)dataReader["login"], (string)dataReader["email"], (DateTime)dataReader["RegDate"]);
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
            return user;
        }

        public UserInfo GetUser(string login)
        {
            UserInfo user = null;
            try
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"select UserId,Login,Email,RegDate from users where Login = @login ";
                    command.Parameters.Add("@login", SqlDbType.VarChar);
                    command.Parameters["@login"].Value = login;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                            user = new UserInfo((long)dataReader["UserId"], (string)dataReader["login"], (string)dataReader["email"], (DateTime)dataReader["RegDate"]);
                    }
                }
                connection.Close();
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
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"select UserId,Login,Email,RegDate from users where Email = @email and Password = @pwd";
                    command.Parameters.Add("@email", SqlDbType.VarChar);
                    command.Parameters["@email"].Value = email;
                    command.Parameters.Add("@pwd", SqlDbType.VarChar);
                    command.Parameters["@pwd"].Value = password;

                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                            user = new UserInfo((long)dataReader["UserId"], (string)dataReader["login"], (string)dataReader["email"], (DateTime)dataReader["RegDate"]);
                    }
                }
                connection.Close();
            }

            catch (Exception)
            {
                throw;
            }
            return user;
        }

        public IEnumerable<UserInfo> GetUsers()
        {
            List<UserInfo> list = new List<UserInfo>();
            try
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select userId,login,email,regDate from users";
                    using (SqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            list.Add(new UserInfo((long)dataReader["UserId"], (string)dataReader["login"], (string)dataReader["email"], (DateTime)dataReader["RegDate"]));
                        }
                    }
                }
                connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public void UpdateCredentianals(int id, string login, string password, string email)
        {
            try
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"update users set Login=@login, Password = @password, Email=@email where userid = @userid";

                    command.Parameters.Add("@login", SqlDbType.VarChar);
                    command.Parameters["@login"].Value = login;

                    command.Parameters.Add("@password", SqlDbType.VarChar);
                    command.Parameters["@password"].Value = password;

                    command.Parameters.Add("@email", SqlDbType.VarChar);
                    command.Parameters["@email"].Value = email;


                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = id;

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateUserInfo(int id, string about, DateTime bday)
        {
            try
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"update users set DateOfBirth=@bday, about = @about where userid = @userid";

                    command.Parameters.Add("@bday", SqlDbType.Date);
                    command.Parameters["@bday"].Value = bday;

                    command.Parameters.Add("@about", SqlDbType.VarChar);
                    command.Parameters["@about"].Value = about;

                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = id;

                    command.ExecuteNonQuery();
                }
                connection.Close();
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
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"insert into subscriptions values (@userid1, @userid2,@subdate)";
                    command.Parameters.Add("@userid1", SqlDbType.BigInt);
                    command.Parameters["@userid1"].Value = userId1;

                    command.Parameters.Add("@userid2", SqlDbType.BigInt);
                    command.Parameters["@userid2"].Value = userId2;

                    command.Parameters.Add("@subdate", SqlDbType.Date);
                    command.Parameters["@subdate"].Value = subdate;

                    command.ExecuteNonQuery();
                }
                connection.Close();
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
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"select userid, login, email, Regdate from users join subscriptions on userid = subscribedid where subscriberid = @userid";
                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = id;

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                            list.Add(new UserInfo((long)dr["userid"], (string)dr["login"], (string)dr["email"], (DateTime)dr[".Regdate"]));
                    }
                }
                connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public IEnumerable<UserInfo> GetSubscribers(long id)
        {
            List<UserInfo> list = new List<UserInfo>();
            try
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"select userid, login, users.email, Regdate from users join subscriptions on userid = subscriberid where subscribedid = @userid";
                    command.Parameters.Add("@userid", SqlDbType.BigInt);
                    command.Parameters["@userid"].Value = id;

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                            list.Add(new UserInfo((long)dr["userid"], (string)dr["login"], (string)dr["email"], (DateTime)dr["Regdate"]));
                    }
                }
                connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return list;
        }

        public void Unsubscribe(long userId1, long userId2)
        {
            try
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"delete from subscriptions where subscriberid = @userid1 and subscribedid = @userid2";
                    command.Parameters.Add("@userid1", SqlDbType.BigInt);
                    command.Parameters["@userid1"].Value = userId1;

                    command.Parameters.Add("@userid2", SqlDbType.BigInt);
                    command.Parameters["@userid2"].Value = userId2;

                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
