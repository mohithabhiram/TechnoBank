using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.API.DTOs.Transaction;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Enums;
using Technovert.BankApp.Services.Interfaces;

namespace Technovert.BankApp.API.Controllers
{
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
        public IActionResult Post(string bankId, string accountId, [FromBody] CreateTransactionDTO newTransaction)
        {
            decimal rateOfCharges = 0m;
            if (newTransaction.TransactionMode == TransactionMode.RTGS)
            {
                if (bankId == newTransaction.DestinationBankId)
                {
                    rateOfCharges = _bankService.GetBank(bankId).RTGSToSame;
                }
                else
                {
                    rateOfCharges = _bankService.GetBank(bankId).RTGSToOther;
                }

            }
            else if (newTransaction.TransactionMode == TransactionMode.IMPS)
            {
                if (bankId == newTransaction.DestinationBankId)
                {
                    rateOfCharges = _bankService.GetBank(bankId).IMPSToSame;
                }
                else
                {
                    rateOfCharges = _bankService.GetBank(bankId).IMPSToOther;
                }
            }
            else
            {
                rateOfCharges = 0;
            }
            decimal totalCharges = newTransaction.Amount * rateOfCharges;
            //withdraw
            if (newTransaction.Type == TransactionType.Withdraw)
            {
                _accountService.UpdateBalance(bankId, accountId, _accountService.GetAccount(bankId, accountId).Balance - newTransaction.Amount);
            }
            //deposit
            else if (newTransaction.Type == TransactionType.Deposit)
            {
                _accountService.UpdateBalance(newTransaction.DestinationBankId, newTransaction.DestinationAccountId, _accountService.GetAccount(newTransaction.DestinationBankId, newTransaction.DestinationAccountId).Balance + newTransaction.Amount);
            }
            //transfer
            else
            {
                _accountService.UpdateBalance(bankId, accountId, _accountService.GetAccount(bankId, accountId).Balance - (newTransaction.Amount + totalCharges));
                _accountService.UpdateBalance(newTransaction.DestinationBankId, newTransaction.DestinationAccountId, _accountService.GetAccount(newTransaction.DestinationBankId, newTransaction.DestinationAccountId).Balance + newTransaction.Amount);
            }
            var t = _mapper.Map<Transaction>(newTransaction);
            t.TransactionId = _transactionService.GenerateTransactionId(bankId,accountId,newTransaction.DestinationBankId,newTransaction.DestinationAccountId,newTransaction.Type);
            t.SourceAccountId = accountId;
            t.SourceBankId = bankId;
            t.SourceAccount = _accountService.GetAccount(bankId, accountId);
            t.DestinationAccount = _accountService.GetAccount(newTransaction.DestinationBankId,newTransaction.DestinationAccountId);
            t.TransactionMode = newTransaction.TransactionMode;
            t.On = DateTime.Now;
            _transactionService.CreateTransaction(t);
            var getTDto = _mapper.Map<GetTransactionDTO>(t);
            return Ok(getTDto);
        }

    }
}
