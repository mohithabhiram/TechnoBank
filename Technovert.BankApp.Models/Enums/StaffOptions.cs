using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Enums
{
    public enum StaffOptions
    {
        CreateAccount = 1,
        GetAccountDetails,
        UpdateAccount,
        DeleteAccount,
        UpdateServiceChargesForSameBank,
        UpdateServiceChargesForOtherBanks,
        ShowAccountTransactionHistory,
        ShowBankTransactionHistory,
        RevertTransaction,
        AddNewCurrency,
        Back,
        Exit
    }
}