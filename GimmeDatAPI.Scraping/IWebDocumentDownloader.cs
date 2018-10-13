using System.Threading.Tasks;
using HtmlAgilityPack;

namespace GimmeDatAPI.Scraping
{
    public interface IWebDocumentDownloader
    {
        Task<HtmlDocument> DownloadWebDocument(string url);
    }
}