using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.DTOs.Account
{
    public class AuthenticateAccountDTO
    {
        public string AccountId { get; set; }
        public string Password { get; set; }
    }
}

