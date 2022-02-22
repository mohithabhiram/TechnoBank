using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.DTOs.Account;
using Technovert.BankApp.Models.Enums;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Models.Interfaces;

namespace Technovert.BankApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankDbContext _ctx;
        private readonly IMapper _mapper;
       
        public AccountService(BankDbContext bankDbContext, IMapper mapper)
        {
            _ctx = bankDbContext;
            _mapper = mapper;
        }
      
        public string GenerateAccountId(string name)
        {
            DateTime d = DateTime.Now;
            string date = DateTime.Now.ToString("ddMMyy");
            return name.Substring(0, 3) + date;
        }

        public Account GetAccount(string bankId, string accountId)
        {
            
            return _ctx.Accounts.FirstOrDefault(a => (a.AccountId == accountId) && (a.BankId == bankId));
          
        }

      

        public Account CreateAccount(Account account, string bankId)
        {
            account.Status = Models.Enums.Status.Active;
            account.AccountId = GenerateAccountId(account.Name);
            account.BankId = bankId;
            _ctx.Accounts.Add(account);
            _ctx.SaveChanges();
            var createdAccount = _ctx.Accounts.FirstOrDefault(a => a.AccountId == account.AccountId);
            return createdAccount;

        }

        public Account UpdateAccount(string bankId, string accountId, UpdateAccountDTO accountDTO)
        {
            var acc = GetAccount(bankId, accountId);
            acc.Name = accountDTO.Name;
            acc.Password = accountDTO.Password;
            acc.Gender = accountDTO.Gender;
            acc.Status = accountDTO.Status;
            _ctx.SaveChanges();
            return acc;
        }

        Account IAccountService.DeleteAccount(string bankId, string accountId)
        {
            var acc = _ctx.Accounts.FirstOrDefault(a => (a.AccountId == accountId) && (a.BankId == bankId));
            _ctx.Accounts.Remove(acc);
            _ctx.SaveChanges();
            return acc;
        }

        public IEnumerable<Account> GetAllAccounts(string bankId)
        {
            IEnumerable<Account> acc = _ctx.Accounts.Where(a => a.BankId == bankId).ToList();
            if (acc.LongCount() == 0)
                return null;
            return acc;
        }

        public void UpdateBalance(string bankId, string accountId, decimal balance)
        {
            var acc = _ctx.Accounts.FirstOrDefault(a => (a.AccountId == accountId) && (a.BankId == bankId));
            acc.Balance = balance;
        }

        public string Authenticate(string accountId, string password)
        {
            Account account = _ctx.Accounts.SingleOrDefault(a => a.AccountId == accountId);
            if (account == null || account.Password!=password)
                return null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("this is my secret key");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new Claim[] {
                        new Claim(ClaimTypes.Name,account.AccountId.ToString()) ,
                        new Claim(ClaimTypes.Role,account.Type.ToString())
                    }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
    }
}