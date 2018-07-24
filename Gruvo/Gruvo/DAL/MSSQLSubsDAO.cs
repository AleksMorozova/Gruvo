using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Gruvo.Models;

namespace Gruvo.DAL
{
    public class MSSQLSubsDAO //: ISubs
    {
     /*   private SqlConnection connection;

        public MSSQLSubsDAO(SqlConnection connection) => this.connection = connection;

        public void AddSubscription(long userId1, long userId2, DateTime subdate)
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

        public IEnumerable<User> GetSubscriptions(long id)
        {
            List<User> list = new List<User>();
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
                            list.Add(new User((long)dr["userid"], (string)dr["login"], (string)dr["email"], (DateTime)dr[".Regdate"]));
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

        public IEnumerable<User> GetSubscribers(long id)
        {
            List<User> list = new List<User>();
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
                            list.Add(new User((long)dr["userid"], (string)dr["login"], (string)dr["email"], (DateTime)dr["Regdate"]));
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

        public Subscription GetSubscription(long userId1, long userId2)
        {
            Subscription sub = null;
            try
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = @"select * from subscriptions where subscriberid = @userid1 and subscribedid = @userid2";
                    command.Parameters.Add("@userid1", SqlDbType.BigInt);
                    command.Parameters["@userid1"].Value = userId1;

                    command.Parameters.Add("@userid2", SqlDbType.BigInt);
                    command.Parameters["@userid2"].Value = userId2;

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        while (dr.Read())
                            sub = new Subscription((long)dr["subscriberid"], (long)dr["SubscribedId"], (DateTime)dr["SubDate"]);
                    }
                }
                connection.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return sub;
        }

        public void DeleteSubscription(long userId1, long userId2)
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
        }*/
    }

}