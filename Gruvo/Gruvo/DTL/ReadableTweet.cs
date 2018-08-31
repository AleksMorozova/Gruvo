using System;

namespace Gruvo.DTL
{
    public class ReadableTweet
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserLogin { get; set; }
        public string Message { get; set; }
        public DateTimeOffset SendingDateTime { get; set; }
        public bool? IsDeletable{ get; set; }        
        public ReadableTweet(long id, long userId, string userLogin, string message, bool isDeletable, DateTimeOffset sendingDateTime)
        {
            Id = id;
            UserId = userId;
            UserLogin = userLogin;
            Message = message;
            IsDeletable=isDeletable;
            SendingDateTime = sendingDateTime;
        }

    }
}
