using Market_Console.Common.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Market_Console.Services
{
    public class MarketService
    {
      
        public List<Product> products { get; set; }

        public void AddProduct()
        {
            //Init with default values
            products = new List<Product>()
            {
                new Product{ Id = 1, Name = "Iced Tea", Quantity = 100, Price = 1.99m },
                new Product{ Id = 2, Name = "Canada Dry", Quantity = 200, Price = 1.99m },
                new Product{ Id = 3, Name = "Whole Wheat Bread", Quantity = 300, Price = 1.50m },
                new Product{ Id = 4, Name = "White Bread", Quantity = 400, Price = 2.50m },
                new Product{ Id = 5, Name = "Wagyu A5", Quantity = 70, Price = 150 },
                new Product{ Id = 6, Name = "Rib Eye", Quantity = 160, Price = 75.25m }
            };
        }
        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public void AddProduct(Product product)
        {
            if (products.Any(x => x.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase))) return;

            if (products != null && products.Count > 0)
            {
                var maxId = products.Max(x => x.Id);
                product.Id = maxId + 1;
            }
            else
            {
                product.Id = 1;
            }

            products.Add(product);

          

        }
    }
}
