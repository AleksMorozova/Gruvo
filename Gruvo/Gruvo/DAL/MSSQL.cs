using Microsoft.Extensions.Configuration;

namespace Gruvo.DAL
{
    public class MSSQL : BaseDAO, IMSSQLRepository
    {
        public MSSQL(IConfiguration configuration)
        {
            string connectionStr = configuration.GetConnectionString("GruvoMSSQL");
            userDAO = new MSSQLUserDAO(connectionStr);
            postDAO = new MSSQLPostDAO(connectionStr);
        }
    }
}
