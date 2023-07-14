using Market_Console.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Market_Console.Common.Market
{
    public class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }
        public string ProductCode;


    }
}
