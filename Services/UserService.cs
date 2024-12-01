using Common.DTO;
using Data.Entities;
using Data.Repository;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;
        public UserService(IUserRepository userRepository, ICurrencyRepository currencyRepository)
        {
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
        }

        public List<UserForView> GetAllUsers()
        {

            var users = _userRepository.GetAll();

            return users.Select(u => new UserForView()
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                IsAdmin = u.IsAdmin,
                SubscriptionPlanId = u.SubscriptionPlanId,
                FavedCurrencies = u.FavedCurrencies,
                ConversionHistory = u.ConversionHistory,
                ConversionsLeft = u.SubscriptionPlan.MaxConversions.HasValue ? (u.SubscriptionPlan.MaxConversions.Value - u.ConversionHistory.Count).ToString() : "unlimited",
                AccountStatus = u.AccountStatus,
            }).ToList();
        }

        public UserForView? GetUser(int id)
        {
            User? user = _userRepository.GetById(id);

            if (user is null)
            {
                throw new Exception("User not found.");
            }

            UserForView userForView = new UserForView()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                IsAdmin = user.IsAdmin,
                SubscriptionPlanId = user.SubscriptionPlanId,
                FavedCurrencies = user.FavedCurrencies,
                ConversionHistory = user.ConversionHistory,
                ConversionsLeft = user.SubscriptionPlan.MaxConversions.HasValue ? (user.SubscriptionPlan.MaxConversions.Value - user.ConversionHistory.Count).ToString() : "unlimited",
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

        public void RemoveUser(int id)
        {
            var user = _userRepository.GetById(id);
            if (user is null || user.AccountStatus == false)
            {
                throw new Exception("The user could not be deleted: already removed or does not exist.");
            }
            user.AccountStatus = false;
            _userRepository.UpdateUser(user);
        }

        public void UpgradePlan(int id, int newPlanId, string newPlanName)
        {
            var user = _userRepository.GetById(id);
            if (user is null || user.AccountStatus == false || user.SubscriptionPlan.Id == newPlanId)
            {
                throw new Exception($"Plan not upgraded: user does not exist or has already subscribed to {newPlanName}.");
            }
            user.SubscriptionPlan.Id = newPlanId;
            _userRepository.UpdateUser(user);
        }

        public void AddConversionHistory(int id, ConversionForCreation newConversion)
        {
            var user = _userRepository.GetById(id);
            if (user is null || user.AccountStatus == false)
            {
                throw new Exception("User not found.");
            }

            var fromCurrency = _currencyRepository.GetById(newConversion.FromCurrencyId);
            var toCurrency = _currencyRepository.GetById(newConversion.ToCurrencyId);

            if (fromCurrency is null || toCurrency is null)
            {
                throw new Exception("One or both currencies not found.");
            }

            var conversionsLeft = user.SubscriptionPlan.MaxConversions.HasValue ? user.SubscriptionPlan.MaxConversions.Value - user.ConversionHistory.Count : int.MaxValue;

            if (conversionsLeft <= 0)
            {
                throw new Exception("User has got no conversions left.");
            }

            var conversionHistory = new History
            {
                Date = DateTime.Now,
                UserId = user.Id,
                User = user,
                FromCurrencyId = newConversion.FromCurrencyId,
                FromCurrency = fromCurrency,
                ToCurrencyId = newConversion.ToCurrencyId,
                ToCurrency = toCurrency,
            };

            user.ConversionHistory.Add(conversionHistory);
            _userRepository.UpdateUser(user);
        }
    }
}
