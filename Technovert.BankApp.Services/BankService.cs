using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;

namespace Technovert.BankApp.Services
{
    public class BankService
    {
        private DataStore dataStore;
        public BankService(DataStore datastore)
        {
            this.dataStore = datastore;
        }
        public string GenerateBankId(string name)
        {
            DateTime d = DateTime.Now;
            string date = DateTime.Now.ToString("ddMMyy");
            return name.Substring(0, 3) + date;
        }

        public string AddBank(string name)
        {
            if(dataStore.Banks.Any(m=> m.Name==name))
            {
                throw new Exception("Bank Exists with this name!");
            }
            Bank bank = new Bank
            {
                BankId = GenerateBankId(name),
                Name = name,
                CreatedBy = "Mohith",
                CreatedOn = DateTime.Now,
                UpdatedBy = "Mohith",
                UpdatedOn = DateTime.Now,
                Accounts = new List<Account>(),
                Currencies = new List<Currency>(),
                RTGSToSame = 0m,
                IMPSToSame = 0.05m,
                RTGSToOther = 0.02m,
                IMPSToOther = 0.06m
            };
            dataStore.Banks.Add(bank);
            return bank.BankId;
        }
        public Bank GetBank(string bankId)
        {

            return dataStore.Banks.SingleOrDefault(b => b.BankId == bankId);
        }
        public void UpdateServiceChargesForSameBank(string bankId, decimal RTGS, decimal IMPS)
        {
            Bank bank = GetBank(bankId);
            bank.RTGSToSame = RTGS;
            bank.IMPSToSame = IMPS;

        }
        public void UpdateServiceChargesForOtherBanks(string bankId, decimal RTGS, decimal IMPS)
        {
            Bank bank = GetBank(bankId);
            bank.RTGSToOther = RTGS;
            bank.IMPSToOther = IMPS;
        }
        public void AddCurrency(string bankId, string name, String code, decimal exchangeRate)
        {
            Bank bank = GetBank(bankId);
            Currency currency = new Currency
            {
                Name = name,
                Code = code,
                ExchangeRate = exchangeRate,
            };
            bank.Currencies.Add(currency);
        }



    }
}