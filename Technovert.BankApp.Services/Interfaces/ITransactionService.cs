using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Technovert.BankApp.Services.Interfaces
{
    public interface ITransactionService
    {
        public Transaction CreateTransaction(Transaction transaction);
        public Transaction UpdateTransaction(Transaction transaction);
        public Transaction DeleteTransaction(Transaction transaction);
        public Transaction GetTransaction(string transactionId);
        public IEnumerable<Transaction> GetAllTransactions(string bankId, string accountId);
    }
}
