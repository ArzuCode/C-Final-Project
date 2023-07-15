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
               Price=1.50,
               Category=Category.Beverage,
               Quantity=300,
               ProductCode="009068"
            },
            new Product{
               Name="Sprite",
               Price=2.10,
               Category=Category.Beverage,
               Quantity=250,
               ProductCode="074084"
            },
            new Product{
               Name="Twix",
               Price=1.40,
               Category=Category.Sweets,
               Quantity=142,
               ProductCode="113175"
            },
            new Product{
               Name="Pensil",
               Price=0.50,
               Category=Category.OfficeSupplies,
               Quantity=55,
               ProductCode="005631"
            },
            new Product{
               Name="Book",
               Price=5.50,
               Category=Category.OfficeSupplies,
               Quantity=20,
               ProductCode="005852"
            },
            new Product{
               Name="Fish",
               Price=11,
               Category=Category.Meat,
               Quantity=50,
               ProductCode="015985"
            },
            new Product{
               Name="Bread",
               Price=0.70,
               Category=Category.Bakery,
               Quantity=100,
               ProductCode="007541"
            },
            new Product{
               Name="Snickers",
               Price=2.80,
               Category=Category.Sweets,
               Quantity=85,
               ProductCode="056329"
            },
            new Product{
               Name="Chicken",
               Price=6.50,
               Category=Category.Meat,
               Quantity=15,
               ProductCode="254896"
            },
            new Product{
               Name="Wishky",
               Price=35,
               Category=Category.Beverage,
               Quantity=40,
               ProductCode="356821"
            },
        };

        #region Check Is Number
        /// <summary>
        /// Converts string value to Number type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T To<T>(string value)
        {
            bool converted = false;
            T number = default;
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
        public void EditProductInfo(string ProductCode)
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

        #region Sale

        /// <summary>
        /// Creates Sale, adds Sale Items untill click SpaceBar button
        /// </summary>
        public void AddSale()
        {
            Sale sale = new Sale();
            sale.SaleNo = SaleId++;
            sale.SaleItems = new List<SaleItem>();
            ConsoleKeyInfo key = default;
            int i = 1;
            do
            {
                SaleItem item = new SaleItem();

                // --------------- input PRODUCT CODE -------------------

                Console.Write($"\n{i} Product code: ");
                string prodCode = Console.ReadLine();
                Product saledProd = Products.Find(p => p.ProductCode == prodCode);
                if (saledProd != null)
                {
                    // --------------- input SALE İTEM COUNT -------------------

                    Console.Write("Product quantity: ");
                    string ItemCount = Console.ReadLine();
                    int SaleItemCount = To<int>(ItemCount);

                    // --------------- CHECK COUNT IS ZERO OR NOT -------------------

                    while (SaleItemCount == 0)
                    {
                        Console.Write("Product number cannot be 0.Enter again: ");
                        ItemCount = Console.ReadLine();
                        SaleItemCount = To<int>(ItemCount);
                    }

                    // --------------- ASSIGN SALE ITEM DATA -------------------

                    item.ItemNo = i;
                    item.product = saledProd;
                    if (saledProd.Quantity == 0)
                    {
                        Console.WriteLine($"{prodCode} Product with code is out of stock!");
                        continue;
                    }
                    else if (saledProd.Quantity >= SaleItemCount)
                    {
                        item.ProductCount = SaleItemCount;
                        saledProd.Quantity -= SaleItemCount;
                        i++;
                        sale.Amount += saledProd.Price * item.ProductCount;
                        sale.SaleItems.Add(item);
                        Console.WriteLine(saledProd.Name);
                    }
                    else
                    {
                        // -----------------------------------> ASKS USER IF WANTS TO BUY LEFT PRODUCT
                        Console.WriteLine($"It is not possible to sell up to the amount entered. Number of products available: {saledProd.Quantity} Should it be sold up to ? ");
                        Console.WriteLine("Click the \"Enter\" button to confirm, otherwise click the other desired button.");
                        if (Console.ReadKey().Key == ConsoleKey.Enter)
                        {
                            item.ProductCount = saledProd.Quantity;
                            saledProd.Quantity = 0;
                            i++;
                            sale.Amount += saledProd.Price * item.ProductCount;
                            sale.SaleItems.Add(item);
                            Console.WriteLine(saledProd.Name);
                        }
                        else
                        {
                            Console.WriteLine("\nThis product is not sold.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"{prodCode} Product with code is not available!");
                    continue;
                }
                Console.WriteLine("Click the \"Sapce\" button to stop the process and any other button to continue.");
                key = Console.ReadKey();
            } while (key.Key != ConsoleKey.Spacebar);

            sale.Date = DateTime.Now;

            if (sale.SaleItems.Count > 0)
            {
                Sales.Add(sale);
                showSale(sale.SaleNo.ToString());
                Console.WriteLine("Sale added!");
            }
            else Console.WriteLine("No sale added!");
        }

        /// <summary>
        /// Show Sale, including Sale No, Sale Date, Sale Items, Each Saled Product count, Prices, Amount
        /// </summary>
        /// <param name="SaleNo">Sale Number</param>
        public void showSale(string SaleNo)
        {
            Sale sale = Sales.Find(s => s.SaleNo.ToString() == SaleNo);
            try
            {
                if (sale != null)
                {
                    Console.WriteLine();
                    if (sale.SaleItems.Count > 0)
                    {
                        var table = new ConsoleTable("No", "Item No", "Product Name", "Count", "Price", "Amount");
                        int i = 1;
                        foreach (var item in sale.SaleItems)
                        {
                            table.AddRow(i, item.ItemNo, item.product.Name, item.ProductCount, item.product.Price, (item.ProductCount * item.product.Price).ToString("0.00"));
                            i++;
                        }
                        sale.Amount = sale.SaleItems.Sum(s => s.ProductCount * s.product.Price);

                        table.AddRow("", "", "", "", "", "");
                        table.AddRow("Total Amount:", "", "", "", "", sale.Amount.ToString("0.00"));
                        table.AddRow("Date: ", sale.Date, "", "", "Sale No:", sale.SaleNo);

                        table.Write(Format.Minimal);

                    }
                    else Console.WriteLine("The sales list is empty!");
                }
                else throw new SaleException(SaleNo);
            }
            catch (SaleException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Decrease Sale Item Count by given Count
        /// </summary>
        /// <param name="saleNo">Sale number</param>
        public void DeleteSaleItem(string saleNo)
        {
            Sale sale = Sales.Find(s => s.SaleNo.ToString() == saleNo);
            if (sale != null)
            {
                // ---------------------------  Input ItemNo To delete SaleItem -----------------------

                Console.Write("Product number to be removed: ");
                string prodNo = Console.ReadLine();
                int No = To<int>(prodNo);
                SaleItem item = sale.SaleItems.Find(s => s.ItemNo == No);
                if (item != null)
                {
                    // ---------------------------  Input Count To delete SaleItem -----------------------

                    Console.Write("The quantity of products to be removed: ");
                    string count = Console.ReadLine();
                    int delProdCount = To<int>(count);

                    if (delProdCount <= item.ProductCount && delProdCount != 0)
                    {
                        item.ProductCount -= delProdCount;
                        Products.Find(p => p.ProductCode == item.product.ProductCode).Quantity += delProdCount;

                        if (item.ProductCount == 0)
                        {
                            sale.SaleItems.Remove(item);
                        }

                        Console.WriteLine("The product has been removed from sale.");
                        showSale(saleNo);
                    }
                    else if (delProdCount == 0) Console.WriteLine("The number entered is 0. The product was not deleted.");
                    else Console.WriteLine($"{count} sold less than quantity.");
                }
                else Console.WriteLine("The product number is not entered correctly!");
            }
            else throw new SaleException();
        }

        /// <summary>
        /// Delete Sale from Sale List
        /// </summary>
        /// <param name="saleNo">Sale number</param>
        public void DeleteSale(string saleNo)
        {
            Sale sale = Sales.Find(s => s.SaleNo.ToString() == saleNo);
            if (sale != null)
            {
                List<SaleItem> saleItems = sale.SaleItems;
                foreach (SaleItem item in saleItems)
                {
                    Products.Find(p => p.ProductCode == item.product.ProductCode).Quantity += item.ProductCount;
                }
                Sales.Remove(sale);
            }
            else throw new SaleException(saleNo);
        }

        /// <summary>
        /// Finds Sales by given Amount range
        /// </summary>
        /// <param name="mnAmount">Minimum amount</param>
        /// <param name="mxAmount">Maximum amount</param>
        /// <returns>Sale list by Amount range</returns>
        public List<Sale> GetSalesByAmountRange(double mnAmount, double mxAmount)
        {
            return Sales.FindAll(s => s.Amount >= mnAmount && s.Amount <= mxAmount);
        }

        /// <summary>
        /// Finds Sales between two date
        /// </summary>
        /// <param name="startDate">First Sale date</param>
        /// <param name="endDate">Last Sale date</param>
        /// <returns>Sale list by Date range</returns>
        public List<Sale> GetSalesByDateRange(DateTime startDate, DateTime endDate)
        {
            return Sales.FindAll(s => s.Date.Date >= startDate.Date && s.Date.Date <= endDate.Date);
        }

        /// <summary>
        /// Finds Sales by given Date
        /// </summary>
        /// <param name="day">Sale Date</param>
        /// <returns>Sale list by date</returns>
        public List<Sale> GetSalesByDay(DateTime day)
        {
            return Sales.FindAll(s => s.Date.Date == day.Date);
        }

        /// <summary>
        /// Finds Sale by given Sale number
        /// </summary>
        /// <param name="SaleNo">Sale number</param>
        /// <returns>Sale by SaleNo</returns>
        public Sale GetSalesBySaleNo(string SaleNo)
        {
            return Sales.Find(s => s.SaleNo.ToString() == SaleNo);
        }

        /// <summary>
        /// Show Sales by given Sale List
        /// </summary>
        /// <param name="sales">Sale List</param>
        public void ShowSales(List<Sale> sales)
        {
            if (sales.Count > 0)
            {
                var table = new ConsoleTable("No", "Sale No", "Product quantity", "Amount", "Date");
                int i = 1;
                foreach (Sale item in sales)
                {
                    table.AddRow(i, item.SaleNo, item.SaleItems.Count, item.Amount.ToString("0.00"), item.Date);
                    i++;
                }
                table.Write();
            }
            else Console.WriteLine("The sales list is empty.");
        }
        #endregion


    }
}




