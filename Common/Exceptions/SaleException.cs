using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Console.Common.Exceptions
{
    [Serializable]

    public class SaleException : Exception
    {
        public SaleException() { }
        public SaleException(string saleNo) : base($"{saleNo} No. sale is not available!") { }
    }
}
