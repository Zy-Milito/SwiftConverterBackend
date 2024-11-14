using Data.Entities;

namespace Data.Repository
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User? GetByUsername(string username);
        void AddUser(User newUser);
    }
}
