using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.Models.DTOs.Account;
using Technovert.BankApp.Models.DTOs.Bank;
using Technovert.BankApp.Models.DTOs.Transaction;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.DTOs.Currency;

namespace Technovert.BankApp.API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateBankDTO, Bank>();
            CreateMap<CreateAccountDTO, Account>();
            CreateMap<Account,AccountBalanceDTO>();
            CreateMap<Account, UpdateAccountDTO>();
          
            CreateMap<Bank, GetBankDTO>();
            CreateMap<Account, GetAccountDTO>();

            CreateMap<UpdateBankDTO, Bank>();
            CreateMap<UpdateAccountDTO, Account>();
            CreateMap<AuthenticateAccountDTO, Account>();

            CreateMap<Transaction, GetTransactionDTO>();
            CreateMap<CreateTransactionDTO, Transaction>();
            CreateMap<Transaction, CreateTransactionDTO>();

            CreateMap<AddCurrencyDTO, Currency>();
            CreateMap<Currency, AddCurrencyDTO>();


        }
    }
}
