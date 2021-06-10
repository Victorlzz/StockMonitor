using HtmlAgilityPack;
using Stock.Contract.GatewayContract;
using Stock.Contract.StockContract;
using Stock.VO.Stock;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace Stock.Service.StockService
{
    public class StockMonitorService : IStockMonitorService
    {
        private readonly IGatewayServiceProvider _gatewayServiceProvider;

        public StockMonitorService(IGatewayServiceProvider gatewayServiceProvider)
        {
            _gatewayServiceProvider = gatewayServiceProvider;
        }

        public async Task Monitor()
        {
            // Do not run when Saturday and Sunday
            if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                return;

            IStockHtmlAgilityPackService stockHtmlAgilityPackService = this._gatewayServiceProvider.Get<IStockHtmlAgilityPackService>();
            List<Quote> quotes = await this._gatewayServiceProvider.Get<IStockQuoteService>().GetAll();
            if (quotes is null || quotes.Count == 0)
                return;

            foreach (Quote quote in quotes)
            {
                string url = $"https://finance.yahoo.com/quote/{quote.TickUrl}?p={quote.TickUrl}";
                HtmlDocument htmlDocument = await stockHtmlAgilityPackService.GetDocument(url);
                if (htmlDocument is null)
                    break;

                HtmlNodeCollection nodes = await stockHtmlAgilityPackService.GetNodes(htmlDocument, "/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[5]/div/div/div/div[3]/div[1]/div/span[1]");
                quote.UpdatedAt = DateTime.Now;
                quote.Value = decimal.Parse(nodes[0].InnerText, new CultureInfo("pt-BR"));

                nodes = await stockHtmlAgilityPackService.GetNodes(htmlDocument, "/html/body/div[1]/div/div/div[1]/div/div[2]/div/div/div[5]/div/div/div/div[2]/div[1]/div[1]/h1");
                quote.CompanyName = nodes[0].InnerText;
            }
        }
    }
}
