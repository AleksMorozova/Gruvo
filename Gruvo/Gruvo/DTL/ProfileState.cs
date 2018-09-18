using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruvo.DTL
{
    public class ProfileState
    {
        public DateTimeOffset date { get; set; }
        public long? id { get; set; }

        public ProfileState(DateTimeOffset date, long? id)
        {
            this.date = date;
            this.id = id;
        }
    }
}
