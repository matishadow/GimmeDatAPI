using System.Threading.Tasks;
using GimmeDatAPI.PlainOldClrObjects;

namespace GimmeDatAPI.Scraping
{
    public interface IZascianekWithPricesScraper
    {
        Task<ZascianekDataWithPrices> ScrapeZascianekWithPricesData();
    }
}