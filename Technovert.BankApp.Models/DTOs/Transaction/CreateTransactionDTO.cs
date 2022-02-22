using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.Models.Enums;

namespace Technovert.BankApp.Models.DTOs.Transaction
{
    public class CreateTransactionDTO
    {
        [Required]
        public string DestinationAccountId { get; set; }
        [Required]
        public string DestinationBankId { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required(AllowEmptyStrings = true)]
        public string CurrencyCode { get; set; }
        [Required]
        public TransactionMode TransactionMode { get; set; }
        [Required]
        public TransactionType Type { get; set; }
    }
}
