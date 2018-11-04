using System.Threading.Tasks;
using GimmeDatAPI.PlainOldClrObjects;
using GimmeDatAPI.PlainOldClrObjects.Templates;

namespace GimmeDatAPI.Scraping
{
    public interface IZascianekScraper
    {
        Task<ZascianekData> ScrapeZascianekData(ZascianekXPathTemplate zascianekXPathTemplate);
    }
}