using Stock.Contract.StockContract;
using Stock.VO.Stock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Service.StockService
{
    public class StockQuoteService : IStockQuoteService
    {
        public async Task<List<Quote>> GetAll()
        {
            List<Quote> quotes = new List<Quote>();
            quotes.Add(new Quote { Tick = "PETR4" });
            quotes.Add(new Quote { Tick = "VALE3" });
            return await Task.FromResult(quotes);
        }
    }
}