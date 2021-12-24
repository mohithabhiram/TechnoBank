using Microsoft.EntityFrameworkCore;
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
    public class TransactionService : ITransactionService
    {
        private readonly BankDbContext _cxt;

        public TransactionService(BankDbContext bankDbContext)
        {
            _cxt = bankDbContext;
        }

        public Transaction CreateTransaction(Transaction transaction)
        {
            _cxt.Transactions.Add(transaction);
            _cxt.SaveChanges();
            return _cxt.Transactions.FirstOrDefault(t => t.TransactionId == transaction.TransactionId);
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
            /*string timestamp = DateTime.UtcNow.ToString("ddhmsf",
                                        System.Globalization.CultureInfo.InvariantCulture);*/
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
    }
}
