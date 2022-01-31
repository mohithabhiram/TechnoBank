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
        private readonly BankDbContext _cxt;
        private readonly IMapper _mapper;

        private readonly IBankService _bankService;
        private readonly IAccountService _accountService;

        public TransactionService(BankDbContext bankDbContext,IMapper mapper, IBankService bankService, IAccountService accountService)
        {
            _cxt = bankDbContext;
            _mapper = mapper;
            _bankService = bankService;
            _accountService = accountService;
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

            _cxt.Transactions.Add(t);
            _cxt.SaveChanges();
            return _cxt.Transactions.FirstOrDefault(tr => tr.TransactionId == t.TransactionId);
        }

        public Transaction DeleteTransaction(Transaction transaction)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _cxt.Transactions
                .Include(t => t.SourceAccount)
                .Include(t => t.DestinationAccount)
                .ToList();
        }

        public Transaction GetTransaction(string transactionId)
        {
            var transaction = _cxt.Transactions.FirstOrDefault(t => t.TransactionId == transactionId);
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

        public CreateTransactionDTO UpdateBalance(string bankId, string accountId, CreateTransactionDTO newTransaction)
        {
            decimal rateOfCharges = 0m;
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
            if (newTransaction.Type == TransactionType.Withdraw && accBal>newTransaction.Amount)
            {
                _accountService.UpdateBalance(bankId, accountId, accBal - newTransaction.Amount);
            }
            //deposit
            else if (newTransaction.Type == TransactionType.Deposit)
            {

                _accountService.UpdateBalance(newTransaction.DestinationBankId, newTransaction.DestinationAccountId, _accountService.GetAccount(newTransaction.DestinationBankId, newTransaction.DestinationAccountId).Balance + newTransaction.Amount);
            }
            //transfer
            else if (newTransaction.Type == TransactionType.Transfer && accBal>newTransaction.Amount)
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
