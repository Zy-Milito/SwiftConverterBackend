using Common.DTO;
using Data.Entities;
using Data.Repository;

namespace Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public List<CurrencyForView> GetAllCurrencies()
        {
            var currencies = _currencyRepository.GetAll();

            return currencies.Where(c => c.Status == true).Select(c => new CurrencyForView()
            {
                Name = c.Name,
                Symbol = c.Symbol,
                ISOCode = c.ISOCode,
                ExchangeRate = c.ExchangeRate,
            }).ToList();
        }

        public CurrencyForView? GetCurrency(string code)
        {
            Currency? currency = _currencyRepository.GetByCode(code);

            if (currency is null)
            {
                throw new Exception("Currency not found.");
            }

            CurrencyForView currencyForView = new CurrencyForView()
            {
                Name = currency.Name,
                Symbol = currency.Symbol,
                ISOCode = currency.ISOCode,
                ExchangeRate = currency.ExchangeRate,
            };

            return currencyForView;
        }

        public void AddCurrency(CurrencyForCreation currencyForCreation)
        {
            Currency newCurrency = new Currency()
            {
                Name = currencyForCreation.Name,
                Symbol = currencyForCreation.Symbol,
                ISOCode = currencyForCreation.ISOCode,
                ExchangeRate = currencyForCreation.ExchangeRate,
            };
            _currencyRepository.AddCurrency(newCurrency);
        }

        public bool NewRate(double newRate, string ISOCode)
        {
            var currency = _currencyRepository.GetByCode(ISOCode);
            if (currency is null) return false;
            currency.ExchangeRate = newRate;
            _currencyRepository.UpdateCurrency(currency);
            return true;
        }

        public bool RemoveCurrency(string ISOCode)
        {
            var currency = _currencyRepository.GetByCode(ISOCode);
            if (currency is null || currency.Status == false)
            {
                return false;
            }
            currency.Status = false;
            _currencyRepository.UpdateCurrency(currency);
            return true;
        }
    }
}
