using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Technovert.BankApp.API.DTOs.Bank;
using Technovert.BankApp.Models;
using Technovert.BankApp.Services.Interfaces;

namespace Technovert.BankApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanksController : ControllerBase
    {
        private readonly IBankService _bankService;
        private readonly IMapper _mapper;

        public BanksController(IBankService bankService, IMapper mapper)
        {
            _bankService = bankService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var all = _bankService.GetAllBanks();
                var allDTO = _mapper.Map<IEnumerable<GetBankDTO>>(all);
                return Ok(allDTO);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }




        [HttpGet("{bankId}")]
        public IActionResult GetBank(string bankId)
        {
            var bank = _bankService.GetBank(bankId);
            if (bank == null)
                return NotFound();
            var bankDTO = _mapper.Map<GetBankDTO>(bank);
            return Ok(bankDTO);

        }
        [HttpPost]
        public IActionResult Post([FromBody]CreateBankDTO bankDTO)
        {
            try
            {
                var bank = _mapper.Map<Bank>(bankDTO);
                bank.BankId = _bankService.GenerateBankId(bank.Name);
                bank.CreatedOn = DateTime.Now;
                bank.CreatedBy = "Abhiram";
                bank.UpdatedBy = bank.CreatedBy;
                bank.UpdatedOn = DateTime.Now;
                var createdBank = _bankService.CreateBank(bank);
                return Ok(createdBank);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}

