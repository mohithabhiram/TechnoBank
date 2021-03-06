using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Enums
{
    public enum StaffOptions
    {
        CreateAccount=1,
        UpdateAccount,
        DeleteAccount,
        UpdateServiceChargesForSameBank,
        UpdateServiceChargesForOtherBanks,
        ShowAccountTransactionHistory,
        RevertTransaction,
        AddCurrency,
        Back,
        Exit
    }
}