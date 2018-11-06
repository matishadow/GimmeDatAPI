using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GimmeDatAPI.Configuration.InversionOfControl.RegistrationRelated;
using GimmeDatAPI.Configuration.InversionOfControl.ScopeRelated;
using GimmeDatAPI.PlainOldClrObjects;
using GimmeDatAPI.PlainOldClrObjects.Templates;
using HtmlAgilityPack;

namespace GimmeDatAPI.Scraping
{
    public class ZascianekScraper : IZascianekScraper, 
        IInstancePerLifetimeScopeDependency, IAsImplementedInterfacesDependency
    {
        private const string ZascianekUrl = "https://kuchniazasciana.pl/dzisiejsze-menu";
        private const string ZascianekDateTimeFormat = "dd.MM.yyyy";
        private const int ZascianekColumnOffset = 2;
        
        private readonly IEnumerable<string> zascianekTags = new[] {"Vege", "Vegan"};
        private readonly IWebDocumentDownloader webDocumentDownloader;
        protected readonly IHtmlNodeConverter HtmlNodeConverter;

        protected HtmlDocument ZascianekWebDocument;

        public ZascianekScraper(IWebDocumentDownloader webDocumentDownloader, IHtmlNodeConverter htmlNodeConverter)
        {
            this.webDocumentDownloader = webDocumentDownloader;
            HtmlNodeConverter = htmlNodeConverter;
        }

        private IEnumerable<string> RemoveTagsFromCollection(IEnumerable<string> collection)
        {
            return collection?.Where(s => !zascianekTags.Contains(s)).ToList();
        }

        private void RemoveTagsFromZascianekData(ZascianekData zascianekData)
        {
            zascianekData.Menu.Soups = RemoveTagsFromCollection(zascianekData.Menu.Soups);
            zascianekData.Menu.MealsOfTheDay = RemoveTagsFromCollection(zascianekData.Menu.MealsOfTheDay);
            zascianekData.Menu.DeluxeMeals = RemoveTagsFromCollection(zascianekData.Menu.DeluxeMeals);
        }

        protected async Task TryFillUpZascianekWebDocument()
        {
            if (ZascianekWebDocument == null)
                ZascianekWebDocument = await webDocumentDownloader.DownloadWebDocument(ZascianekUrl);
        }

        public virtual async Task<ZascianekData> ScrapeZascianekData(ZascianekXPathTemplate zascianekXPathTemplate)
        {
            var zascianekData = new ZascianekData();

            await TryFillUpZascianekWebDocument();

            zascianekData.MenuDate =
                HtmlNodeConverter.ConvertToDateTime(ZascianekWebDocument,
                    zascianekXPathTemplate.MenuDate.XPathString, ZascianekDateTimeFormat);

            IEnumerable<string> soups = HtmlNodeConverter.ConvertToStrings(ZascianekWebDocument,
                zascianekXPathTemplate.Soups.XPathString);
            IEnumerable<string> mealsOfTheDay = HtmlNodeConverter.ConvertToStrings(ZascianekWebDocument,
                zascianekXPathTemplate.MealsOfTheDay.XPathString);
            IEnumerable<string> deluxeMeals = HtmlNodeConverter.ConvertToStrings(ZascianekWebDocument,
                zascianekXPathTemplate.DeluxeMeals.XPathString);
            
            zascianekData.Menu = new Menu
            {
                Soups = soups?.Skip(ZascianekColumnOffset)?.ToList(),
                MealsOfTheDay = mealsOfTheDay?.Skip(ZascianekColumnOffset)?.ToList(),
                DeluxeMeals = deluxeMeals?.Skip(ZascianekColumnOffset)?.ToList(),
            };
            
            RemoveTagsFromZascianekData(zascianekData);

            return zascianekData;
        }
    }
}