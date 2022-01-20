using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Enums;

namespace Technovert.BankApp.Services.Interfaces
{
    public interface ITransactionService
    {
        public Transaction CreateTransaction(Transaction transaction);
        public Transaction UpdateTransaction(Transaction transaction);
        public Transaction DeleteTransaction(Transaction transaction);
        public Transaction GetTransaction(string transactionId);
        public IEnumerable<Transaction> GetAllTransactions();
        public string GenerateTransactionId(string sourceBankId, string sourceAccountId, string destinationBankId, string destinationAccountId, TransactionType transactionType);
    }
}
