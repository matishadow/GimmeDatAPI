using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace GimmeDatAPI.Scraping
{
    public class HtmlNodeConverter : IHtmlNodeConverter
    {
        private const string CollectionXPath = "//text()[normalize-space(.) != '']";
        
        public DateTime ConvertToDateTime(HtmlDocument htmlDocument, string xpath, string dateTimeFormat)
        {
            DateTime convertedDateTime = DateTime.ParseExact(ConvertToString(htmlDocument, xpath), dateTimeFormat,
                System.Globalization.CultureInfo.InvariantCulture);

            return convertedDateTime;
        }

        public string ConvertToString(HtmlDocument htmlDocument, string xpath)
        {
            string convertedString = htmlDocument.DocumentNode
                .SelectNodes(xpath)
                .First().InnerText;

            return convertedString;
        }

        public IEnumerable<string> ConvertToStrings(HtmlDocument htmlDocument, string xpath)
        {
            HtmlNodeCollection collectionNode = htmlDocument.DocumentNode.SelectNodes(xpath + CollectionXPath);

            IEnumerable<string> convertedStrings =
                collectionNode
                    .Where(node => !string.IsNullOrWhiteSpace(node.InnerText))
                    .Select(node => node.InnerText.Trim())
                    .ToList();

            return convertedStrings;
        }
    }
}