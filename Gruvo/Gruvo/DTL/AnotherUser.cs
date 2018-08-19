using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruvo.DTL
{
    public class AnotherUser
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string About { get; set; }
        public DateTime RegDateTime { get; set; }
        public DateTime? Bday { get; set; }
        public bool IsSubscribed { get; set; }
    }
}
