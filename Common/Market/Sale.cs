using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Console.Common.Market
{
    public class Sale
    {
        public int SaleNo { get; set; }
        public double Amount { get; set; }
        public List<SaleItem> SaleItems { get; set; }
        public DateTime Date { get; set; }
    }
}
