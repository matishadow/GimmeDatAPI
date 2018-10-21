using System;
using System.Threading.Tasks;
using GimmeDatAPI.Configuration.InversionOfControl.RegistrationRelated;
using GimmeDatAPI.Configuration.InversionOfControl.ScopeRelated;
using HtmlAgilityPack;

namespace GimmeDatAPI.Scraping
{
    public class WebDocumentDownloader : IWebDocumentDownloader,
        IInstancePerLifetimeScopeDependency, IAsImplementedInterfacesDependency
    {
        public Task<HtmlDocument> DownloadWebDocument(string url)
        {
            var web = new HtmlWeb();
            Task<HtmlDocument> htmlDocumentTask = web.LoadFromWebAsync(url);
            
            return htmlDocumentTask;
        }
    }
}