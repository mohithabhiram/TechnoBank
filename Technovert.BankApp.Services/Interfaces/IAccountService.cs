using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;

namespace Technovert.BankApp.Services.Interfaces
{
    public interface IAccountService
    {
        public Account GetAccount(string bankId, string acccountId);
        public Account CreateAccount(Account account);
        public Account UpdateAccount(Account account);
        public Account DeleteAccount(string bankId, string accountid);
        public IEnumerable<Account> GetAllAccounts(string bankId);
        public string GenerateAccountId(string name);
        public void UpdateBalance(string bankId, string acccountId, decimal balance);
    }
}
