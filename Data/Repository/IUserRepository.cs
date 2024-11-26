using Data.Entities;

namespace Data.Repository
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User? GetById(int id);
        User? GetByUsername(string username);
        void AddUser(User newUser);
        void UpdateUser(User user);
    }
}
