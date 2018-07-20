using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gruvo.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Login { get; set; }
        [DataType(DataType.Password)] 
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)] 
        public string Email { get; set; }
        public string ProfileDesc { get; set; }
    }
}
