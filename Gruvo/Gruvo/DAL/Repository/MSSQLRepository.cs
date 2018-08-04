using Microsoft.Extensions.Configuration;

namespace Gruvo.DAL.Repository
{
    public class MSSQLRepository : BaseRepository
    {
        public MSSQLRepository(IConfiguration configuration)
        {
            string connectionStr = configuration.GetConnectionString("GruvoMSSQL");
            userDAO = new MSSQLUserDAO(connectionStr);
            postDAO = new MSSQLPostDAO(connectionStr);
        }
    }
}
