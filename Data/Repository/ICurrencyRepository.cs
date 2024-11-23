using Data.Entities;

namespace Data.Repository
{
    public interface ICurrencyRepository
    {
        List<Currency> GetAll();
        Currency? GetByCode(string code);
        void AddCurrency(Currency newCurrency);
        void UpdateCurrency(Currency currency);
    }
}
