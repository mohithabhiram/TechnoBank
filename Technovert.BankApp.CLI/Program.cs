using System;
using System.Collections.Generic;
using Technovert.BankApp.Services;
using Technovert.BankApp.Models;
using Technovert.BankApp.CLI.Controllers;
using Technovert.BankApp.Models.Enums;
using Technovert.BankApp.Models.Exceptions;

namespace Technovert.BankApp.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            int currentMenu = 0;
            string userBankId = "";
            string userAccountId = "";
            string userPassword = "";

            Inputs inputs = new Inputs();
            DataStore datastore = new DataStore();

            BankService bankService = new BankService(datastore);
            AccountService accountService = new AccountService(bankService);
            TransactionService transactionService = new TransactionService(accountService, bankService);

            BanksController banksController = new BanksController(bankService, inputs);
            AccountsController accountsController = new AccountsController(accountService, inputs);
            TransactionsController transactionsController = new TransactionsController(transactionService, accountService, bankService);

            Utilities.CreateBank.CustomBanks(banksController);

            while (true)
            {
                if (currentMenu == (int)Menus.BankMenu)
                {
                    Console.WriteLine("Existing Banks:\n-------------------------");
                    Menu.BankMenu(datastore);
                    Console.WriteLine("1.Choose Your Bank:\n2.Add a new Bank:");
                    int option = Convert.ToInt32(Console.ReadLine());
                    if (option == 2)
                    {
                        Console.WriteLine("Enter Bank Name:");
                        string bankName = Console.ReadLine();
                        banksController.CreateBank(bankName);
                    }
                    else
                    {
                        string b = inputs.GetBankId();
                        try
                        {
                            bankService.GetBank(b);
                            userBankId = b;
                            currentMenu++;
                        }
                        catch (BankIdException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                if (currentMenu == (int)Menus.LoginMenu)
                {
                    Menu.LoginMenu();
                    LoginOptions option = (LoginOptions)Enum.Parse(typeof(LoginOptions), Console.ReadLine());
                    switch (option)
                    {
                        case LoginOptions.StaffLogin:
                            try
                            {
                                userAccountId = inputs.GetAccountNumber();
                                accountsController.GetAccount(userBankId, userAccountId);
                                userPassword = inputs.GetPassword();
                                accountService.ValidateStaff(userBankId, userAccountId, userPassword);
                                currentMenu += 2;
                            }
                            catch(AccountNumberException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (Models.Exceptions.UnauthorizedAccessException)
                            {
                                Console.WriteLine("You are not Authorized to Access this Page");
                            }
                            catch (Models.Exceptions.PasswordIncorrectException)
                            {
                                Console.WriteLine("Password Entered is Incorrect");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Something is Wrong");
                            }               
                            break;
                        case LoginOptions.CustomerLogin:
                            try
                            {
                                userAccountId = inputs.GetAccountNumber();
                                accountsController.GetAccount(userBankId, userAccountId);
                                userPassword = inputs.GetPassword();
                                accountService.ValidateUser(userBankId, userAccountId, userPassword);
                                currentMenu++;
                            }
                            catch (AccountNumberException e)
                            {
                                Console.WriteLine(e.Message);
                            }

                            catch (PasswordIncorrectException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Internal Error");
                            }
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
                            transactionsController.Deposit(userBankId, userAccountId);
                            break;
                        case UserOptions.Withdraw:
                            amount = inputs.GetAmount();
                            transactionsController.Withdraw(userBankId, userAccountId, amount);
                            break;
                        case UserOptions.Transfer:
                            try
                            {
                                List<string> recp = inputs.GetRecipient(banksController);
                                amount = inputs.GetAmount();
                                TransactionMode transactionMode = inputs.GetTransactionMode();
                                transactionsController.Transfer(userBankId, userAccountId, recp[0], recp[1], amount, transactionMode);
                            }
                            catch (BankIdException e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            catch (AccountNumberException e)
                            {
                                Console.WriteLine(e.Message);
                            }
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
                        case UserOptions.Back:
                            currentMenu--;
                            break;
                        case UserOptions.Exit:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid");
                            break;

                    }

                }


                if (currentMenu == (int)Menus.StaffMenu)
                {
                    Menu.StaffMenu();
                    StaffOptions option = (StaffOptions)Enum.Parse(typeof(StaffOptions), Console.ReadLine());
                    decimal amount = 0m;
                    switch (option)
                    {
                        case StaffOptions.CreateAccount:
                            string newuserAccountId = accountsController.CreateAccount(userBankId);
                            Console.WriteLine("New Account is Created  account number is " + newuserAccountId + " and your BankID is " + userBankId );
                            break;
                        case StaffOptions.UpdateAccount:
                            throw new ImplementationException("Not implemented");
                            break;
                        case StaffOptions.DeleteAccount:
                            throw new ImplementationException("Not implemented");
                            break;
                        case StaffOptions.ShowAccountTransactionHistory:
                            userAccountId = inputs.GetAccountNumber();
                            List<Transaction> hist = accountsController.GetTransactionHistory(userBankId, userAccountId);

                            Console.WriteLine("TransactionId | Source Bank | Source Account | Dest. Bank | Dest Account |  Amount  | Timestamp ");
                            Console.WriteLine("-----------------------------------------------------------------------------------------------------------");
                            foreach (Transaction t in hist)
                            {
                                Console.WriteLine(t.ToString());
                            }
                            break;
                        case StaffOptions.UpdateServiceChargesForSameBank:
                            banksController.UpdateServiceChargesForSameBank(userBankId);
                            break;
                        case StaffOptions.UpdateServiceChargesForOtherBanks:
                            banksController.UpdateServiceChargesForOtherBanks(userBankId);
                            break;
                        case StaffOptions.AddNewCurrency:
                            Console.WriteLine("List of currencies:");
                            foreach (Currency currency in datastore.Currencies)
                            {
                                Console.WriteLine(currency.Code + "-->" + currency.Name);
                            }
                            Console.WriteLine("Choose a currency to add\nEnter currency code:");
                            string currencyCode = Console.ReadLine();
                            banksController.AddNewCurrency(userBankId,currencyCode);
                            break;
                        case StaffOptions.RevertTransaction:
                            throw new ImplementationException("Not implemented");
                            break;
                        case StaffOptions.Back:
                            currentMenu--;
                            currentMenu--;
                            break;
                        case StaffOptions.Exit:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid Option");
                            break;

                    }

                }

            }

        }
    }
}
