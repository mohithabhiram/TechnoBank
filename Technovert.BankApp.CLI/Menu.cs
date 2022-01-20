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
            foreach (Bank bank in datastore.Banks)
            {
                Console.WriteLine(bank.BankId + " | " + bank.Name);
            }
            Console.WriteLine("-------------------------");
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
            Console.WriteLine("   1   | Staff Login");
            Console.WriteLine("   2   | User Login");
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
            Console.WriteLine("   1   | Create Account");
            Console.WriteLine("   2   | Update Acount");
            Console.WriteLine("   3   | Delete Account");
            Console.WriteLine("   4   | Update Service Charges For Same Bank");
            Console.WriteLine("   5   | Update Service Charges For Other Banks");
            Console.WriteLine("   6   | Show Account Transaction History");
            Console.WriteLine("   7   | Revert Transaction");
            Console.WriteLine("   8   | Add Currency");
            Console.WriteLine("   9   | Back");
            Console.WriteLine("  10   | Exit");
            Console.WriteLine("----------------------------------");
        }
    }
}

        