namespace Gruvo.DAL.Repository
{
    public abstract class BaseRepository
    {
        protected IUserDAO userDAO;
        protected ITweetDAO tweetDAO;

        public IUserDAO UserDAO { get { return userDAO; } }
        public ITweetDAO TweetDAO { get { return tweetDAO; } }
    }
}
