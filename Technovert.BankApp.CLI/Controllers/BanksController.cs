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
            //catch (BankIdException e)
            //{

            //    Console.WriteLine("Bank already exists.");
            //}
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
                //if (b == null)
                //{
                //    throw new BankIdException();
                //}
                return b;
            }
            //catch (BankIdException e)
            //{

            //    Console.WriteLine("Bank does not exist.");
            //}
            catch (Exception e)
            {
                Console.WriteLine("Internal Error");
            }
            return null;
        }
    }
}

