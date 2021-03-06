using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.DTOs.Currency;

namespace Technovert.BankApp.Models.Interfaces
{
    public interface IBankService
    {
        public Bank CreateBank(Bank bank);
        public Bank UpdateBank(Bank bank);
        public Bank DeleteBank(string bankId);
        public Bank GetBank(string bankId);
        public void AddCurrency(Currency currency);
        public IEnumerable<Bank> GetAllBanks();
        public string GenerateBankId(string name);

    }
}
