using System.Threading.Tasks;
using GimmeDatAPI.PlainOldClrObjects;
using GimmeDatAPI.PlainOldClrObjects.Templates;
using GimmeDatAPI.Scraping;
using Microsoft.AspNetCore.Mvc;

namespace GimmeDatAPI.Web.Controllers
{
    [Route("api/[controller]")]
    public class ZascianekWithPricesDataController : Controller
    {
        private readonly IZascianekWithPricesScraper zascianekWithPricesScraper;
        private readonly ZascianekXPathTemplate zascianekXPathTemplate;

        public ZascianekWithPricesDataController(IZascianekWithPricesScraper zascianekWithPricesScraper)
        {
            this.zascianekWithPricesScraper = zascianekWithPricesScraper;
            zascianekXPathTemplate = new ZascianekXPathTemplate();
        }

        [HttpGet]
        public async Task<ZascianekDataWithPrices> Get()
        {
            ZascianekDataWithPrices zascianekDataWithPrices =
                await zascianekWithPricesScraper.ScrapeZascianekWithPricesData(zascianekXPathTemplate);

            return zascianekDataWithPrices;
        }
    }
}