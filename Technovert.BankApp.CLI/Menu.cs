using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technovert.BankApp.Models;
using Technovert.BankApp.Services;

namespace Technovert.BankApp.CLI
{
    public static class Menu
    {
        public static void BankMenu(DataStore datastore)
        {
            Console.WriteLine("Choose Your Bank");
            foreach (Bank bank in datastore.Banks)
            {
                Console.WriteLine(bank.BankId + " | " + bank.Name);
            }
        }
        public static void UserLoginMenu()
        {
            Console.WriteLine("1-> Create Account");
            Console.WriteLine("2-> Login");
            Console.WriteLine("3-> Back");
            Console.WriteLine("4-> Exit");
        }
        public static void LoginMenu()
        {
            Console.WriteLine("LOGIN MENU:   ");
            Console.WriteLine("Choose and Enter The option below");
            Console.WriteLine("Option | Description");
            Console.WriteLine("-------------------------");
            Console.WriteLine("   1   | StaffLogin");
            Console.WriteLine("   2   | UserLogin");
            Console.WriteLine("   3   | Back");
            Console.WriteLine("   4   | Exit");
            Console.WriteLine("-------------------------");
        }
        public static void UserMenu()
        {
            Console.WriteLine("1.Deposit");
            Console.WriteLine("2.Withdraw");
            Console.WriteLine("3.Transfer");
            Console.WriteLine("4.Show Balance");
            Console.WriteLine("5.Show Transaction History");
            Console.WriteLine("6.Back");
            Console.WriteLine("7.Exit");
        }


        public static void StaffMenu()
        {
            Console.WriteLine("STAFF MENU:   ");
            Console.WriteLine("Choose and Enter the option below");
            Console.WriteLine("Option | Description");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("   1   | CreateAccount");
            Console.WriteLine("   2   | UpdateAcount");
            Console.WriteLine("   3   | DeleteAccount");
            Console.WriteLine("   4   | UpdateServiceChargesForSameBank");
            Console.WriteLine("   5   | UpdateServiceChargesForOtherBanks");
            Console.WriteLine("   6   | ShowAccountTransactionHistory");
            Console.WriteLine("   7   | ShowBankTransactionHistory");
            Console.WriteLine("   8   | RevertTransaction");
            Console.WriteLine("   9   | AddNewCurrency");
            Console.WriteLine("  10   | Back");
            Console.WriteLine("  11   | Exit");
            Console.WriteLine("----------------------------------");
        }
    }
}

        