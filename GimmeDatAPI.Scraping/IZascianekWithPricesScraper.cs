using System.Threading.Tasks;
using GimmeDatAPI.PlainOldClrObjects;
using GimmeDatAPI.PlainOldClrObjects.Templates;

namespace GimmeDatAPI.Scraping
{
    public interface IZascianekWithPricesScraper
    {
        Task<ZascianekDataWithPrices> ScrapeZascianekWithPricesData(ZascianekXPathTemplate zascianekXPathTemplate);
    }
}