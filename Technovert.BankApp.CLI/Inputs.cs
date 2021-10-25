using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models.Enums;

namespace Technovert.BankApp.CLI
{
    public class Inputs
    {
        public string GetAccountNumber()
        {
            Console.WriteLine("Please Enter Your Account Number :");
            return (Console.ReadLine());
        }
        public string GetPassword()
        {
            Console.WriteLine("Please Enter Your Password :");
            return Console.ReadLine();
        }
        public string GetName()
        {
            Console.WriteLine("Please Enter Your Name :");
            return Console.ReadLine();
        }
        public string GetBankId()
        {
            Console.WriteLine("Please Enter Your Selection :");
            return Console.ReadLine();
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
        public List<string> GetRecipient()
        {
            List<string> res = new List<string>();
            Console.WriteLine("Please Enter Recipient BankId");
            string recipBankId = Console.ReadLine();
            res.Add(recipBankId);
            Console.WriteLine("Please Enter Recipient Account number");
            res.Add((Console.ReadLine()));
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
