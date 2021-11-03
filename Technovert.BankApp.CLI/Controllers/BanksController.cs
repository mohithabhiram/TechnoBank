using Technovert.BankApp.Models;
using Technovert.BankApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models.Exceptions;



namespace Technovert.BankApp.CLI.Controllers
{
    public class BanksController
    {
        private BankService bankService;
        private Inputs inputs;

        public BanksController(BankService bankService, Inputs inputs)
        {
            this.bankService = bankService;
            this.inputs = inputs;
        }
        public string CreateBank(string name)
        {
            string id;
            try
            {
                string bankName = name;
                id = bankService.AddBank(bankName);
                return id;

            }
            catch (BankIdException e)
            {

                Console.WriteLine("Bank already exists.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Internal Error");
            }
            return null;


        }
        public Bank GetBank(string bankId)
        {

            try
            {
                Bank b = bankService.GetBank(bankId);
                return b;
            }
            catch (Exception e)
            {
                Console.WriteLine("Internal Error");
            }
            return null;
        }
        public void UpdateServiceChargesForSameBank(string userBankId)
        {
            decimal imps = inputs.GetImps();
            decimal rtgs = inputs.GetRtgs();
            bankService.UpdateServiceChargesForSameBank(rtgs, imps, userBankId);
        }
        public void UpdateServiceChargesForOtherBanks(string userBankId)
        {
            decimal imps = inputs.GetImps();
            decimal rtgs = inputs.GetRtgs();
            bankService.UpdateServiceChargesForOtherBanks(rtgs, imps, userBankId);
        }
        public void AddNewCurrency(string bankId, string currencyCode)
        {
            if(bankService.GetBank(bankId).Currencies.SingleOrDefault(c => c.Code == currencyCode)==null)
            {
                bankService.AddNewCurrency(bankId, currencyCode); 
            }
            else
            {
                Console.WriteLine("Currency already exists:");
                return;
            }
           
        }

    }
}

