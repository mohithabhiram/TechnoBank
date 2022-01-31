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
    //[Authorize]
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


        //[Authorize(Roles ="BankStaff")]
        [HttpGet("{bankId}")]
        public IActionResult Get(string bankId)
        {
            try
            {
                var allAcc = _accountService.GetAllAccounts(bankId);
                if (allAcc == null)
                    return NotFound();
                var allDTO = _mapper.Map<IEnumerable<GetAccountDTO>>(allAcc);
                return Ok(allDTO);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{bankId}/{accountId}")]
        public IActionResult GetAccount(string bankId, string accountId)
        {
            try
            {
                var acc = _accountService.GetAccount(bankId, accountId);
                var accDTO = _mapper.Map<GetAccountDTO>(acc);
                return Ok(accDTO);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("balance/{bankId}/{id}")]
        public IActionResult GetBalance(string bankId, string id)
        {
            
            var acc = _accountService.GetAccount(bankId, id);
            if (_accountService.GetAccount(bankId, id) == null)
                return BadRequest();
            var accDTO = _mapper.Map<AccountBalanceDTO>(acc);
            return Ok(accDTO);
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
            catch (Exception)
            {
                return BadRequest();
            }


        }

        [HttpPut("{accountId}")]
        public IActionResult UpdateAccount(string bankId, string accountId, [FromBody] UpdateAccountDTO accountDTO)
        {
            var updatedAcc = _accountService.UpdateAccount(bankId,accountId,accountDTO);
            var updatedAccDTO = _mapper.Map<UpdateAccountDTO>(updatedAcc);
            return Ok(updatedAccDTO);
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
