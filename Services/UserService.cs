using Common.DTO;
using Data.Entities;
using Data.Repository;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<UserForView> GetAllUsers()
        {

            var users = _userRepository.GetAll();

            return users.Select(u => new UserForView()
            {
                Username = u.Username,
                Email = u.Email,
                IsAdmin = u.IsAdmin,
                SubscriptionPlanId = u.SubscriptionPlanId,
                FavedCurrencies = u.FavedCurrencies,
                ConversionHistory = u.ConversionHistory,
                AccountStatus = u.AccountStatus,
            }).ToList();
        }

        public UserForView? GetUser(string username)
        {
            User? user = _userRepository.GetByUsername(username);

            if (user is null)
            {
                throw new Exception("User not found.");
            }

            UserForView userForView = new UserForView()
            {
                Username = user.Username,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                SubscriptionPlanId = user.SubscriptionPlanId,
                FavedCurrencies = user.FavedCurrencies,
                ConversionHistory = user.ConversionHistory,
                AccountStatus = user.AccountStatus,
            };

            return userForView;
        }

        public void AddUser(UserForCreation userForCreation)
        {
            User newUser = new User()
            {
                Username = userForCreation.Username,
                Password = userForCreation.Password,
                Email = userForCreation.Email
            };
            _userRepository.AddUser(newUser);
        }

        public User? ValidateUser(UserForLogin loginData)
        {
            User? userToReturn = _userRepository.GetByUsername(loginData.Username);
            if (userToReturn is not null && userToReturn.Password == loginData.Password)
                return userToReturn;
            return null;
        }
    }
}
