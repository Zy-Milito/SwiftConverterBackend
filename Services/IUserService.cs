using Common.DTO;
using Data.Entities;

namespace Services
{
    public interface IUserService
    {
        List<UserForView> GetAllUsers();
        UserForView? GetUser(string username);
        void AddUser(UserForCreation userForCreation);
        User? ValidateUser(UserForLogin loginData);
    }
}
