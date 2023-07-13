using Market_Console.Common.Market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_Console.Common.Interface
{
    public interface IMarketable
    {
        // PRODUCT
        public List<Product> Products { get; }
        public void AddProduct();
        public void EditProductInfo(string ProductCode);
        public void DeleteProduct(string ProductCode);
        public void GetProductByCategory(string category);
        public void GetProductByPriceRange(string minPrice, string maxPrice);
        public List<Product> GetProductByName(string productName);

        // SALE
        public List<Sale> Sales { get; set; }
        public void AddSale();
        public void DeleteSaleItem(string saleNo);
        public void ShowSales(List<Sale> sales);
        public List<Sale> GetSalesByDateRange(DateTime startDate, DateTime endDate);
        public List<Sale> GetSalesByDay(DateTime day);
        public List<Sale> GetSalesByAmountRange(double mnAmount, double mxAmount);
        public Sale GetSalesBySaleNo(string SaleNo);

    }
}
