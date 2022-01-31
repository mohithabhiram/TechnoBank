using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.Models.Enums;

namespace Technovert.BankApp.Models.DTOs.Account
{
    public class UpdateAccountDTO
    {
        public string Name { get; set; }
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        public Gender Gender { get; set; }
        public Status Status { get; set; }
    }
}
