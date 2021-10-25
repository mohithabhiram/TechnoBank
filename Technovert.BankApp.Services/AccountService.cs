using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Enums;
using Technovert.BankApp.Models.Exceptions;

namespace Technovert.BankApp.Services
{
    public class AccountService
    {
        public BankService bankService;
        public AccountService(BankService bankService)
        {
            this.bankService = bankService;
        }
        public string AddAccount(String name, String password, string bankId, Gender gender, AccountType type)
        {
            Account account = new Account
            {
                AccountId = GenerateAccountId(name),
                BankId = bankId,
                Type = type,
                Name = name,
                Password = password,
                Balance = 0m,
                Gender = gender,
                Status = Models.Enums.Status.Active,
                Transactions = new List<Transaction>()
            };
            bankService.GetBank(bankId).Accounts.Add(account);
            return account.AccountId;
        }
        public string GenerateAccountId(string name)
        {
            DateTime d = DateTime.Now;
            string date = DateTime.Now.ToString("ddMMyy");
            return name.Substring(0, 3) + date;
        }



        public Account GetAccount(string bankId, string accountId)
        {
            return bankService.GetBank(bankId).Accounts.SingleOrDefault(a => a.AccountId == accountId);
        }

        public void UpdateBalance(string bankId, string accountId, decimal balance)
        {
            Account acc = GetAccount(bankId, accountId);
            acc.Balance = balance;

        }

    }
}