using System.Collections.Generic;
using System.Threading.Tasks;
using GimmeDatAPI.PlainOldClrObjects;
using GimmeDatAPI.PlainOldClrObjects.Templates;

namespace GimmeDatAPI.Scraping
{
    public interface IZascianekMultipleTemplateScraper
    {
        Task<ZascianekData> ScrapeZascianekData(IEnumerable<ZascianekXPathTemplate> zascianekXPathTemplates);
    }
}