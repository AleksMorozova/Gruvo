using System;
using System.Collections.Generic;
using Gruvo.Models;

namespace Gruvo.DAL
{
    public interface IUserDAO
    {
        void AddUser(string login, string password, string email, DateTime regDateTime);

        IEnumerable<UserInfo> GetUsers();
        UserInfo GetUser(long id);
        UserInfo GetUser(string login);
        UserInfo GetUserByEmailAndPwd(string email, string password);

        void UpdateCredentianals(long id, string login, string password, string email);
        void UpdateUserInfo(long id, string about, DateTime bday);
        void DeleteUser(long id);
        void DeleteUser(string login);

        void Subscribe(long userId1, long userId2, DateTime subdate);

        bool IsSubscribed(long userId1, long userId2);

        IEnumerable<UserInfo> GetSubscribers(long id);
        IEnumerable<UserInfo> GetSubscriptions(long id);
        IEnumerable<UserInfo> GetRandomUsers(int count);

        void Unsubscribe(long userid1, long userid2);

    }
}
