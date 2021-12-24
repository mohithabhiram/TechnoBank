using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.API.DTOs.Account;
using Technovert.BankApp.Models;
using Technovert.BankApp.Services.Interfaces;

namespace Technovert.BankApp.API.Controllers
{
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



        [HttpGet("{bankId}")]
        public IActionResult Get(string bankId)
        {
            try
            {
                var all = _accountService.GetAllAccounts(bankId);
                var allDTO = _mapper.Map<IEnumerable<GetAccountDTO>>(all);
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
                account.Status = Models.Enums.Status.Active;
                account.AccountId = _accountService.GenerateAccountId(account.Name);
                account.BankId = bankId;
                var createdAccount = _accountService.CreateAccount(account);
                return Created(nameof(GetAccount),createdAccount);
            }
            catch (Exception)
            {

                return BadRequest();
            }


        }




    }
}
