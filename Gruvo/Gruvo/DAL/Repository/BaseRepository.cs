namespace Gruvo.DAL.Repository
{
    public abstract class BaseRepository
    {
        protected IUserDAO userDAO;
        protected IPostDAO postDAO;

        public IUserDAO UserDAO { get { return userDAO; } }
        public IPostDAO PostDAO { get { return postDAO; } }
    }
}
