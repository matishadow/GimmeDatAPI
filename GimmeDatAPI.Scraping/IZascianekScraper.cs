using System.Threading.Tasks;
using GimmeDatAPI.PlainOldClrObjects;

namespace GimmeDatAPI.Scraping
{
    public interface IZascianekScraper
    {
        Task<ZascianekData> ScrapeZascianekData();
    }
}