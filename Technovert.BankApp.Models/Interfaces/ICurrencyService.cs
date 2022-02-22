using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Interfaces
{
    public interface ICurrencyService
    {
        public Currency GetCurrency(string Code);
        public IEnumerable<Currency> GetAllCurrencies();

    }
}
