using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.API.DTOs.Account;
using Technovert.BankApp.API.DTOs.Bank;
using Technovert.BankApp.API.DTOs.Transaction;
using Technovert.BankApp.Models;

namespace Technovert.BankApp.API.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateBankDTO, Bank>();
            CreateMap<CreateAccountDTO, Account>();
            CreateMap<Account,AccountBalanceDTO>();
          
            CreateMap<Bank, GetBankDTO>();
            CreateMap<Account, GetAccountDTO>();

            CreateMap<UpdateBankDTO, Bank>();
            CreateMap<UpdateAccountDTO, Account>();
            CreateMap<AuthenticateAccountDTO, Account>();

            CreateMap<Transaction, GetTransactionDTO>();
            CreateMap<CreateTransactionDTO, Transaction>();


        }
    }
}
