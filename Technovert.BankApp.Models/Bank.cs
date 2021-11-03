using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models
{
    public class Bank
    {
        public string BankId { get; set; }
        public string Name { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Currency> Currencies { get; set; }
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