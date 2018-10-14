using System.Threading.Tasks;
using GimmeDatAPI.PlainOldClrObjects;
using GimmeDatAPI.PlainOldClrObjects.Templates;

namespace GimmeDatAPI.Scraping
{
    public class ZascianekWithPricesScraper : ZascianekScraper, IZascianekWithPricesScraper
    {
        private readonly ZascianekPricesXPathTemplate zascianekPricesXPathTemplate;

        public ZascianekWithPricesScraper(IWebDocumentDownloader webDocumentDownloader,
            IHtmlNodeConverter htmlNodeConverter, ZascianekXPathTemplate zascianekXPathTemplate,
            ZascianekPricesXPathTemplate zascianekPricesXPathTemplate) : base(
            webDocumentDownloader, htmlNodeConverter, zascianekXPathTemplate)
        {
            this.zascianekPricesXPathTemplate = zascianekPricesXPathTemplate;
        }

        public async Task<ZascianekDataWithPrices> ScrapeZascianekWithPricesData()
        {
            var zascianekDataWithPrices = new ZascianekDataWithPrices()
            {
                ZascianekData = await ScrapeZascianekData()
            };
            
            await TryFillUpZascianekWebDocument();

            string soupPrice = HtmlNodeConverter.ConvertToString(ZascianekWebDocument,
                zascianekPricesXPathTemplate.SoupPrice.XPathString);
            string standardMealPrice = HtmlNodeConverter.ConvertToString(ZascianekWebDocument,
                zascianekPricesXPathTemplate.StandardMealPrice.XPathString);
            string deluxeMealPrice = HtmlNodeConverter.ConvertToString(ZascianekWebDocument,
                zascianekPricesXPathTemplate.DeluxeMealPrice.XPathString);

            zascianekDataWithPrices.MenuPrices = new MenuPrices
            {
                SoupPrice = soupPrice,
                StandardMealPrice = standardMealPrice,
                DeluxeMealPrice = deluxeMealPrice
            };

            return zascianekDataWithPrices;
        }
    }
}