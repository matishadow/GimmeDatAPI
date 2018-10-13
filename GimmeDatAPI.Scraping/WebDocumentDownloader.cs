using System;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace GimmeDatAPI.Scraping
{
    public class WebDocumentDownloader : IWebDocumentDownloader
    {
        public Task<HtmlDocument> DownloadWebDocument(string url)
        {
            var web = new HtmlWeb();
            Task<HtmlDocument> htmlDocumentTask = web.LoadFromWebAsync(url);
            
            return htmlDocumentTask;
        }
    }
}