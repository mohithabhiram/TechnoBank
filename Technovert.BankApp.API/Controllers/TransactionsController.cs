using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.Models.DTOs.Transaction;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Enums;
using Technovert.BankApp.Models.Interfaces;

namespace Technovert.BankApp.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly IBankService _bankService;

        public TransactionsController(ITransactionService transactionService, IMapper mapper, IAccountService accountService, IBankService bankService)
        {
            _transactionService = transactionService;
            _mapper = mapper;
            _accountService = accountService;
            _bankService = bankService;
        }


        [HttpGet("{transactionId}")]
        public IActionResult Get(string transactionId)
        {
            var transaction = _transactionService.GetTransaction(transactionId);
            if(transaction == null)
            {
                return NotFound("Invalid Transaction Id");
            }
            var transactionDTO = _mapper.Map<GetTransactionDTO>(transaction);
            return Ok(transactionDTO);
        }


        [HttpGet]
        public IActionResult Get()
        {
            var transactions = _transactionService.GetAllTransactions();
            var transactionsDTO = _mapper.Map<IEnumerable<GetTransactionDTO>>(transactions);
            return Ok(transactionsDTO);
        }

        [HttpPost("{bankId}/{accountId}")]
        public IActionResult Post(string bankId, string accountId, [FromBody] CreateTransactionDTO newTransactionDTO)
        {
            var newTransaction = _transactionService.UpdateBalance(bankId, accountId, newTransactionDTO);
            if(newTransaction == null)
            {
                return BadRequest("Invalid Transaction");
            }
            var transaction =_transactionService.CreateTransaction(bankId,accountId,newTransaction);
            var getTransactionDto = _mapper.Map<GetTransactionDTO>(transaction);
            return Ok(getTransactionDto);
        }

    }
}
