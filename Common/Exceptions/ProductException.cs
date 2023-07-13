using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Market_Console.Common.Exceptions
{
    [Serializable]

    public class ProductException : Exception
    {
        public ProductException() { }
        public ProductException(string code) : base($"{code} Coded product is not available!") { }
    }

    public class ProductCategoryException : Exception
    {
        public ProductCategoryException() { }
        public ProductCategoryException(string category) : base($"{category} There are no products in this category.") { }
    }
}





