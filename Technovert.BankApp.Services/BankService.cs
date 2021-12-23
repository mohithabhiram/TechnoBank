using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Services.Interfaces;

namespace Technovert.BankApp.Services
{
    public class BankService : IBankService
    {
        private readonly BankDbContext _cxt;
        //private DataStore dataStore;
        public BankService(BankDbContext bankDbContext)
        {
            _cxt = bankDbContext;
        }
        public string GenerateBankId(string name)
        {
            DateTime d = DateTime.Now;
            string date = DateTime.Now.ToString("ddMMyy");
            return name.Substring(0, 3) + date;
        }

        //public string AddBank(string name)
        //{
        //    if(dataStore.Banks.Any(m=> m.Name==name))
        //    {
        //        throw new Exception("Bank Exists with this name!");
        //    }
        //    Account manager = new Account
        //    {
        //        AccountId = BankConstants.StaffName,
        //        Name = BankConstants.StaffName,
        //        Password = BankConstants.StaffName,
        //        Balance = 0m,
        //        Gender = Models.Enums.Gender.Male,
        //        Status = Models.Enums.Status.Active,
        //        Type = Models.Enums.AccountType.BankStaff,
        //        Transactions = new List<Transaction>()
        //    };
        //    Bank bank = new Bank
        //    {
        //        BankId = GenerateBankId(name),
        //        Name = name,
        //        CreatedBy = "Mohith",
        //        CreatedOn = DateTime.Now,
        //        UpdatedBy = "Mohith",
        //        UpdatedOn = DateTime.Now,
        //        Accounts = new List<Account>(),
        //        Currencies = new List<Currency>(),
        //        DefaultCurrency = dataStore.Currencies.SingleOrDefault(c => c.Code == "INR"),
        //        RTGSToSame = 0m,
        //        IMPSToSame = 0.05m,
        //        RTGSToOther = 0.02m,
        //        IMPSToOther = 0.06m
        //    };
        //    bank.Currencies.Add(dataStore.Currencies.SingleOrDefault(c => c.Code == "INR"));
        //    bank.Accounts.Add(manager);
        //    dataStore.Banks.Add(bank);
        //    return bank.BankId;
        //}
        public Bank GetBank(string bankId)
        {

            //Bank b = dataStore.Banks.SingleOrDefault(b => b.BankId == bankId);
            //if (b == null)
            //{
            //    throw new BankIdException("Bank does not exist" );
            //}
            //return b;
            Bank bank = _cxt.Banks.SingleOrDefault(b => (b.BankId == bankId));
            return bank;
        }
        public void UpdateServiceChargesForSameBank(decimal RTGS, decimal IMPS, string bankId)
        {
            Bank bank = GetBank(bankId);
            bank.RTGSToSame = RTGS;
            bank.IMPSToSame = IMPS;

        }
        public void UpdateServiceChargesForOtherBanks(decimal RTGS, decimal IMPS, string bankId)
        {
            Bank bank = GetBank(bankId);
            bank.RTGSToOther = RTGS;
            bank.IMPSToOther = IMPS;
        }
        //public void AddCurrency(string bankId,String code)
        //{
        //    Bank bank = GetBank(bankId);
        //    Currency c = dataStore.Currencies.SingleOrDefault(c => c.Code == code);
        //    if(c == null)
        //    {
        //        throw new InvalidCurrencyException("Currency does not exist");
        //    }
        //    Currency currency = new Currency
        //    {
        //        Name = dataStore.Currencies.SingleOrDefault(c => c.Code == code).Name,
        //        Code = code,
        //        ExchangeRate = dataStore.Currencies.SingleOrDefault(c => c.Code == code).ExchangeRate,
        //    };
        //    bank.Currencies.Add(currency);
        //}

        public Bank CreateBank(Bank bank)
        {
            _cxt.Banks.Add(bank);
            _cxt.SaveChanges();
            return bank;   
        }

        public Bank UpdateBank(Bank bank)
        {
            throw new NotImplementedException();
        }

        public Bank DeleteBank(string bankId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bank> GetAllBanks()
        {
            return _cxt.Banks.Include(b => b.Accounts).ToList();
            
        }
    }
}