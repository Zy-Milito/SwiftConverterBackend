using Common.DTO;

namespace Services
{
    public interface ICurrencyService
    {
        List<CurrencyForView> GetAllCurrencies();
        CurrencyForView? GetCurrency(string code);
        void AddCurrency(CurrencyForCreation currencyForCreation);
        bool NewRate(double newRate, string ISOCode);
        bool RestoreCurrency(string ISOCode);
        bool RemoveCurrency(string ISOCode);
    }
}
