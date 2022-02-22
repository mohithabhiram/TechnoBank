using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Interfaces;

namespace Technovert.BankApp.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly BankDbContext _ctx;

        public CurrencyService(BankDbContext appDbContext)
        {
            _ctx = appDbContext;
        }

        public IEnumerable<Currency> GetAllCurrencies()
        {
            return _ctx.Currencies.ToList();
           
        }

        public Currency GetCurrency(string code)
        {
            return _ctx.Currencies.FirstOrDefault(c => c.Code == code);
        }
    }
}
