using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.API.DTOs.Account;
using Technovert.BankApp.API.DTOs.Bank;
using Technovert.BankApp.Models;

namespace Technovert.BankApp.API.Profiles
{
    public class BankProfile : Profile
    {
        public BankProfile()
        {
            CreateMap<CreateBankDTO, Bank>();
            CreateMap<CreateAccountDTO, Account>();
          
            CreateMap<Bank, GetBankDTO>();
            CreateMap<Account, GetAccountDTO>();

            CreateMap<UpdateBankDTO, Bank>();
            CreateMap<UpdateAccountDTO, Account>();
            
            
        }
    }
}
