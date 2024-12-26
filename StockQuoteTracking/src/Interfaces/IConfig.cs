using StockQuoteTracking.src.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockQuoteTracking.src.Interfaces
{
    public interface IConfig
    {
        EmailSettings LoadEmailSettings();
    }
}
