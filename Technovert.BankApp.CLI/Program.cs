using System;
using System.Collections.Generic;
using Technovert.BankApp.Services;
using Technovert.BankApp.Models;
using Technovert.BankApp.CLI.Controllers;
using Technovert.BankApp.Models.Enums;

namespace Technovert.BankApp.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            int currentMenu = 0;
            string userBankId = "";
            string userAccountId = "";

            Inputs inputs = new Inputs();
            DataStore datastore = new DataStore();

            BankService bankService = new BankService(datastore);
            AccountService accountService = new AccountService(bankService);
            TransactionService transactionService = new TransactionService(accountService, bankService);

            BanksController banksController = new BanksController(bankService, inputs);
            AccountsController accountsController = new AccountsController(accountService, inputs);
            TransactionsController transactionsController = new TransactionsController(transactionService, accountService);

            Utilities.CreateBank.CustomBanks(banksController);

            while (true)
            {
                if (currentMenu == (int)Menus.BankMenu)
                {
                    Menu.BankMenu(datastore);
                    string b = inputs.GetBankId();
                    userBankId = b;
                    currentMenu++;
                }
                if (currentMenu == (int)Menus.LoginMenu)
                {
                    Menu.LoginMenu();
                    LoginOptions option = (LoginOptions)Enum.Parse(typeof(LoginOptions), Console.ReadLine());
                    switch (option)
                    {
                        case LoginOptions.Create:
                            userAccountId = accountsController.CreateAccount(userBankId);
                            Console.WriteLine("Your account number is " + userAccountId);
                            break;
                        case LoginOptions.Login:
                            userAccountId = inputs.GetAccountNumber();
                            string userPassword = inputs.GetPassword();
                            currentMenu++;
                            break;
                        case LoginOptions.Back:
                            currentMenu--;
                            break;
                        case LoginOptions.Exit:
                            Environment.Exit(0);
                            break;
                    }
                }
                if (currentMenu == (int)Menus.UserMenu)
                {
                    Menu.UserMenu();
                    UserOptions option = (UserOptions)Enum.Parse(typeof(UserOptions), Console.ReadLine());
                    decimal amount = 0m;
                    switch (option)
                    {
                        case UserOptions.Deposit:
                            amount = inputs.GetAmount();
                            transactionsController.Deposit(userBankId, userAccountId, amount);
                            break;
                        case UserOptions.Withdraw:
                            amount = inputs.GetAmount();
                            transactionsController.Withdraw(userBankId, userAccountId, amount);
                            break;
                        case UserOptions.Transfer:
                            List<string> recp = inputs.GetRecipient();
                            amount = inputs.GetAmount();
                            TransactionMode transactionMode = inputs.GetTransactionMode();
                            transactionsController.Transfer(userBankId, userAccountId, recp[0], recp[1], amount, transactionMode);
                            break;
                        case UserOptions.ShowBalance:
                            {
                                Console.WriteLine("Your Balance is: " + accountsController.GetBalance(userBankId, userAccountId));
                                break;
                            }
                        case UserOptions.TransactionHistory:
                            List<Transaction> tHist = accountsController.GetTransactionHistory(userBankId, userAccountId);
                            Console.WriteLine("TransactionId | Source Bank | Source Account | Dest. Bank | Dest Account |  Amount  | Timestamp ");
                            Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
                            foreach (Transaction t in tHist)
                            {
                                Console.WriteLine($" {t.TransactionId} | {t.SourceBankId}  |   {t.SourceAccountId}   |   {t.DestinationBankId}  |  {t.DestinationAccountId}   | {t.Amount.ToString()} | {t.On}");
                            }
                            break;
                        case UserOptions.Exit:
                            currentMenu = 0;
                            break;
                        default:
                            Console.WriteLine("Invalid");
                            break;

                    }

                }

            }

        }
    }
}
