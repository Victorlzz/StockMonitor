using HtmlAgilityPack;
using Stock.Contract.StockContract;
using System.Threading.Tasks;

namespace Stock.Service.StockService
{
    public class StockHtmlAgilityPackService : IStockHtmlAgilityPackService
    {
        public async Task<HtmlDocument> GetDocument(string url)
        {
            try
            {
                var web = new HtmlWeb();
                return await Task.FromResult(web.Load(url));
            }
            catch
            {
                return null;
            }
        }
        public async Task<HtmlNodeCollection> GetNodes(HtmlDocument doc, string xPath)
        {
            return await Task.FromResult(doc.DocumentNode.SelectNodes(xPath));
        }
    }
}