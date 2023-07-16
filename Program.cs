using Market_Console.Common.Enums;
using Market_Console.Common.Exceptions;
using Market_Console.Common.Interface;
using Market_Console.Common.Market;
using Market_Console.Services;
using System.Text;

namespace Market_Console
{
    public class Program
    {
        //Market management system
        //Program for managing products in the market

        static MarketService operations = new MarketService();
        #region Check Is Number
        /// <summary>
        /// Converts String type to given Number Type 
        /// </summary>
        /// <typeparam name="T">required DataType</typeparam>
        /// <param name="value">Given string value</param>
        /// <returns>Number typed value</returns>
        public static T To<T>(string value)
        {
            bool converted = false;
            T numb = default(T);
            while (!converted)
            {
                try
                {
                    numb = (T)Convert.ChangeType(value, typeof(T));
                    converted = true;
                }
                catch
                {
                    converted = false;
                    Console.Write("Enter a number: ");
                    value = Console.ReadLine();
                }
            }
            return numb;
        }

        #endregion

        #region Check Is Date
        /// <summary>
        /// Converts String type to DateTime type
        /// </summary>
        /// <param name="value">Must be date format</param>
        /// <returns>DateTime value</returns>
        public static DateTime ToDate(string value)
        {
            bool converted = false;
            DateTime date = default(DateTime);
            while (!converted)
            {
                try
                {
                    date = (DateTime)Convert.ChangeType(value, typeof(DateTime));
                    converted = true;
                }
                catch
                {
                    converted = false;
                    Console.WriteLine("You have not entered the date correctly. Try again!");
                    Console.Write("Date (dd.MM.yyyy HH:mm:ss) : ");
                    value = Console.ReadLine();
                }
            }
            return date;
        }

        #endregion


        static void Main(string[] args)
        {            
            Console.OutputEncoding = Encoding.UTF8;

            Console.WriteLine("1 - 3 Enter a number between ");
            Console.WriteLine("- 1 Transaction on products\n" +
                              "- 2 Transaction on sales\n" +
                              "- 3 Sign out");
            int SelectInt;

            do
            {
                Console.Write("Your choice: ");
                string select = Console.ReadLine();
                while (!int.TryParse(select, out SelectInt))
                {
                    Console.WriteLine("Enter a number.");
                    Console.Write("Your choice: ");
                    select = Console.ReadLine();
                }
                switch (SelectInt)
                {
                    case 1:
                        Console.WriteLine("---------------- Transactions on products ----------------");
                        showProductChoices();
                        ProductOperations();
                        break;
                    case 2:
                        Console.WriteLine("---------------- Transactions on sales -----------------");
                        showSaleChoices();
                        SaleOperations();
                        break;
                    case 3:
                        Console.WriteLine("You have logged out.");
                        break;
                    default:
                        Console.WriteLine("You can enter a number between 1 and 3 to perform an operation.");
                        continue;
                }


            } while (SelectInt != 3);

        }

        #region Product operations
        static void ProductOperations()
        {
            Console.OutputEncoding = Encoding.UTF8;
            int SlctInt;
            do
            {
                Console.Write("Your choice: ");
                string slct = Console.ReadLine();
                while (!int.TryParse(slct, out SlctInt))
                {
                    Console.WriteLine("Enter a number.");
                    slct = Console.ReadLine();
                }
                switch (SlctInt)
                {
                    case 0:
                        Console.WriteLine("1 - 3 Enter a number between: ");
                        Console.WriteLine("- 1 Transaction on products\n" +
                                          "- 2 Transaction on sales\n" +
                                          "- 3 Sign out");
                        break;
                    case 1:
                        Console.WriteLine("---- Adding new products ----");
                        AddProduct();
                        break;
                    case 2:
                        Console.WriteLine("-- Amending product information --");
                        EditProduct();
                        break;
                    case 3:
                        Console.WriteLine("------ Product Removal ------");
                        DeleteProduct();
                        break;
                    case 4:
                        Console.WriteLine("-------------- All products --------------");
                        operations.ShowProducts(operations.Products);
                        showProductChoices();
                        break;
                    case 5:
                        Console.WriteLine("-------------- Products by category --------------");
                        showProductsByCategory();
                        break;
                    case 6:
                        Console.WriteLine("------------------ Products by price range ------------------");
                        showProductsByPriceRange();
                        break;
                    case 7:
                        Console.WriteLine("------------------ Search products by name ------------------");
                        showProductByName();
                        break;
                    default:
                        Console.WriteLine("You can enter a number between 0 and 7 to perform an operation.");
                        break;
                }
            } while (SlctInt != 0);

        }

        static void showProductChoices()
        {
            Console.WriteLine("");
            Console.WriteLine("Enter a number in the range 0-7 to perform a transaction on products: ");
            Console.WriteLine(" 1 - Adding new products\n" +
                              " 2 - Making corrections on the product\n" +
                              " 3 - Product Removal\n" +
                              " 4 - Showing all products\n" +
                              " 5 - Displaying products by category\n" +
                              " 6 - Displaying products by price range\n" +
                              " 7 -Search products by name\n" +
                              " 0 - Exit transactions on products");
            Console.WriteLine("");
        }
        static void AddProduct()
        {
            Console.OutputEncoding = Encoding.UTF8;
            operations.AddProduct();
            Console.WriteLine("\n Product added. ");
            showProductChoices();
        }
        static void EditProduct()
        {
            Console.Write("Enter the product code you want to edit: ");
            string prodCode = Console.ReadLine();
            try
            {
                operations.EditProductInfo(prodCode);
            }
            catch (ProductException ex)
            {
                Console.WriteLine(ex.Message);
            }
            showProductChoices();
        }
        static void DeleteProduct()
        {
            Console.Write("Enter the product code you want to remove: ");
            string prodCode = Console.ReadLine();
            try
            {
                operations.DeleteProduct(prodCode);
            }
            catch (ProductException ex)
            {
                Console.WriteLine(ex.Message);
            }
            showProductChoices();
        }
        static void showProductsByCategory()
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

            // --------------------------------------  ASSIGN CATEGORY ---------------------------------------

            Console.Write("Enter the category: ");
            string category = Console.ReadLine();
            try
            {
                operations.GetProductByCategory(category);
            }
            catch (ProductCategoryException ex)
            {
                Console.WriteLine(ex.Message);
            }
            showProductChoices();
        }
        static void showProductsByPriceRange()
        {
            Console.Write("Minimum price: ");
            string minPrice = Console.ReadLine();

            Console.Write("Maximum price: ");
            string maxPrice = Console.ReadLine();
            operations.GetProductByPriceRange(minPrice, maxPrice);
            showProductChoices();
        }
        static void showProductByName()
        {
            Console.Write("The name of the product: ");
            string name = Console.ReadLine();
            List<Product> products = operations.GetProductByName(name);
            if (products.Count != 0)
            {
                operations.ShowProducts(products);
            }
            else Console.WriteLine($"{name} The product named is not available.");
            showProductChoices();
        }
        #endregion

        #region Sale operations
        static void SaleOperations()
        {
            int SlctInt;
            do
            {
                Console.Write("Your choice: ");
                string slct = Console.ReadLine();
                Console.WriteLine("");
                while (!int.TryParse(slct, out SlctInt))
                {
                    Console.WriteLine("Enter a number.");
                    Console.Write("Your choice: ");
                    slct = Console.ReadLine();
                }
                switch (SlctInt)
                {
                    case 0:
                        Console.WriteLine("1 - 3 Enter a number between: ");
                        Console.WriteLine("- 1 Transaction on products\n" +
                                          "- 2 Transaction on sales\n" +
                                          "- 3 Sign out");
                        break;
                    case 1:
                        Console.WriteLine("==========================================================================================");
                        Console.WriteLine("\n-----------------------  ADD A SALE ------------------------");
                        addSale();
                        break;
                    case 2:
                        Console.WriteLine("==========================================================================================");
                        Console.WriteLine("\n----------------------- PRODUCT RETURN -----------------------");
                        deleteSaleItem();
                        break;
                    case 3:
                        Console.WriteLine("==========================================================================================");
                        Console.WriteLine("\n----------------------- DELETE OF SALE -----------------------");
                        deleteSale();
                        break;
                    case 4:
                        Console.WriteLine("==========================================================================================");
                        Console.WriteLine("\n----------------------- ALL SALES -----------------------");
                        operations.ShowSales(operations.Sales);
                        showSaleChoices();
                        break;
                    case 5:
                        Console.WriteLine("==========================================================================================");
                        Console.WriteLine("\n--------------------- SALES BY SELECTED DATE RANGE --------------------");
                        ShowSalesByDateRange();
                        break;
                    case 6:
                        Console.WriteLine("==========================================================================================");
                        Console.WriteLine("--------------------- SALES BY SELECTED AMOUNT RANGE ----------------------");
                        ShowSalesByAmountRange();
                        break;
                    case 7:
                        Console.WriteLine("==========================================================================================");
                        Console.WriteLine("\n----------------------- SALES BY SELECTED DATE -----------------------");
                        ShowSalesByDay();
                        break;
                    case 8:
                        Console.WriteLine("==========================================================================================");
                        Console.WriteLine("\n-------------- EXTRACTING SALES INFORMATION BY SALES NUMBER --------------");
                        Console.Write("Sales number: ");
                        string saleNo = Console.ReadLine();
                        operations.showSale(saleNo);
                        showSaleChoices();
                        break;
                    default:
                        Console.WriteLine("You can enter a number between 1 and 8 to perform an operation.");
                        break;
                }


            } while (SlctInt != 0);

        }
        static void showSaleChoices()
        {
            Console.WriteLine("");
            Console.WriteLine("- 1 Add a new sale\n" +
                              "- 2 Return of the product on sale\n" +
                              "- 3 Deletion of Sale \n" +
                              "- 4 Display of all sales\n" +
                              "- 5 Display of sales for a given date range\n" +
                              "- 6 Display sales by given amount range\n" +
                              "- 7 Showing sales for a given date\n" +
                              "- 8 According to the given number, displaying the information of the sale of that number\n" +
                              "- 0 Exit sales transactions");
            Console.WriteLine("");
        }
        static void addSale()
        {
            operations.AddSale();
            Console.WriteLine("==========================================================================================");
            showSaleChoices();
        }
        static void deleteSale()
        {
            Console.Write("Enter the sales number: ");
            string saleNo = Console.ReadLine();
            try
            {
                operations.DeleteSale(saleNo);
            }
            catch (SaleException ex)
            {

                Console.WriteLine(ex.Message);
            }
            showSaleChoices();
        }
        static void deleteSaleItem()
        {
            Console.Write("Sales number: ");
            string saleNo = Console.ReadLine();
            try
            {
                operations.DeleteSaleItem(saleNo);
            }
            catch (SaleException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("==========================================================================================");
            showSaleChoices();
        }
        static void ShowSalesByAmountRange()
        {
            List<Sale> sales;
            Console.Write("Minimum amount: ");

            string amountMin = Console.ReadLine();
            double mnAmount = To<double>(amountMin);

            Console.Write("Maximum amount: ");
            string amountMax = Console.ReadLine();
            double mxAmount = To<double>(amountMax);

            if (mnAmount < mxAmount)
            {
                sales = operations.GetSalesByAmountRange(mnAmount, mxAmount);
            }
            else
            {
                sales = null;
                Console.WriteLine("You have not entered the interval correctly.");
            }

            if (sales != null)
            {
                operations.ShowSales(sales);
            }
            else Console.WriteLine("No sales found in this amount range.");
            showSaleChoices();

        }
        static void ShowSalesByDateRange()
        {
            Console.Write("Start date (MM.dd.yyyy): ");
            string startDate = Console.ReadLine();
            DateTime dateSt = ToDate(startDate);

            Console.Write("Last date (MM.dd.yyyy): ");
            string endDate = Console.ReadLine();
            DateTime dateEnd = ToDate(endDate);

            List<Sale> sales = operations.GetSalesByDateRange(dateSt, dateEnd);
            operations.ShowSales(sales);
            showSaleChoices();
        }
        static void ShowSalesByDay()
        {
            Console.Write("Date: (dd.MM.yyyy) ");
            string dt = Console.ReadLine();
            DateTime date = ToDate(dt);
            List<Sale> sales = operations.GetSalesByDay(date);
            operations.ShowSales(sales);
            Console.WriteLine(date);
            showSaleChoices();
        }
        #endregion
    }
}