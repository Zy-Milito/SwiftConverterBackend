using Common.DTO;

namespace Services
{
    public interface ICurrencyService
    {
        List<CurrencyForView> GetAllCurrencies();
        CurrencyForView? GetCurrency(string code);
        void AddCurrency(CurrencyForCreation currencyForCreation);
    }
}
