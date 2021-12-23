using System;
using Technovert.BankApp.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Technovert.BankApp.Models
{
    public class Transaction
    {
        [Key]
        public string TransactionId { get; set; }
        public string SourceAccountId { get; set; }
        public string SourceBankId { get; set; }
        public string DestinationAccountId { get; set; }
        public Account SourceAccount { get; set; }
        public Account DestinationAccount { get; set; }
        public string DestinationBankId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public TransactionMode TransactionMode { get; set; }
        public Currency Currency { get; set; }
        public DateTime On { get; set; }

    }
}