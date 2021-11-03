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
        public List<Currency> Currencies { get; set; }
        public DataStore()
        {
            Banks = new List<Bank>();
            Currencies = new List<Currency>();
            Currencies.Add(new Currency { Name = "Rupee", Code = "INR", ExchangeRate = 1 });
            Currencies.Add(new Currency { Name = "Canadian dollar", Code = "CAD", ExchangeRate = 60 });
            Currencies.Add(new Currency { Name = "US dollar", Code = "USD", ExchangeRate = 75 });
        }
        //public static List<Account> Accounts { get; set; }
        //public static List<Transaction> Transactions { get; set; }

    }
}
