using System.Threading.Tasks;
using GimmeDatAPI.PlainOldClrObjects;
using GimmeDatAPI.PlainOldClrObjects.Templates;
using GimmeDatAPI.Scraping;
using Microsoft.AspNetCore.Mvc;

namespace GimmeDatAPI.Web.Controllers
{
    [Route("api/[controller]")]
    public class ZascianekDataController : Controller
    {
        private readonly IZascianekMultipleTemplateScraper zascianekScraper;
        private readonly ZascianekXPathTemplates zascianekXPathTemplates;

        public ZascianekDataController(IZascianekMultipleTemplateScraper zascianekScraper,
            ZascianekXPathTemplates zascianekXPathTemplates)
        {
            this.zascianekScraper = zascianekScraper;
            this.zascianekXPathTemplates = zascianekXPathTemplates;
        }

        [HttpGet]
        public async Task<ZascianekData> Get()
        {
            ZascianekData zascianekData = await zascianekScraper.ScrapeZascianekData(zascianekXPathTemplates.Templates);

            return zascianekData;
        }
    }
}