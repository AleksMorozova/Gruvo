using Gruvo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruvo.Data
{
    public class AccessDatabase
    {
        private static AccessDatabase instance;
        private static MSSQL db;

        private AccessDatabase()
        {

        }

        public static AccessDatabase GetInstance()
        {
            if (instance == null)
                instance = new AccessDatabase();
            return instance;
        }

        public static MSSQL MsSQL()
        {
            if(db == null)
                db = new MSSQL("Data Source=INTEL;Initial Catalog=Gruvo;Integrated Security=True");
            return db;
        }
    }
}
