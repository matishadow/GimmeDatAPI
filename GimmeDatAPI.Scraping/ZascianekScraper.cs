using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GimmeDatAPI.PlainOldClrObjects;
using GimmeDatAPI.PlainOldClrObjects.Templates;
using HtmlAgilityPack;

namespace GimmeDatAPI.Scraping
{
    public class ZascianekScraper
    {
        private const string ZascianekUrl = "https://kuchniazasciana.pl/dzisiejsze-menu";
        private const string ZascianekDateTimeFormat = "dd.MM.yyyy";
        private const int ZascianekColumnOffset = 2;
        
        private readonly IEnumerable<string> zascianekTags = new[] {"Vege", "Vegan"};
        private readonly IWebDocumentDownloader webDocumentDownloader;
        private readonly IHtmlNodeConverter htmlNodeConverter;
        private readonly ZascianekXPathTemplate zascianekXPathTemplate;

        public ZascianekScraper(IWebDocumentDownloader webDocumentDownloader, IHtmlNodeConverter htmlNodeConverter)
        {
            this.webDocumentDownloader = webDocumentDownloader;
            this.htmlNodeConverter = htmlNodeConverter;
            this.zascianekXPathTemplate = new ZascianekXPathTemplate();
        }

        private IList<string> RemoveTagsFromCollection(IEnumerable<string> collection)
        {
            return collection.Where(s => !zascianekTags.Contains(s)).ToList();
        }

        private void RemoveTagsFromZascianekData(ZascianekData zascianekData)
        {
            zascianekData.Menu.Soups = RemoveTagsFromCollection(zascianekData.Menu.Soups);
            zascianekData.Menu.MealsOfTheDay = RemoveTagsFromCollection(zascianekData.Menu.MealsOfTheDay);
            zascianekData.Menu.DeluxeMeals = RemoveTagsFromCollection(zascianekData.Menu.DeluxeMeals);
        }

        public async Task<ZascianekData> ScrapeZascianekData()
        {
            var zascianekData = new ZascianekData();

            HtmlDocument webDocument = await webDocumentDownloader.DownloadWebDocument(ZascianekUrl);
            
            zascianekData.MenuDate =
                htmlNodeConverter.ConvertToDateTime(webDocument,
                    zascianekXPathTemplate.MenuDate.XPathString, ZascianekDateTimeFormat);

            IEnumerable<string> soups = htmlNodeConverter.ConvertToStrings(webDocument,
                zascianekXPathTemplate.Soups.XPathString);
            IEnumerable<string> mealsOfTheDay = htmlNodeConverter.ConvertToStrings(webDocument,
                zascianekXPathTemplate.MealsOfTheDay.XPathString);
            IEnumerable<string> deluxeMeals = htmlNodeConverter.ConvertToStrings(webDocument,
                zascianekXPathTemplate.DeluxeMeals.XPathString);
            
            zascianekData.Menu = new Menu
            {
                Soups = soups.Skip(ZascianekColumnOffset).ToList(),
                MealsOfTheDay = mealsOfTheDay.Skip(ZascianekColumnOffset).ToList(),
                DeluxeMeals = deluxeMeals.Skip(ZascianekColumnOffset).ToList(),
            };
            
            RemoveTagsFromZascianekData(zascianekData);

            return zascianekData;
        }
    }
}