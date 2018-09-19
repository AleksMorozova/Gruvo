using System;

namespace Gruvo.Models
{
    public class UserInfo
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string About { get; set; }
        public DateTime RegDateTime { get; set; }
        public DateTime? Bday { get; set; }
        public bool? IsSubscribed { get; set; }

        public UserInfo(long id, string login,string image, bool? isSubscribed, DateTime regDateTime)
        {
            this.Id = id;
            this.Login = login;
            this.IsSubscribed = isSubscribed;
            this.RegDateTime = regDateTime;
            this.Image = image;
        }

        public UserInfo(long id, string login, string image, string email, DateTime regDateTime, DateTime? bday, string about)
        {
            this.Id = id;
            this.Login = login;
            this.Email = email;
            this.RegDateTime = regDateTime;
            this.Bday = bday;
            this.About = about;
            this.Image = image;
        }
    }
}
