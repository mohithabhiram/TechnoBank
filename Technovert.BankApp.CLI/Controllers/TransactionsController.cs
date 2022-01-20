using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models.Enums;

namespace Technovert.BankApp.CLI.Controllers
{
    class TransactionsController

    {
        private TransactionService transactionService;
        private AccountService accountService;
        private BankService bankService;

        public TransactionsController(TransactionService transactionService, AccountService accountService, BankService bankService)
        {
            this.transactionService = transactionService;
            this.accountService = accountService;
            this.bankService = bankService;
        }
        public string Withdraw(string bankId, string accountId, decimal amount)
        {
            string id = "";
            try
            {
                if(amount<0 || amount>accountService.GetBalance(bankId,accountId))
                {
                    throw new BalanceException("Insufficient Balance or Invalid Amount");
                }
                id = transactionService.AddTransaction(bankId, accountId, bankId, "USER".PadRight(9), amount, TransactionType.Withdraw, TransactionMode.Standard);
            }
            catch (BalanceException)
            {
                Console.WriteLine("Insufficient Balance or InvalidAmount");
            }
            catch (Exception)
            {
                Console.WriteLine("Internal Error");

            }
            return id;
        }
        public string Deposit(string bankId, string accountId)
        {
            Console.WriteLine("List of currencies accepted by the bank");
            foreach (Currency currency in bankService.GetBank(bankId).Currencies)
            {
                Console.WriteLine(currency.Code+"-->"+currency.Name);
            }
            Console.WriteLine("Enter currency code");
            string currencyCode = Console.ReadLine();
            if (bankService.GetBank(bankId).Currencies.SingleOrDefault(c => c.Code == currencyCode) == null)
                throw new InvalidCurrencyException("Currency does not exist");
            Console.WriteLine("Enter amount to deposit:");
            decimal amount = Convert.ToDecimal(Console.ReadLine());
            amount = transactionService.ConvertToDefaultCurrency(currencyCode, amount, bankId);
            string id = "";
            try
            {
                if(amount<0)
                {
                    throw new BalanceException("Invalid Amount");
                }

                id = transactionService.AddTransaction(bankId, "USER".PadRight(9), bankId, accountId, amount, TransactionType.Deposit, TransactionMode.Standard);
            }
            catch (BalanceException)
            {
                Console.WriteLine("Invalid Amount");
            }
            catch (Exception)
            {
                Console.WriteLine("Internal Error");

            }
            return id;
        }
        public string Transfer(string sourceBankId, string sourceAccountId, string destinationBankId, string destinationAccountId, decimal amount, TransactionMode transactionMode)
        {
            string id = "";
            try
            {
                if (amount < 0 || amount > accountService.GetBalance(sourceBankId, sourceAccountId))
                {
                    throw new BalanceException("Insufficient Balance or Invalid Amount");
                }
                id = transactionService.AddTransaction(sourceBankId, sourceAccountId, destinationBankId, destinationAccountId, amount, TransactionType.Transfer, transactionMode);
            }
            catch (BalanceException)
            {
                Console.WriteLine("Insufficient Balance or Invalid Amount");
            }
            catch (Exception)
            {
                Console.WriteLine("Internal Error");

            }
            return id;
        }
        public void DisplayTransactions(string userAccountId,string userBankId,List<Transaction> hist,AccountsController accountsController)
        {
            Console.WriteLine("       TransactionId           | Source Bank | Source Account | Dest Bank | Dest Account |   Type      Amount  ");
            Console.WriteLine("----------------------------------------------------------------------------------------------------------------");
            foreach (Transaction t in hist)
            {
                Console.WriteLine($" {t.TransactionId} |  {t.SourceBankId}  |   {t.SourceAccountId}    | {t.DestinationBankId} |  {t.DestinationAccountId}   | {t.Type}      {t.Amount}   ");
            }
        }
        public TransactionMode GetTransactionMode(decimal amount)
        {
            if (amount > 200000)
                return TransactionMode.RTGS;
            else
                return TransactionMode.IMPS;
        }
        public void RevertTransaction(string userAccountId, string userBankId,List<Transaction> hist)
        {
            Console.WriteLine("Select a Transaction Id to revert the transaction:");
            string transactionId = Console.ReadLine();
            Transaction transaction = hist.SingleOrDefault(t => t.TransactionId == transactionId);
            if (transaction == null)
                throw new TransactionIdException("Invalid Transaction Id");
            decimal balance;
            try
            {
                if (transaction.Type != TransactionType.Withdraw)
                {
                    if (transaction.Type == TransactionType.Deposit)
                    {
                        balance = accountService.GetBalance(userBankId, userAccountId) - transaction.Amount;
                        accountService.UpdateBalance(userBankId, userAccountId, balance);
                    }
                    else
                    {
                        balance = accountService.GetBalance(userBankId, userAccountId);
                        accountService.UpdateBalance(userBankId, userAccountId, balance + transaction.Amount);
                        decimal destinationAccountBalance = accountService.GetBalance(transaction.DestinationBankId, transaction.DestinationAccountId);
                        accountService.UpdateBalance(transaction.DestinationBankId, transaction.DestinationAccountId, destinationAccountBalance - transaction.Amount);
                    }
                }
                else
                {
                    Console.WriteLine("Withdrawals cannot be reverted");
                }
            }
            catch(Exception)
            {
                Console.WriteLine("Internal Error");
            }
            hist.RemoveAll(t => t.TransactionId == transactionId);
            transactionService.RemoveTransaction(userBankId, userAccountId, transactionId);
        }
    }
}

