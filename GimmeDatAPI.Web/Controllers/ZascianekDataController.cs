using System.Threading.Tasks;
using GimmeDatAPI.PlainOldClrObjects;
using GimmeDatAPI.Scraping;
using Microsoft.AspNetCore.Mvc;

namespace GimmeDatAPI.Web.Controllers
{
    [Route("api/[controller]")]
    public class ZascianekDataController : Controller
    {
        private readonly IZascianekScraper zascianekScraper;

        public ZascianekDataController(IZascianekScraper zascianekScraper)
        {
            this.zascianekScraper = zascianekScraper;
        }

        [HttpGet]
        public async Task<ZascianekData> Get()
        {
            ZascianekData zascianekData = await zascianekScraper.ScrapeZascianekData();

            return zascianekData;
        }
    }
}