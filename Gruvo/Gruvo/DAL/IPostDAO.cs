using System;
using System.Collections.Generic;
using Gruvo.Models;

namespace Gruvo.DAL
{
    public interface IPostDAO
    {
        void AddPost(long userId, string message, DateTime sendingDateTime);

        IEnumerable<Post> GetPostsForUser(long id);
        Post GetPost(long id);

        void DeletePost(long id);
    }
}
