using Technovert.BankApp.Models;
using Technovert.BankApp.Services;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.CLI.Controllers
{
    class AccountsController
    {
        private AccountService accountService;
        private Inputs inputs;

        public AccountsController(AccountService accountService, Inputs inputs)
        {
            this.accountService = accountService;
            this.inputs = inputs;
        }
        public string CreateAccount(string bankId)
        {
            string id = "";
            try
            {
                string name = inputs.GetName();
                string password = inputs.GetPassword();
                Gender gender = inputs.GetGender();
                AccountType accountType = inputs.GetAccountType();
                id = accountService.AddAccount(name, password, bankId, gender, accountType);
            }
            catch (AccountNumberException e)
            {

                Console.WriteLine("Account Number already exists");
            }
            catch (Exception e)
            {
                Console.WriteLine("Internal Error");
            }
            return id;
        }
        public Account GetAccount(string bankId, string accountId)
        {

            Account acc = accountService.GetAccount(bankId, accountId);
            if (acc == null)
            {
                throw new AccountNumberException("Account Number already exists");
            }
            return acc;
        }
        public decimal GetBalance(string bankId, string accountId)
        {
            try
            {
                Account acc = accountService.GetAccount(bankId, accountId);
                if (acc == null)
                {
                    throw new AccountNumberException("Account Number already exists");
                }
                return acc.Balance;
            }
            catch (AccountNumberException e)
            {

                Console.WriteLine("Account  does not  exist.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Internal Error");
            }
            return -1m;
        }
        public List<Transaction> GetTransactionHistory(string bankId, string accountId)
        {
            List<Transaction> transactions = new List<Transaction>();
            try
            {
                transactions = accountService.GetAccount(bankId, accountId).Transactions.ToList();
                return transactions;
            }
            catch (Exception)
            {
                Console.WriteLine("Internal Error");
            }
            return null;
        }
    }
}
