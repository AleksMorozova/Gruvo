using Microsoft.Extensions.Configuration;

namespace Gruvo.DAL.Repository
{
    public class MSSQLRepository : BaseRepository
    {
        public MSSQLRepository(IConfiguration configuration)
        {
            string connectionStr = configuration.GetConnectionString("GruvoMSSQL");
            userDAO = new MSSQLUserDAO(connectionStr);
            tweetDAO = new MSSQLTweetDAO(connectionStr);
        }
    }
}
