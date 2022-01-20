using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.Models;

namespace Technovert.BankApp.API.DTOs.Bank
{
    public class GetBankDTO
    {
        public string BankId { get; set; }
        public string Name { get; set; }
        public Currency DefaultCurrency { get; set; }
        public decimal RTGSToSame { get; set; }
        public decimal RTGSToOther { get; set; }
        public decimal IMPSToSame { get; set; }
        public decimal IMPSToOther { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
    }
}
