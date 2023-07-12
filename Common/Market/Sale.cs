using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Console.Common.Market
{
    public class Sale
    {
        public int Number { get; set; }
        public decimal Amount { get; set; }
        public int SaleItems { get; set; }
        public DateTime Date { get; set; }
    }
}
