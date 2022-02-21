using Technovert.BankApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technovert.BankApp.Models
{
    public class Account
    {
        [Key]
        public string AccountId { get; set; }
        [Required]
        public string BankId { get; set; }
        [ForeignKey("BankId")]
        public Bank Banks { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public decimal Balance { get; set; }
        //public Currency Currency { get; set; }
        public AccountType Type { get; set; }
        public Status Status { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

    }
}