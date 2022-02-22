using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.DTOs.Bank
{
    public class CreateBankDTO
    {
        [Required]
        public string Name { get; set; }
        public decimal RTGSToSame { get; set; }
        public decimal RTGSToOther { get; set; }
        public decimal IMPSToSame { get; set; }
        public decimal IMPSToOther { get; set; }
        public string? CreatedBy { get; set; }
    }
}
