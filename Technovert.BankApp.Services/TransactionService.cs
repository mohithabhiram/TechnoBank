using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Models.Enums;
using Technovert.BankApp.Services.Interfaces;

namespace Technovert.BankApp.Services
{
    public  class TransactionService : ITransactionService
    {
        //public AccountService accountService;
        //public BankService bankService;
        //public TransactionService(AccountService accountService,BankService bankService)
        //{
        //    this.accountService = accountService;
        //    this.bankService = bankService;
        //}
        //public string AddTransaction(string sourceBankId, string sourceAccountId, string destinationBankId, string destinationAccountId, decimal amount, TransactionType transactionType, TransactionMode transactionMode)

        //{
        //    decimal rateOfCharges = 0m;
        //    if(transactionMode == TransactionMode.RTGS)
        //    {
        //        if(sourceBankId == destinationBankId)
        //        {
        //            rateOfCharges = bankService.GetBank(sourceBankId).RTGSToSame;
        //        }
        //        else
        //        {
        //            rateOfCharges = bankService.GetBank(sourceBankId).RTGSToOther;
        //        }

        //    }
        //    else if(transactionMode == TransactionMode.IMPS)
        //    {
        //        if(sourceBankId ==destinationBankId)
        //        {
        //            rateOfCharges = bankService.GetBank(sourceBankId).IMPSToSame;
        //        }
        //        else
        //        {
        //            rateOfCharges = bankService.GetBank(sourceBankId).IMPSToOther;
        //        }

        //    }
        //    else
        //    {
        //        rateOfCharges = 0;
        //    }
        //    decimal totalCharges = amount * rateOfCharges;
        //    //withdraw
        //    if(transactionType == TransactionType.Withdraw)
        //    {
        //        accountService.UpdateBalance(sourceBankId, sourceAccountId, accountService.GetAccount(sourceBankId, sourceAccountId).Balance - amount);
        //    }
        //    //deposit
        //    else if(transactionType == TransactionType.Deposit)
        //    {
        //        accountService.UpdateBalance(destinationBankId, destinationAccountId, accountService.GetAccount(destinationBankId, destinationAccountId).Balance + amount);
        //    }
        //    //transfer
        //    else
        //    {
        //        accountService.UpdateBalance(sourceBankId, sourceAccountId, accountService.GetAccount(sourceBankId, sourceAccountId).Balance - (amount + totalCharges));
        //        accountService.UpdateBalance(destinationBankId, destinationAccountId, accountService.GetAccount(destinationBankId, destinationAccountId).Balance + amount);
        //    }
        //    Transaction transaction = new Transaction
        //    {
        //        TransactionId = GenerateTransactionId(sourceBankId, sourceAccountId, destinationBankId, destinationAccountId,transactionType),
        //        SourceAccountId = sourceAccountId,
        //        SourceBankId = sourceBankId,
        //        DestinationAccountId = destinationAccountId,
        //        DestinationBankId = destinationBankId,
        //        Amount = amount,
        //        Type = transactionType,
        //        TransactionMode = transactionMode,
        //        On = DateTime.Now
        //    };
        //    if (transactionType == TransactionType.Deposit)
        //    {
        //        accountService.GetAccount(destinationBankId, destinationAccountId).Transactions.Add(transaction);
        //    }
        //    else if (transactionType == TransactionType.Withdraw)
        //    {
        //        accountService.GetAccount(sourceBankId, sourceAccountId).Transactions.Add(transaction);
        //    }
        //    else
        //    {
        //        accountService.GetAccount(sourceBankId, sourceAccountId).Transactions.Add(transaction);
        //        accountService.GetAccount(destinationBankId, destinationAccountId).Transactions.Add(transaction);

        //    }
        //    return transaction.TransactionId;
        //}


        ////Still needs some checking with existing Ids and adding milliseconds
        //public string GenerateTransactionId(string sourceBankId, string sourceAccountId, string destinationBankId, string destinationAccountId,TransactionType transactionType)
        //{
        //    /*string timestamp = DateTime.UtcNow.ToString("ddhmsf",
        //                                System.Globalization.CultureInfo.InvariantCulture);*/
        //    string timestamp = DateTime.Now.ToString("ddMMyys");
        //    string id;
        //    //for deposit
        //    if (transactionType == TransactionType.Deposit)
        //    {
        //        id = ("TXN" + destinationBankId + destinationAccountId + timestamp).PadRight(29,'0');
        //        return id;
        //    }
        //    //for withdraw and transfer
        //    return ("TXN" + sourceBankId + sourceAccountId + timestamp).PadRight(29,'0');
        //}
        //public decimal ConvertToDefaultCurrency(string currencyCode, decimal amount, string bankId)
        //{
        //    if(currencyCode!="INR")
        //    {
        //        Currency currency = bankService.GetBank(bankId).Currencies.SingleOrDefault(c => c.Code == currencyCode);
        //        return amount * currency.ExchangeRate;
        //    }
        //    return amount;
        //}


        //public Transaction GetTransaction(string bankId, string accountId, string TransactionId)
        //{
        //    Account account = accountService.GetAccount(bankId, accountId);
        //    var transaction = account.Transactions.SingleOrDefault(t => t.TransactionId == TransactionId);
        //    return transaction;
        //}

        public System.Transactions.Transaction CreateTransaction(System.Transactions.Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public System.Transactions.Transaction UpdateTransaction(System.Transactions.Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public System.Transactions.Transaction DeleteTransaction(System.Transactions.Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public System.Transactions.Transaction GetTransaction(string transactionId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<System.Transactions.Transaction> GetAllTransactions(string bankId, string accountId)
        {
            throw new NotImplementedException();
        }
        //public void RemoveTransaction(string bankId, string accountId, string transactionId)
        //{
        //    accountService.GetAccount(bankId,accountId).Transactions.RemoveAll(t => t.TransactionId == transactionId);
        //}

    }
}
