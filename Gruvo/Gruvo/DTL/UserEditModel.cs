using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruvo.DTL
{
    public class UserEditModel
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string About { get; set; }
        public DateTime? Bday { get; set; }

    }
}
