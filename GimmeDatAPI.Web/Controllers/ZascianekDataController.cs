using System;
using System.Threading.Tasks;
using FluentCache;
using GimmeDatAPI.Cache;
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
        private readonly ICacheInvalidation cacheInvalidation;
        private readonly IGimmeDatCache gimmeDatCache;

        public ZascianekDataController(IZascianekMultipleTemplateScraper zascianekScraper,
            ZascianekXPathTemplates zascianekXPathTemplates, ICacheInvalidation cacheInvalidation,
            IGimmeDatCache gimmeDatCache)
        {
            this.zascianekScraper = zascianekScraper;
            this.zascianekXPathTemplates = zascianekXPathTemplates;
            this.cacheInvalidation = cacheInvalidation;
            this.gimmeDatCache = gimmeDatCache;
        }

        [HttpGet]
        public async Task<ZascianekData> Get()
        {
            Cache<IZascianekMultipleTemplateScraper> scraperCache = gimmeDatCache.GetCacheWrapper(zascianekScraper);

            ZascianekData zascianekData = await scraperCache
                .Method(scraper => scraper.ScrapeZascianekData(zascianekXPathTemplates.Templates))
                .ExpireAfter(cacheInvalidation.GetZascianekExpireAfter()).GetValueAsync();
            
            return zascianekData;
        }
    }
}