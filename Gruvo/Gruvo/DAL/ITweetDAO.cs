using System;
using System.Collections.Generic;
using Gruvo.DTL;

namespace Gruvo.DAL
{
    public interface ITweetDAO
    {
        void AddPost(long userId, string message, DateTime sendingDateTime);

        IEnumerable<ReadableTweet> GetPostsForUser(long id);
        IEnumerable<ReadableTweet> GetUserPosts(long id);
        ReadableTweet GetPost(long id);

        Int32 GetUserPostsCount(long id);

        void DeletePost(long id);
    }
}
