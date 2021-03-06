using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.Models.DTOs.Bank;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Interfaces;
using Technovert.BankApp.Models.DTOs.Currency;

namespace Technovert.BankApp.API.Controllers
{
    [Authorize(Roles = "BankStaff")]
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IBankService _bankService;
        private readonly ICurrencyService _currencyService;
        private readonly IMapper _mapper;

        public BanksController(IBankService bankService, IMapper mapper, ICurrencyService currencyService)
        {
            _bankService = bankService;
            _mapper = mapper;
            _currencyService = currencyService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var allBanks = _bankService.GetAllBanks();
                var allBanksDTO = _mapper.Map<IEnumerable<GetBankDTO>>(allBanks);
                return Ok(allBanksDTO);
            }
            catch (Exception)
            {

                return BadRequest("Access Denied");
            }
        }


        [AllowAnonymous]
        [HttpGet("{bankId}")]
        public IActionResult GetBank(string bankId)
        {
            var bank = _bankService.GetBank(bankId);
            if (bank == null)
                return NotFound("Bank Not Found");
            var bankDTO = _mapper.Map<GetBankDTO>(bank);
            return Ok(bankDTO);

        }
        [Authorize(Roles = "BankStaff")]
        [HttpPost]
        public IActionResult Post([FromBody]CreateBankDTO bankDTO)
        {
            try
            {
                var bank = _mapper.Map<Bank>(bankDTO);
                var createdBank = _bankService.CreateBank(bank);
                return Ok(createdBank);
            }
            catch (Exception)
            {
                return BadRequest("Internal Error");
            }
        }


        [Authorize(Roles = "BankStaff")]
        [HttpPost("addCurrency")]
        public IActionResult AddCurrency([FromBody] Currency currency)
        {
            try
            {
                _bankService.AddCurrency(currency);
                return Ok(currency);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet("currencies")]
        public IActionResult GetCurrencies()
        {
            var currencies = _currencyService.GetAllCurrencies();
            if (currencies == null)
                return NotFound();
            return Ok(currencies);

        }


        [HttpDelete("{bankId}")]
        public IActionResult Delete(string bankId)
        {
            try
            {
                var deletedBank = _bankService.DeleteBank(bankId);
                return Ok(deletedBank);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    }
}

