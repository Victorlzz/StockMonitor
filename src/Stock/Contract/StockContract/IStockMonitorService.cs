using System;
using System.Threading.Tasks;

namespace Stock.Contract.StockContract
{
    public interface IStockMonitorService
    {
        Task Monitor();
    }
}
