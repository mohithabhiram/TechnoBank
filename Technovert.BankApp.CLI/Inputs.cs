using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models.Enums;
using Technovert.BankApp.Models;
using Technovert.BankApp.Models.Exceptions;

namespace Technovert.BankApp.CLI
{
    public class Inputs
    {
        public string GetAccountNumber()
        {
            
            Console.WriteLine("Please Enter the Account Number :");
            return (Console.ReadLine());
        }
        public string GetPassword()
        {
            Console.WriteLine("Enter Password :");
            string password = Console.ReadLine();
            while (password.Length < 3)
            {
                Console.WriteLine("Password should have a length of minimum 4 characters");
                Console.WriteLine("Enter Password :");
                password = Console.ReadLine();
            }
            return password;
        }
        public string GetName()
        {
            Console.WriteLine("Enter Name :");
            string name = Console.ReadLine();
            while((!name.All(Char.IsLetter)) || name.Length<3)
            {
                Console.WriteLine("Name should contain only letters and should have a length of minimum 3 characters");
                Console.WriteLine("Enter Name :");
                name = Console.ReadLine();
            }
            return name;
        }
        public string GetBankId()
        {
            Console.WriteLine("Please Enter Your Selection :");
            string bankId = Console.ReadLine();
            return bankId;
        }
        public Gender GetGender()
        {
            Console.WriteLine("Please Enter Your Gender (Male/Female/Other) :");
            Enum.TryParse(Console.ReadLine(), out Gender gender);
            return gender;
        }
        public int GetSelection()
        {
            try
            {
                Console.WriteLine("Please Enter Your Selection :");

                return Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException e)
            {
                Console.WriteLine("Invalid Selection");
            }
            //Goback
            return -1;
        }
        public decimal GetAmount()
        {
            Console.WriteLine("Please Enter The Amount :");
            return Convert.ToDecimal(Console.ReadLine());
        }
        public decimal GetRtgs()
        {
            Console.WriteLine("Set RTGS value :");
            return Convert.ToDecimal(Console.ReadLine())/100;
        }
        public decimal GetImps()
        {
            Console.WriteLine("Set IMPS value :");
            return Convert.ToDecimal(Console.ReadLine())/100;
        }
        public List<string> GetRecipient(Controllers.BanksController banksController)
        {
            List<string> res = new List<string>();
            Console.WriteLine("Please Enter Recipient BankId");
            string recipBankId = Console.ReadLine();
            if (banksController.GetBank(recipBankId) == null)
                throw new BankIdException("Bank does not exist");
            res.Add(recipBankId);
            Console.WriteLine("Please Enter Recipient Account number");
            string recipAccountId = Console.ReadLine();
            if (banksController.GetBank(recipBankId).Accounts.SingleOrDefault(a => a.AccountId == recipBankId) == null)
                throw new AccountNumberException("Account does not exist");
            res.Add(recipAccountId);
            return res;
        }
        public TransactionMode GetTransactionMode()
        {
            Console.WriteLine("Enter Transaction Mode:\nRTGS\nIMPS");
            string mode = Console.ReadLine();
            Enum.TryParse(mode, out TransactionMode transactionMode);
            return transactionMode;
        }
        public AccountType GetAccountType()
        {
            Console.WriteLine("Enter Account Type:\n1.User\n2.Bank Staff");
            string type = Console.ReadLine();
            Enum.TryParse(type, out AccountType accountType);
            return accountType;
        }
    }
}
