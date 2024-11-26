using Common.DTO;
using Data.Entities;

namespace Services
{
    public interface IUserService
    {
        List<UserForView> GetAllUsers();
        UserForView? GetUser(int id);
        void AddUser(UserForCreation userForCreation);
        User? ValidateUser(UserForLogin loginData);
        bool RemoveUser(int id);
    }
}
