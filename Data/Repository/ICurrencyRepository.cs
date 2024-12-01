using Data.Entities;

namespace Data.Repository
{
    public interface ICurrencyRepository
    {
        List<Currency> GetAll();
        Currency? GetByCode(string code);
        Currency? GetById(int id);
        void AddCurrency(Currency newCurrency);
        void UpdateCurrency(Currency currency);
    }
}
