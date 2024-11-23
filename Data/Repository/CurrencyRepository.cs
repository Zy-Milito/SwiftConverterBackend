using Data.Entities;

namespace Data.Repository
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ApplicationContext _context;
        public CurrencyRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<Currency> GetAll()
        {
            return _context.Currencies.ToList();
        }

        public Currency? GetByCode(string code)
        {
            return _context.Currencies.FirstOrDefault(c => c.ISOCode == code && c.Status == true);
        }

        public void AddCurrency(Currency newCurrency)
        {
            newCurrency.Id = _context.Currencies.Count() + 1;
            _context.Currencies.Add(newCurrency);
            _context.SaveChanges();
        }

        public void UpdateCurrency(Currency currency)
        {
            _context.Currencies.Update(currency);
            _context.SaveChanges();
        }
    }
}
