using Gruvo.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gruvo.Data
{
    public class Store
    {
        private static Store instance;
        private static MSSQL db;

        private Store()
        {

        }

        public static Store GetInstance()
        {
            if (instance == null)
            {
                instance = new Store();
            }
            return instance;
        }

        public static MSSQL MsSQL()
        {
            if (db == null)
            {
                db = new MSSQL("Data Source=INTEL;Initial Catalog=Gruvo;Integrated Security=True");
            }
            return db;
        }
    }
}
