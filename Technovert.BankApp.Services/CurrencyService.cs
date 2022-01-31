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
        private readonly BankDbContext _cxt;

        public CurrencyService(BankDbContext appDbContext)
        {
            _cxt = appDbContext;
        }
        public Currency GetCurrency(string code)
        {
            return _cxt.Currencies.FirstOrDefault(c => c.Code == code);
        }
    }
}
