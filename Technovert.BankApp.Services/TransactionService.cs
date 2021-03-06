using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.DTOs.Transaction;
using Technovert.BankApp.Models.Enums;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Models.Interfaces;

namespace Technovert.BankApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankDbContext _ctx;
        private readonly IMapper _mapper;

        private readonly IBankService _bankService;
        private readonly IAccountService _accountService;
        private readonly ICurrencyService _currencyService;

        public TransactionService(BankDbContext bankDbContext,IMapper mapper, IBankService bankService, IAccountService accountService, ICurrencyService currencyService)
        {
            _ctx = bankDbContext;
            _mapper = mapper;
            _bankService = bankService;
            _accountService = accountService;
            _currencyService = currencyService;
        }

        public Transaction CreateTransaction(string bankId, string accountId, CreateTransactionDTO nT)
        {
            var t = _mapper.Map<Transaction>(nT);
            t.TransactionId = GenerateTransactionId(bankId, accountId, nT.DestinationBankId, nT.DestinationAccountId, nT.Type);
            t.SourceAccountId = accountId;
            t.SourceBankId = bankId;
            t.SourceAccount = _accountService.GetAccount(bankId, accountId);
            t.DestinationAccount = _accountService.GetAccount(nT.DestinationBankId, nT.DestinationAccountId);
            t.TransactionMode = nT.TransactionMode;
            t.On = DateTime.Now;

            _ctx.Transactions.Add(t);
            _ctx.SaveChanges();
            return _ctx.Transactions.FirstOrDefault(tr => tr.TransactionId == t.TransactionId);
        }

        public Transaction DeleteTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _ctx.Transactions
                .Include(t => t.SourceAccount)
                .Include(t => t.DestinationAccount)
                .ToList();
        }

        public Transaction GetTransaction(string transactionId)
        {
            var transaction = _ctx.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
            return transaction;
        }

        public Transaction UpdateTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }
        public string GenerateTransactionId(string sourceBankId, string sourceAccountId, string destinationBankId, string destinationAccountId, TransactionType transactionType)
        {
            
            string timestamp = DateTime.Now.ToString("ddMMyys");
            string id;
            //for deposit
            if (transactionType == TransactionType.Deposit)
            {
                id = ("TXN" + destinationBankId + destinationAccountId + timestamp);
                return id;
            }
            //for withdraw and transfer
            return ("TXN" + sourceBankId + sourceAccountId + timestamp);
        }

        public decimal ConvertToDefaultCurrency(string code, decimal amount)
        {
            var currency = _currencyService.GetCurrency(code);
            return amount * currency.ExchangeRateWRTDefaultCurrency;
        }

        public CreateTransactionDTO UpdateBalance(string bankId, string accountId, CreateTransactionDTO newTransaction)
        {
            decimal rateOfCharges = 0m;
            if (newTransaction.CurrencyCode == "")
                newTransaction.CurrencyCode = BankConstants.DefaultCurrencyCode;
            if (newTransaction.Type == TransactionType.Deposit && newTransaction.CurrencyCode != BankConstants.DefaultCurrencyCode)
                newTransaction.Amount = ConvertToDefaultCurrency(newTransaction.CurrencyCode, newTransaction.Amount);
            if (newTransaction.TransactionMode == TransactionMode.RTGS)
            {
                if (bankId == newTransaction.DestinationBankId)
                {
                    rateOfCharges = _bankService.GetBank(bankId).RTGSToSame;
                }
                else
                {
                    rateOfCharges = _bankService.GetBank(bankId).RTGSToOther;
                }

            }
            else if (newTransaction.TransactionMode == TransactionMode.IMPS)
            {
                if (bankId == newTransaction.DestinationBankId)
                {
                    rateOfCharges = _bankService.GetBank(bankId).IMPSToSame;
                }
                else
                {
                    rateOfCharges = _bankService.GetBank(bankId).IMPSToOther;
                }
            }
            else
            {
                rateOfCharges = 0;
            }
            decimal totalCharges = newTransaction.Amount * rateOfCharges;
            decimal accBal = _accountService.GetAccount(bankId, accountId).Balance;
            //withdraw
            if (newTransaction.Type == TransactionType.Withdraw && accBal>newTransaction.Amount && newTransaction.CurrencyCode == BankConstants.DefaultCurrencyCode)
            {
                _accountService.UpdateBalance(bankId, accountId, accBal - newTransaction.Amount);
            }
            //deposit
            else if (newTransaction.Type == TransactionType.Deposit)
            {

                _accountService.UpdateBalance(newTransaction.DestinationBankId, newTransaction.DestinationAccountId, _accountService.GetAccount(newTransaction.DestinationBankId, newTransaction.DestinationAccountId).Balance + newTransaction.Amount);
            }
            //transfer
            else if (newTransaction.Type == TransactionType.Transfer && accBal>newTransaction.Amount && newTransaction.CurrencyCode == BankConstants.DefaultCurrencyCode)
            {
                _accountService.UpdateBalance(bankId, accountId, accBal - (newTransaction.Amount + totalCharges));
                _accountService.UpdateBalance(newTransaction.DestinationBankId, newTransaction.DestinationAccountId, _accountService.GetAccount(newTransaction.DestinationBankId, newTransaction.DestinationAccountId).Balance + newTransaction.Amount);
            }
            else
            {
                return null;
            }


            return newTransaction;
        }
    }
}
