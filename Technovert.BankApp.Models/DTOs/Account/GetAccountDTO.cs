using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.Models.Enums;

namespace Technovert.BankApp.Models.DTOs.Account
{
    public class GetAccountDTO
    {
        public string AccountId { get; set; }
        public string BankId { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; }
        public AccountType Type { get; set; }

    }
}
