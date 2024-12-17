using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockQuoteTracking.src.Models
{
    public class Stock
    {
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double PurchasePrice { get; set; }

        public Stock(string name, double salePrice, double purchasePrice)
        {
            Name = name;
            SalePrice = salePrice;
            PurchasePrice = purchasePrice;
        }
    }

}
