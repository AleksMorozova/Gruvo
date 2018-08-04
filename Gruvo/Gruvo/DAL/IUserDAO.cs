using System;
using System.Collections.Generic;
using Gruvo.Models;

namespace Gruvo.DAL
{
    public interface IUserDAO
    {
        void AddUser(string login, string password, string email, DateTime regDateTime);

        IEnumerable<UserInfo> GetUsers();
        UserInfo GetUser(int id);
        UserInfo GetUser(string login);
        UserInfo GetUserByEmailAndPwd(string email, string password);

        void UpdateCredentianals(int id, string login, string password, string email);
        void UpdateUserInfo(int id, string about, DateTime bday);
        void DeleteUser(int id);
        void DeleteUser(string login);

        void Subscribe(long userId1, long userId2, DateTime subdate);

        IEnumerable<UserInfo> GetSubscribers(long id);
        IEnumerable<UserInfo> GetSubscriptions(long id);

        void Unsubscribe(long userid1, long userid2);

    }
}
