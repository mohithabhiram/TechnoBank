using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Models.Enums;

namespace Technovert.BankApp.Services
{
    public  class TransactionService
    {
        public AccountService accountService;
        public BankService bankService;
        public TransactionService(AccountService accountService,BankService bankService)
        {
            this.accountService = accountService;
            this.bankService = bankService;
        }
        public string AddTransaction(string sourceBankId, string sourceAccountId, string destinationBankId, string destinationAccountId, decimal amount, TransactionType transactionType, TransactionMode transactionMode)

        {
            decimal rateOfCharges = 0m;
            if(transactionMode == TransactionMode.RTGS)
            {
                if(sourceBankId == destinationBankId)
                {
                    rateOfCharges = bankService.GetBank(sourceBankId).RTGSToSame;
                }
                else
                {
                    rateOfCharges = bankService.GetBank(sourceBankId).RTGSToOther;
                }

            }
            else if(transactionMode == TransactionMode.IMPS)
            {
                if(sourceBankId ==destinationBankId)
                {
                    rateOfCharges = bankService.GetBank(sourceBankId).IMPSToSame;
                }
                else
                {
                    rateOfCharges = bankService.GetBank(sourceBankId).IMPSToOther;
                }

            }
            else
            {
                rateOfCharges = 0;
            }
            decimal totalCharges = amount * rateOfCharges;
            //withdraw
            if(transactionType == TransactionType.Withdraw)
            {
                accountService.UpdateBalance(sourceBankId, sourceAccountId, accountService.GetAccount(sourceBankId, sourceAccountId).Balance - amount);
            }
            //deposit
            else if(transactionType == TransactionType.Deposit)
            {
                accountService.UpdateBalance(destinationBankId, destinationAccountId, accountService.GetAccount(destinationBankId, destinationAccountId).Balance + amount);
            }
            //transfer
            else
            {
                accountService.UpdateBalance(sourceBankId, sourceAccountId, accountService.GetAccount(sourceBankId, sourceAccountId).Balance - (amount + totalCharges));
                accountService.UpdateBalance(destinationBankId, destinationAccountId, accountService.GetAccount(destinationBankId, destinationAccountId).Balance + amount);
            }
            Transaction transaction = new Transaction
            {
                TransactionId = GenerateTransactionId(sourceBankId, sourceAccountId, destinationBankId, destinationAccountId,transactionType),
                SourceAccountId = sourceAccountId,
                SourceBankId = sourceBankId,
                DestinationAccountId = destinationAccountId,
                DestinationBankId = destinationBankId,
                Amount = amount,
                Type = transactionType,
                TransactionMode = transactionMode,
                On = DateTime.Now
            };
            if (transactionType == TransactionType.Deposit)
            {
                accountService.GetAccount(destinationBankId, destinationAccountId).Transactions.Add(transaction);
            }
            else if (transactionType == TransactionType.Withdraw)
            {
                accountService.GetAccount(sourceBankId, sourceAccountId).Transactions.Add(transaction);
            }
            else
            {
                accountService.GetAccount(sourceBankId, sourceAccountId).Transactions.Add(transaction);
                accountService.GetAccount(destinationBankId, destinationAccountId).Transactions.Add(transaction);

            }
            return transaction.TransactionId;
        }


        //Still needs some checking with existing Ids and adding milliseconds
        public string GenerateTransactionId(string sourceBankId, string sourceAccountId, string destinationBankId, string destinationAccountId,TransactionType transactionType)
        {
            DateTime d = DateTime.Now;
            string date = DateTime.Now.ToString("ddMMyy");
            //for deposit
            if (transactionType == TransactionType.Deposit)
            {
                return "TXN" + destinationBankId + destinationAccountId + date;
            }
            //for withdraw and transfer
            return "TXN"+sourceBankId+sourceAccountId+date;
        }
        public decimal ConvertToDefaultCurrency(string currencyCode, decimal amount, string bankId)
        {
            if(currencyCode!="INR")
            {
                Currency currency = bankService.GetBank(bankId).Currencies.SingleOrDefault(c => c.Code == currencyCode);
                return amount * currency.ExchangeRate;
            }
            return amount;
        }


        public Transaction GetTransaction(string bankId, string accountId, string TransactionId)
        {
            Account account = accountService.GetAccount(bankId, accountId);
            var transaction = account.Transactions.SingleOrDefault(t => t.TransactionId == TransactionId);
            return transaction;
        }

    }
}
