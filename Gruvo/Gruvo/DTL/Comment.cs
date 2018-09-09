using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruvo.DTL
{
    public class Comment
    {
        public Comment(long commentId, long tweetId, long userId,string userLogin, string message, DateTimeOffset postDate, bool? isDeletable)
        {
            CommentId = commentId;
            TweetId = tweetId;
            UserId = userId;
            UserLogin = userLogin;
            Message = message;
            PostDate = postDate;
            IsDeletable = isDeletable;
        }

        public long CommentId { get; set; }
        public long TweetId { get; set; }
        public long UserId { get; set; }
        public string UserLogin { get; set; }
        public string Message { get; set; }
        public DateTimeOffset PostDate { get; set; }
        public bool? IsDeletable { get; set; }
    }
}
