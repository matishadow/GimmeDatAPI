using System;
using System.Collections.Generic;
using HtmlAgilityPack;

namespace GimmeDatAPI.Scraping
{
    public interface IHtmlNodeConverter
    {
        DateTime ConvertToDateTime(HtmlDocument htmlDocument, string xpath, string dateTimeFormat);
        string ConvertToString(HtmlDocument htmlDocument, string xpath);
        IEnumerable<string> ConvertToStrings(HtmlDocument htmlDocument, string xpath);
    }
}