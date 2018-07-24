using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gruvo.Models
{
    public class UserInfo
    {
        public long Id { get; set; }
        public string Login { get; set; }        
        public string Password { get; set; }
        public string Email { get; set; }
       // public string About { get; set; }
        public DateTime RegDateTime { get; set; }
       // public DateTime Bday { get; set; }

        public UserInfo(long id, string login, string email, DateTime regDateTime)
        {
            this.Id = id;
            this.Login = login;
            this.Email = email;
            this.RegDateTime = regDateTime;
        }
    }
}
