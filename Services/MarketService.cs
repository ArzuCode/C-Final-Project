using ConsoleTables;
using Market_Console.Common.Enums;
using Market_Console.Common.Exceptions;
using Market_Console.Common.Interface;
using Market_Console.Common.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Market_Console.Services

                       //----Market management system----
                      //-----Program for managing products in the market----
{
    public class MarketService : IMarketable
    {

        private int SaleId = 1;
        public List<Sale> Sales { get; set; }
        public List<Product> Products { get; set; }

        
        public MarketService()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Sales = new List<Sale>();
            Products = new List<Product>();

            // ====================== Default filled PRODUCT List ==========================
            Products = _products;

            // ====================== Default filled SALE List ==========================
            //Sales = new List<Sale> {
            //    new Sale{
            //        SaleNo=SaleId++,
            //        SaleItems = new List<SaleItem>
            //        {
            //            new SaleItem
            //            {
            //                No=1,
            //                product=Products.Find(p=>p.ProductCode=="009068"),
            //                prodCount=10
            //            }

            //        }
            //        ,date=new DateTime(2020,11,20),
            //        Amount= Products.Find(p=>p.ProductCode=="009068").Price*10
            //    },
            //    new Sale{
            //        SaleNo=SaleId++,
            //        SaleItems = new List<SaleItem>
            //        {
            //            new SaleItem
            //            {
            //                No=1,
            //                product = Products.Find(p=>p.ProductCode=="005631"),
            //                prodCount=20
            //            }

            //        }
            //        ,date=new DateTime(2020,11,22),
            //        Amount= Products.Find(p=>p.ProductCode=="005631").Price*20
            //    }
            //};

        }
        List<Product> _products = new List<Product> {
            new Product{
               Name="Coca-Cola",
               Price=14.85,
               Category=Category.Beverage,
               Quantity=100,
               ProductCode="009068"
            },
            new Product{
               Name="Sprite",
               Price=2.60,
               Category=Category.Beverage,
               Quantity=250,
               ProductCode="074084"
            },
            new Product{
               Name="Twix",
               Price=30.40,
               Category=Category.Sweets,
               Quantity=142,
               ProductCode="113175"
            },
            new Product{
               Name="Pensil",
               Price=1.10,
               Category=Category.OfficeSupplies,
               Quantity=50,
               ProductCode="005631"
            }
        };

          # region Check Is Number
          /// <summary>
          /// Converts string value to Number type
          /// </summary>
          /// <typeparam name="T"></typeparam>
          /// <param name="value"></param>
          /// <returns></returns>
          public static T To<T>(string value)
          {
            bool converted = false;
            T number = default(T);
            while (!converted)
            {
                try
                {
                    number = (T)Convert.ChangeType(value, typeof(T));
                    converted = true;
                }
                catch
                {
                    converted = false;
                    Console.Write("Enter a number: ");
                    value = Console.ReadLine();
                }
            }
              return number;
          }

        #endregion

        //---------------Product Metods--------------

        #region Product


        /// <summary>
        /// Add product to Product List
        /// </summary>
        public void AddProduct()
        {

            Product prod = new Product();

            #region productName
            Console.Write("The name of the product: ");
            prod.Name = Console.ReadLine();

            #endregion

            #region ProductPrice
            Console.Write("Product price: ");
            string priceVal = Console.ReadLine();
            prod.Price = To<double>(priceVal);
            #endregion

            #region productCategory
            prod.Category = showCategories();
            #endregion

            #region productQuantitiy
            Console.Write("Product quantity: ");
            string quantity = Console.ReadLine();
            prod.Quantity = To<int>(quantity);
            #endregion

            #region productCode
            Console.Write("Product code: ");
            string prodCode = Console.ReadLine();
            while (Products.Find(p => p.ProductCode == prodCode) != null)
            {
                Console.WriteLine($"{prodCode}Coded product available. Enter a new code.");
                Console.Write("Product code: ");
                prodCode = Console.ReadLine();
            }
            prod.ProductCode = prodCode;
            #endregion

            Products.Add(prod);
        }

        /// <summary>
        /// Edit existing product data 
        /// </summary>
        /// <param name="ProductCode">Product Code</param>
        public void EditProdInfo(string ProductCode)
        {

            Product prod = Products.Find(p => p.ProductCode == ProductCode);
            if (prod != null)
            {
                #region productName
                Console.WriteLine($"The name of the product: {prod.Name}");
                Console.Write("New name: ");
                prod.Name = Console.ReadLine();
                Console.WriteLine("");
                #endregion

                #region ProductPrice
                Console.WriteLine($"Product price: {prod.Price}");
                Console.Write("The new value: ");
                string price = Console.ReadLine();
                prod.Price = To<double>(price);
                Console.WriteLine("");
                #endregion

                #region productCategory
                Console.WriteLine($"Product category: {prod.Category}");

                prod.Category = showCategories();
                Console.WriteLine("");
                #endregion

                #region productQuantitiy
                Console.WriteLine($"Product quantity: {prod.Quantity}");
                Console.Write("New quantity: ");
                string quantity = Console.ReadLine();
                prod.Quantity = To<int>(quantity);
                #endregion

                Console.WriteLine($"\"{ProductCode}\" Coded product information corrected. ");

            }
            else throw new ProductException(ProductCode);
        }

        /// <summary>
        /// Delete product from Product List
        /// </summary>
        /// <param name="ProductCode">Product Code</param>
        public void DeleteProduct(string ProductCode)
        {
            Product delItem = Products.Find(p => p.ProductCode == ProductCode);
            if (delItem != null)
            {
                Products.Remove(delItem);
                Console.WriteLine($"{delItem.Name} Product has been deleted.");
            }
            else throw new ProductException(ProductCode);
        }

        /// <summary>
        /// Show product list 
        /// </summary>
        /// <param name="list">Product list to show</param>
        public void ShowProducts(List<Product> list)
        {

            if (list.Count != 0)
            {
                var table = new ConsoleTable("No", "Code", "Category", " Product", "Quantity", "Price");
                foreach (Product item in list)
                {
                    table.AddRow(Products.IndexOf(item) + 1, item.ProductCode, item.Category, item.Name, item.Quantity, item.Price);
                }
                table.Write();
            }
            else Console.WriteLine("The product list is empty.");
        }

        /// <summary>
        /// Show product list by selected Category
        /// </summary>
        /// <param name="category">Product Category</param>
        public void GetProductByCategory(string category)
        {

            Category ctgr;
            while (!Enum.TryParse(category, out ctgr) || !Enum.IsDefined(typeof(Category), ctgr))
            {
                Console.WriteLine($"\"{category}\" The category named does not exist. Please try again.");
                Console.Write("Category: ");
                category = Console.ReadLine();
            }

            List<Product> products = Products.FindAll(p => p.Category == ctgr);
            if (products.Count != 0)
            {
                ShowProducts(products);
            }
            else throw new ProductCategoryException(category);

        }

        /// <summary>
        /// Show product list by selected Price range
        /// </summary>
        /// <param name="minPrice">Minimum price</param>
        /// <param name="maxPrice">Maximum price</param>
        public void GetProductByPriceRange(string minPrice, string maxPrice)
        {
            if (To<double>(minPrice) <= To<double>(maxPrice))
            {
                List<Product> products = Products.FindAll(p => p.Price >= To<double>(minPrice) && p.Price <= To<double>(maxPrice));
                ShowProducts(products);
            }
            else Console.WriteLine("Amount range not entered correctly");
        }

        /// <summary>
        /// Finds products by given text
        /// </summary>
        /// <param name="productName">Text to search consisting Product name</param>
        /// <returns>Product list by Name</returns>
        public List<Product> GetProductByName(string productName)
        {
            return Products.FindAll(p => p.Name.Contains(productName, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Show existing product categories and requires to select one of shown categories
        /// </summary>
        /// <returns>Selected product category</returns>
        public Category showCategories()
        {
            // ------------------------------------   SHOW CATEGORIES ---------------------------------------

            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("You can choose between these product categories: ");

            Array nums = Enum.GetValues(typeof(Category));
            foreach (var item in nums)
            {
                Console.WriteLine(Array.IndexOf(nums, item) + " - " + item);
            }
            Console.WriteLine("----------------------------------------------------------");


            // -------------------------------------- ASSIGN CATEGORY ------------------------------------------

            Console.Write("Product category: ");
            string category = Console.ReadLine();

            Category ctgr;
            while (!Enum.TryParse(category, out ctgr) || !Enum.IsDefined(typeof(Category), ctgr))
            {
                Console.WriteLine($"\"{category}\"The category named does not exist. Please try again.");
                Console.Write("Product category: ");
                category = Console.ReadLine();
            }
            return ctgr;
        }

        #endregion

        //---------------Sale Metods-----------------

       

    }
}


     
    
