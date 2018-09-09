using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruvo.DTL
{
    public class Comment
    {
        public Comment(long tweetId, long userId, string message, DateTime postDate)
        {
            TweetId = tweetId;
            UserId = userId;
            Message = message;
            PostDate = postDate;
        }

        public long TweetId { get; set; }
        public long UserId { get; set; }
        public string Message { get; set; }
        public DateTime PostDate { get; set; }
    }
}
