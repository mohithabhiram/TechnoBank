using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.Models.Enums;

namespace Technovert.BankApp.API.DTOs.Transaction
{
    public class GetTransactionDTO
    {
        public string TransactionId { get; set; }
        public string SourceBankId { get; set; }
        public string SourceAccountId { get; set; }
        public string DestinationBankId { get; set; }
        public string DestinationAccountId { get; set; }
        public decimal Amount { get; set; }
        public decimal TransactionCharges { get; set; }
        public DateTime On { get; set; }
        public TransactionMode TransactionMode { get; set; }
        public TransactionType Type { get; set; }
    }
}
