using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.Models.DTOs.Account;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Interfaces;

namespace Technovert.BankApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }


        [Authorize(Roles = "BankStaff")]
        [HttpGet("{bankId}")]
        public IActionResult Get(string bankId)
        {
            try
            {
                var allAccounts = _accountService.GetAllAccounts(bankId);
                if (allAccounts == null)
                    return NotFound("No accounts found");
                var allAccountsDTO = _mapper.Map<IEnumerable<GetAccountDTO>>(allAccounts);
                return Ok(allAccountsDTO);
            }
            catch (Exception)
            {
                return BadRequest("Internal Error");
            }
        }

        [HttpGet("{bankId}/{accountId}")]
        public IActionResult GetAccount(string bankId, string accountId)
        {
            try
            {
                var account = _accountService.GetAccount(bankId, accountId);
                if (account == null)
                    return NotFound("Account not found");
                var accountDTO = _mapper.Map<GetAccountDTO>(account);
                return Ok(accountDTO);
            }
            catch (Exception)
            {
                return BadRequest("Internal Error");
            }
        }

        [HttpGet("balance/{bankId}/{id}")]
        public IActionResult GetBalance(string bankId, string id)
        {
            
            var account = _accountService.GetAccount(bankId, id);
            if (_accountService.GetAccount(bankId, id) == null)
                return NotFound("Account Not Found");
            var accountDTO = _mapper.Map<AccountBalanceDTO>(account);
            return Ok(accountDTO);
        }

        [HttpPost("{bankId}")]
        public IActionResult Post(string bankId,[FromBody] CreateAccountDTO accountDTO)
        {
            try
            {

                var account = _mapper.Map<Account>(accountDTO);
                var createdAccount = _accountService.CreateAccount(account,bankId);
                return Created(nameof(GetAccount),createdAccount);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


        }

        [HttpPut("{accountId}")]
        public IActionResult UpdateAccount(string bankId, string accountId, [FromBody] UpdateAccountDTO accountDTO)
        {
            var updatedAccount = _accountService.UpdateAccount(bankId,accountId,accountDTO);
            var updatedAccountDTO = _mapper.Map<UpdateAccountDTO>(updatedAccount);
            return Ok(updatedAccountDTO);
        }


        [HttpDelete("{bankId}/{accountId}")]
        public IActionResult Delete(string bankId, string accountId)
        {
            try
            {
                var deletedAccount = _accountService.DeleteAccount(bankId,accountId);
                return Ok(deletedAccount);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost("authenticate/{bankId}")]
        public IActionResult Authenticate(string bankId, [FromBody] AuthenticateAccountDTO accountDTO)
        {
            var token = _accountService.Authenticate(accountDTO.AccountId, accountDTO.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

         


    }
}
