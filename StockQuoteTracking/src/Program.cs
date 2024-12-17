using System.Text.Json;
using StockQuoteTracking.src.Models;

namespace StockQuoteTracking.src
{
    public class Program
    {
        public static void Main(string[] args)
        {

            if (args.Length < 3)
            {
                Console.WriteLine("You need to send the three arguments! \n " +
                    "StcokQuoteTracking.exe <stock><sale-price><purchase-price>");
                return;
            }

            string stockName = args[0];
            double salePrice = double.Parse(args[1]);
            double purchasePrice = double.Parse(args[2]);

            Stock stock = new Stock(stockName, salePrice, purchasePrice);

            Console.WriteLine(JsonSerializer.Serialize(stock));

        }
    }
}
