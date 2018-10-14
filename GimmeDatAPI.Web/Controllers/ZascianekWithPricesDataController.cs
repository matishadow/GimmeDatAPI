using System.Threading.Tasks;
using GimmeDatAPI.PlainOldClrObjects;
using GimmeDatAPI.Scraping;
using Microsoft.AspNetCore.Mvc;

namespace GimmeDatAPI.Web.Controllers
{
    [Route("api/[controller]")]
    public class ZascianekWithPricesDataController : Controller
    {
        private readonly IZascianekWithPricesScraper zascianekWithPricesScraper;

        public ZascianekWithPricesDataController(IZascianekWithPricesScraper zascianekWithPricesScraper)
        {
            this.zascianekWithPricesScraper = zascianekWithPricesScraper;
        }

        [HttpGet]
        public async Task<ZascianekDataWithPrices> Get()
        {
            ZascianekDataWithPrices zascianekDataWithPrices =
                await zascianekWithPricesScraper.ScrapeZascianekWithPricesData();

            return zascianekDataWithPrices;
        }
    }
}