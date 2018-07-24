using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Gruvo.Models;

namespace Gruvo.DAL
{
    public interface IUserDAO
    {
        void AddUser(string login, string password, string email, DateTime regDateTime);

        IEnumerable<UserInfo> GetUsers();
        UserInfo GetUser(int id);
        UserInfo GetUser(string login);

        void UpdateUser(int id, string login, string password, string email);
        void UpdateUser(int id, string about, DateTime bday);
        void DeleteUser(int id);
        void DeleteUser(string login);
    }
}
