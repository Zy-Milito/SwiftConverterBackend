﻿using Common.DTO;
using Data.Entities;
using Data.Repository;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        public UserService(IUserRepository userRepository, ICurrencyRepository currencyRepository, ISubscriptionPlanRepository subscriptionPlanRepository)
        {
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
            _subscriptionPlanRepository = subscriptionPlanRepository;
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

        public List<CurrencyForView> GetFavoriteCurrencies(int id)
        {
            var user = _userRepository.GetById(id);
            if (user is null)
            {
                throw new Exception("User not found.");
            }

            var favorites = user.FavedCurrencies.Select(c => new CurrencyForView
            {
                Id = c.Id,
                Name = c.Name,
                Symbol = c.Symbol,
                ISOCode = c.ISOCode,
                ExchangeRate = c.ExchangeRate,
                Status = c.Status,
            }).ToList();

            return favorites;
        }

        public List<HistoryForView> GetHistoryById(int id)
        {
            var user = _userRepository.GetById(id);
            if (user is null)
            {
                throw new Exception("User not found.");
            }

            var historyForView = user.ConversionHistory.Select(ch => new HistoryForView
            {
                Id = ch.Id,
                Date = ch.Date,
                UserId = ch.UserId,
                Amount = ch.Amount,
                FromCurrency = ch.FromCurrency.ISOCode,
                ToCurrency = ch.ToCurrency.ISOCode,
            }).ToList();

            return historyForView;
        }

        public void AddUser(UserForCreation userForCreation)
        {
            var exists = _userRepository.GetByUsername(userForCreation.Username);

            if (exists != null)
            {
                throw new Exception("User already exists.");
            }

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

        public void UpgradePlan(int id, string newPlanName)
        {
            var user = _userRepository.GetById(id);
            if (user is null || user.AccountStatus == false)
            {
                throw new Exception($"Plan not upgraded: user does not exist or has already subscribed to {newPlanName}.");
            }

            var newPlan = _subscriptionPlanRepository.GetByName(newPlanName);
            if (newPlan is null)
            {
                throw new Exception($"Plan not upgraded: the plan named {newPlanName} does not exist.");
            }

            if (user.SubscriptionPlan.Id == newPlan.Id)
            {
                throw new Exception($"Plan not upgraded: user has already subscribed to {newPlanName}.");
            }

            user.SubscriptionPlanId = newPlan.Id;
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
                Date = DateTime.Now.ToString("yyyy-MM-dd HH:00"),
                UserId = user.Id,
                User = user,
                Amount = newConversion.Amount,
                FromCurrencyId = newConversion.FromCurrencyId,
                FromCurrency = fromCurrency,
                ToCurrencyId = newConversion.ToCurrencyId,
                ToCurrency = toCurrency,
            };

            user.ConversionHistory.Add(conversionHistory);
            _userRepository.UpdateUser(user);
        }

        public void ToggleFavoriteCurrency(int userId, string code)
        {
            var user = _userRepository.GetById(userId);
            if (user is null)
            {
                throw new Exception("User not found.");
            }

            var currency = _currencyRepository.GetByCode(code);
            if (currency is null)
            {
                throw new Exception("Currency not found.");
            }

            var existingFavorite = user.FavedCurrencies.FirstOrDefault(c => c.ISOCode == code);
            if (existingFavorite != null)
            {
                user.FavedCurrencies.Remove(existingFavorite);
            }
            else
            {
                user.FavedCurrencies.Add(currency);
            }

            _userRepository.UpdateUser(user);
        }
    }
}
