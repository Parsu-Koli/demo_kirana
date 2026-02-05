using DAL.Models;

namespace DAL.Repository.Interfaces
{
    public interface IAuthRepository
    {
        User Login(string username, string password);
        bool IsUsernameExists(string username);

        void AddUser(User user);
    }
}
