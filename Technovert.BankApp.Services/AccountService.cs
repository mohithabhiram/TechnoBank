using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Enums;
using Technovert.BankApp.Models.Exceptions;
using Technovert.BankApp.Services.Interfaces;

namespace Technovert.BankApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly BankDbContext _cxt;
       
        public AccountService(BankDbContext bankDbContext)
        {
            _cxt = bankDbContext;
        }
      
        public string GenerateAccountId(string name)
        {
            DateTime d = DateTime.Now;
            string date = DateTime.Now.ToString("ddMMyy");
            return name.Substring(0, 3) + date;
        }

        public Account GetAccount(string bankId, string accountId)
        {
            
            return _cxt.Accounts.FirstOrDefault(a => (a.AccountId == accountId) && (a.BankId == bankId));
        }

      

        public Account CreateAccount(Account account)
        {
            _cxt.Accounts.Add(account);
            _cxt.SaveChanges();
            var createdAccount = _cxt.Accounts.FirstOrDefault(a => a.AccountId == account.AccountId);
            return createdAccount;

        }

        public Account UpdateAccount(Account account)
        {
            throw new NotImplementedException();
        }

        Account IAccountService.DeleteAccount(string bankId, string accountid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAllAccounts(string bankId)
        {
            return _cxt.Accounts.Where(a => a.BankId == bankId).ToList();
        }

        public void UpdateBalance(string bankId, string accountId, decimal balance)
        {
            var acc = _cxt.Accounts.FirstOrDefault(a => (a.AccountId == accountId) && (a.BankId == bankId));
            acc.Balance = balance;
        }

        public string Authenticate(string accountId, string password)
        {
            Account account = _cxt.Accounts.SingleOrDefault(a => a.AccountId == accountId);
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