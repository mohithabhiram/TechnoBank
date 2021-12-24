using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Enums;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Services.Interfaces;

namespace Technovert.BankApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankDbContext _cxt;
        //public BankService bankService;
        public AccountService(BankDbContext bankDbContext)
        {
            _cxt = bankDbContext;
        }
        //public string AddAccount(String name, String password, string bankId, Gender gender, AccountType type)
        //{
        //    Account account = new Account
        //    {
        //        AccountId = GenerateAccountId(name),
        //        BankId = bankId,
        //        Type = type,
        //        Name = name,
        //        Password = password,
        //        Balance = 0m,
        //        Gender = gender,
        //        Status = Models.Enums.Status.Active,
        //        Transactions = new List<Transaction>()
        //    };
        //    bankService.GetBank(bankId).Accounts.Add(account);
        //    return account.AccountId;
        //}
        public string GenerateAccountId(string name)
        {
            DateTime d = DateTime.Now;
            string date = DateTime.Now.ToString("ddMMyy");
            return name.Substring(0, 3) + date;
        }

        public Account GetAccount(string bankId, string accountId)
        {
            //try
            //{
            //    Account acc = bankService.GetBank(bankId).Accounts.SingleOrDefault(a => a.AccountId == accountId);
            //    return acc;
            //}
            //catch(AccountNumberException)
            //{
            //    Console.WriteLine("Account does not exist");
            //}
            //return null; 
            return _cxt.Accounts.FirstOrDefault(a => (a.AccountId == accountId) && (a.BankId == bankId));
        }

        //public void UpdateBalance(string bankId, string accountId, decimal balance)
        //{
        //    Account acc = GetAccount(bankId, accountId);
        //    acc.Balance = balance;

        //}
        //public bool DeleteAccount(string bankId, string accountId)
        //{
        //    Account account = GetAccount(bankId, accountId);
        //    try
        //    {
        //        if (account != null)
        //        {
        //            bankService.GetBank(bankId).Accounts.Remove(account);
        //            return true;
        //        }
        //        else
        //        {
        //            throw new AccountNumberException("Account does not exist");
        //        }
        //    }
        //    catch(AccountNumberException e)
        //    {
        //        Console.WriteLine(e.Message);
        //    }
        //    return false;
        //}
        //public decimal GetBalance(string bankId,string accountId)
        //{
        //    return GetAccount(bankId, accountId).Balance;
        //}
        //public bool ValidateUser(string bankId, string accountId, string password)
        //{
        //    Account acc = GetAccount(bankId, accountId);
        //    if (acc.Password != password)
        //    {
        //        throw new PasswordIncorrectException("Password Entered is Incorrect");
        //    }
        //    return true;
        //}
        //public bool ValidateStaff(string bankId, string accountId, string password)
        //{
        //    Account acc = GetAccount(bankId, accountId);
        //    if (acc.Type != AccountType.BankStaff)
        //    {
        //        throw new Models.Exceptions.UnauthorizedAccessException("You are not Authorized to Access this Page");
        //    }
        //    if (acc.Password != password)
        //    {
        //        throw new PasswordIncorrectException("Password Entered is Incorrect");
        //    }
        //    return true;
        //}
        //public void UpdateAccount(string bankId,string accountId,string name,string password)
        //{
        //    Account acc = GetAccount(bankId, accountId);
        //    acc.Name = name;
        //    acc.Password = password;
        //}

        public Account CreateAccount(Account account)
        {
            _cxt.Accounts.Add(account);
            _cxt.SaveChanges();
            var createdAccount = _cxt.Accounts.FirstOrDefault(a => a.AccountId == account.AccountId);
            return createdAccount;

        }

        public Account UpdateAccount(Account account)
        {
            throw new NotImplementedException();
        }

        Account IAccountService.DeleteAccount(string bankId, string accountid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAllAccounts(string bankId)
        {
            return _cxt.Accounts.Where(a => a.BankId == bankId).ToList();
        }

        public void UpdateBalance(string bankId, string accountId, decimal balance)
        {
            var acc = _cxt.Accounts.FirstOrDefault(a => (a.AccountId == accountId) && (a.BankId == bankId));
            acc.Balance = balance;
        }
    }
}