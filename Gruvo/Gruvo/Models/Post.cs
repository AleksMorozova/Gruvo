using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gruvo.Models
{
    public class Post
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Message { get; set; }
        public DateTime SendingDateTime { get; set; }

        public Post(long id, long userId, string message, DateTime postDate)
        {
            this.Id = id;
            this.UserId = userId;
            this.Message = message;
            this.SendingDateTime = postDate;
        }
    }
}
