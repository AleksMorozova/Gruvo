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
        string GetUserPassword(long id);

        void UpdatePassword(long id,  string password);
        void UpdateUserInfo(long id, string login, string email, string about, DateTime? bday);
        void DeleteUser(long id);
        void DeleteUser(string login);

        void Subscribe(long userId1, long userId2, DateTime subdate);

        bool IsSubscribed(long userId1, long userId2);

        /// <summary>
        /// Returns specified number of subscribers. If 'subscriberId' is specified, returns only subscribers with higher id(located after this subscriber).
        /// </summary>
        /// <param name="userId">User for whom we are selecting subscribers</param>
        /// <param name="subscriberId">UserId of subscriber</param>
        /// <param name="numOfSubsToReturn">Max number of subscribers that we will return in collection</param>
        /// <returns></returns>
        IEnumerable<UserInfo> GetSubscribers(long userId, long? subscriberId, int numOfSubsToReturn);

        /// <summary>
        /// Returns specified number of subscriptions. If 'subscriptionId' is specified, returns only subscriptions with higher id(located after this subscription).
        /// </summary>
        /// <param name="userId">User for whom we are selecting subscriptions</param>
        /// <param name="subscriptionId">UserId of subscribed user</param>
        /// <param name="numOfSubsToReturn">Max number of subscriptions that we will return in collection</param>
        /// <returns></returns>
        IEnumerable<UserInfo> GetSubscriptions(long userId, long? subscriptionId, int numOfSubsToReturn);

        IEnumerable<UserInfo> GetRandomUsers(int count);

        Int32 GetSubscribersCount(long id);
        Int32 GetSubscriptionsCount(long id);

        void Unsubscribe(long userid1, long userid2);

    }
}
