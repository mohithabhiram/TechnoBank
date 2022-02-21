using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.DTOs.Currency;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Models.Interfaces;

namespace Technovert.BankApp.Services
{
    public class BankService : IBankService
    {
        private readonly IMapper _mapper;
        private readonly BankDbContext _cxt;
        private readonly ICurrencyService _currencyService;
        public BankService(BankDbContext bankDbContext, ICurrencyService currencyService,IMapper mapper)
        {
            _cxt = bankDbContext;
            _currencyService = currencyService;
            _mapper = mapper;
        }
        public string GenerateBankId(string name)
        {
            DateTime d = DateTime.Now;
            string date = DateTime.Now.ToString("ddMMyy");
            return name.Substring(0, 3) + date;
        }

        public Bank GetBank(string bankId)
        {
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
            bank.BankId = GenerateBankId(bank.Name);
            bank.DefaultCurrencyCode = "INR";
            bank.DefaultCurrency = _currencyService.GetCurrency("INR");
            bank.CreatedOn = DateTime.Now;
            bank.CreatedBy = "Admin";
            bank.UpdatedBy = bank.CreatedBy;
            bank.UpdatedOn = DateTime.Now;
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
            Bank bank = _cxt.Banks.SingleOrDefault(b => (b.BankId == bankId));
            _cxt.Banks.Remove(bank);
            _cxt.SaveChanges();
            return bank;
        }

        public IEnumerable<Bank> GetAllBanks()
        {
            return _cxt.Banks.Include(b => b.Accounts)
                .Include(b => b.Currencies)
                .Include(b => b.DefaultCurrency)
                .ToList();
            
        }

        public void AddCurrency(Currency currency)
        {
            _cxt.Currencies.Add(currency);
            _cxt.SaveChanges();
        }
    }
}