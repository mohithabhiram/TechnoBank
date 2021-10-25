using Technovert.BankApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Services
{
    public class  DataStore
    {
        public List<Bank> Banks { get; set; }
        public DataStore()
        {
            Banks = new List<Bank>();
        }
        //public static List<Account> Accounts { get; set; }
        //public static List<Transaction> Transactions { get; set; }

    }
}
