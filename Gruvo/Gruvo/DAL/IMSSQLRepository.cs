namespace Gruvo.DAL
{
    public interface IMSSQLRepository
    {
        IUserDAO UserDAO { get; }
        IPostDAO PostDAO { get; }
    }
}
