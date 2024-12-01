using Common.DTO;

namespace Services
{
    public interface ICurrencyService
    {
        List<CurrencyForView> GetAllCurrencies();
        CurrencyForView? GetCurrency(string code);
        void AddCurrency(CurrencyForCreation currencyForCreation);
        void NewRate(double newRate, string ISOCode);
        void RestoreCurrency(string ISOCode);
        void RemoveCurrency(string ISOCode);
    }
}
