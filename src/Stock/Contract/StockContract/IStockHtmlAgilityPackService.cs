using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Contract.StockContract
{
    public interface IStockHtmlAgilityPackService
    {
        Task<HtmlDocument> GetDocument(string url);
        Task<HtmlNodeCollection> GetNodes(HtmlDocument doc, string xPath);
    }
}