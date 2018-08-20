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

        void DeletePost(long id);

        /// <summary>
        /// Returns number of affected rows
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int Like(long postId, long userId);

        /// <summary>
        /// Returns number of affected rows
        /// </summary>
        /// <param name="postId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int Dislike(long postId, long userId);
    }
}
