using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.DTOs.Account;

namespace Technovert.BankApp.Models.Interfaces
{
    public interface IAccountService
    {
        public Account GetAccount(string bankId, string acccountId);
        public Account CreateAccount(Account account, string bankId);
        public Account UpdateAccount(string bankId, string accountId,UpdateAccountDTO accountDTO);
        public Account DeleteAccount(string bankId, string accountid);
        public IEnumerable<Account> GetAllAccounts(string bankId);
        public string GenerateAccountId(string name);
        public void UpdateBalance(string bankId, string acccountId, decimal balance);
        public string Authenticate(string accountId, string password);
    }
}
