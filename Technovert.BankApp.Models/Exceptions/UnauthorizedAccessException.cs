using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technovert.BankApp.Models.Exceptions
{
    public class UnauthorizedAccessException: Exception
    {
        public UnauthorizedAccessException(string message) : base(message)
        {

        }

    }
}
