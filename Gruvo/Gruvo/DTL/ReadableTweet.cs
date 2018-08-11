using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruvo.DTL
{
    public class ReadableTweet
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserLogin { get; set; }
        public string Message { get; set; }
        public DateTimeOffset SendingDateTime { get; set; }
        
        public ReadableTweet(long id, long userId, string userLogin, string message, DateTimeOffset sendingDateTime)
        {
            Id = id;
            UserId = userId;
            UserLogin = userLogin;
            Message = message;
            SendingDateTime = sendingDateTime;
        }

    }
}
