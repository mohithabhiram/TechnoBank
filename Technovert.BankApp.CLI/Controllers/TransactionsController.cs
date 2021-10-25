﻿using Technovert.BankApp.Models;
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

        public TransactionsController(TransactionService transactionService, AccountService accountService)
        {
            this.transactionService = transactionService;
            this.accountService = accountService;
        }
        public string Withdraw(string bankId, string accountId, decimal amount)
        {
            string id = "";
            try
            {
                id = transactionService.AddTransaction(bankId, accountId, "", "", amount, TransactionType.Withdraw, TransactionMode.Standard);
            }
            catch (BalanceException)
            {
                Console.WriteLine("Insufficient Balance");
            }
            catch (Exception)
            {
                Console.WriteLine("Internal Error");

            }
            return id;
        }
        public string Deposit(string bankId, string accountId, decimal amount)
        {

            string id = "";
            try
            {
                id = transactionService.AddTransaction("", "", bankId, accountId, amount, TransactionType.Deposit, TransactionMode.Standard);
            }
            catch (BalanceException)
            {
                Console.WriteLine("Insufficient Balance");
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
                id = transactionService.AddTransaction(sourceBankId, sourceAccountId, destinationBankId, destinationAccountId, amount, TransactionType.Transfer, transactionMode);
            }
            catch (BalanceException)
            {
                Console.WriteLine("Insufficient Balance");
            }
            catch (Exception)
            {
                Console.WriteLine("Internal Error");

            }
            return id;
        }
    }
}

