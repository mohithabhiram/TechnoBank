using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    public class BalanceException : Exception
    {
        public BalanceException(string message) : base(message)
        {

        }
    }
}
