using Microsoft.Extensions.Configuration;

namespace Gruvo.DAL
{
    public class Store
    {
        public static BaseDAO DAO { get; private set; }

        public Store(IConfiguration configuration)
        {
            //DAO = new MSSQL(configuration.GetConnectionString("Gruvo"));
        }
    }
}
