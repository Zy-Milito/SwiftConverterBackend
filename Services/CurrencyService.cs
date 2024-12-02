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

            return currencies.Select(c => new CurrencyForView()
            {
                Id = c.Id,
                Name = c.Name,
                Symbol = c.Symbol,
                ISOCode = c.ISOCode,
                ExchangeRate = c.ExchangeRate,
                Status = c.Status,
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
                Id = currency.Id,
                Name = currency.Name,
                Symbol = currency.Symbol,
                ISOCode = currency.ISOCode,
                ExchangeRate = currency.ExchangeRate,
                Status = currency.Status,
            };

            return currencyForView;
        }

        public void AddCurrency(CurrencyForCreation currencyForCreation)
        {
            var exists = _currencyRepository.GetByCode(currencyForCreation.ISOCode);

            if (exists != null)
            {
                throw new Exception("Currency already exists.");
            }

            Currency newCurrency = new Currency()
            {
                Name = currencyForCreation.Name,
                Symbol = currencyForCreation.Symbol,
                ISOCode = currencyForCreation.ISOCode,
                ExchangeRate = currencyForCreation.ExchangeRate,
            };
            _currencyRepository.AddCurrency(newCurrency);
        }

        public void NewRate(double newRate, string ISOCode)
        {
            var currency = _currencyRepository.GetByCode(ISOCode);
            if (currency is null)
            {
                throw new Exception("The currency specified could not be removed because it does not exist.");
            }
            currency.ExchangeRate = newRate;
            _currencyRepository.UpdateCurrency(currency);
        }

        public void RestoreCurrency(string ISOCode)
        {
            var currency = _currencyRepository.GetByCode(ISOCode);
            if (currency is null || currency.Status == true)
            {
                throw new Exception("Currency not restored: already restored or does not exist.");
            }
            currency.Status = true;
            _currencyRepository.UpdateCurrency(currency);
        }

        public void RemoveCurrency(string ISOCode)
        {
            var currency = _currencyRepository.GetByCode(ISOCode);
            if (currency is null || currency.Status == false)
            {
                throw new Exception("Currency not deleted: already removed or does not exist.");
            }
            currency.Status = false;
            _currencyRepository.UpdateCurrency(currency);
        }
    }
}
