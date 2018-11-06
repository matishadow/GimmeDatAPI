using System.Collections.Generic;
using System.Threading.Tasks;
using GimmeDatAPI.Configuration.InversionOfControl.RegistrationRelated;
using GimmeDatAPI.Configuration.InversionOfControl.ScopeRelated;
using GimmeDatAPI.PlainOldClrObjects;
using GimmeDatAPI.PlainOldClrObjects.Templates;

namespace GimmeDatAPI.Scraping
{
    public class ZascianekMultipleTemplateScraper : IZascianekMultipleTemplateScraper,
        IInstancePerLifetimeScopeDependency, IAsImplementedInterfacesDependency
    {
        private readonly IZascianekScraper zascianekScraper;
        
        private readonly ISimpleZascianekScrapingValidator simpleZascianekScrapingValidator;

        public ZascianekMultipleTemplateScraper(IZascianekScraper zascianekScraper,
            ISimpleZascianekScrapingValidator simpleZascianekScrapingValidator)
        {
            this.zascianekScraper = zascianekScraper;
            this.simpleZascianekScrapingValidator = simpleZascianekScrapingValidator;
        }

        public async Task<ZascianekData> ScrapeZascianekData(IEnumerable<ZascianekXPathTemplate> zascianekXPathTemplates)
        {
            ZascianekData zascianekData = null;
            
            foreach (ZascianekXPathTemplate template in zascianekXPathTemplates)
            {
                zascianekData = await zascianekScraper.ScrapeZascianekData(template);
                
                if (simpleZascianekScrapingValidator.WasScrapingSuccessful(zascianekData))
                    break;
            }

            return zascianekData;
        }
    }
}