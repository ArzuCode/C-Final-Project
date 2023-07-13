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
        public decimal Price { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }
        public int Id { get; set; }

        internal void Add(Product product)
        {
            throw new NotImplementedException();
        }

        internal object Max(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}
