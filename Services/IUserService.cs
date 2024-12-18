﻿using Common.DTO;
using Data.Entities;

namespace Services
{
    public interface IUserService
    {
        List<UserForView> GetAllUsers();
        UserForView? GetUser(int id);
        List<CurrencyForView> GetFavoriteCurrencies(int id);
        List<HistoryForView> GetHistoryById(int id);
        void AddUser(UserForCreation userForCreation);
        User? ValidateUser(UserForLogin loginData);
        void RemoveUser(int id);
        void UpgradePlan(int id, string newPlanName);
        void AddConversionHistory(int id, ConversionForCreation newConversion);
        void ToggleFavoriteCurrency(int userId, string code);
    }
}
