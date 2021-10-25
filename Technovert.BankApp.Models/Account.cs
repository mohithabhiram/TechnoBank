using Technovert.BankApp.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models
{
    public class Account
    {
        public string AccountId { get; set; }
        public string BankId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public Gender Gender { get; set; }
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }
        public AccountType Type { get; set; }
        public Status Status { get; set; }
        public List<Transaction> Transactions { get; set; }

    }
}