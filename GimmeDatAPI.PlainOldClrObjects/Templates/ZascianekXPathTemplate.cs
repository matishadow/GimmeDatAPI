using System;

namespace GimmeDatAPI.PlainOldClrObjects.Templates
{
    public class ZascianekXPathTemplate
    {
        public readonly XPathElement MenuDate = new XPathElement
        {
            Name = nameof(MenuDate),
            Type = typeof(DateTime).ToString(),
            XPathString = "/html/body/main/div/div/div[3]/div[1]/div[1]/div[1]/h2[2]"
        };
        
        public readonly XPathElement Soups = new XPathElement
        {
            Name = nameof(Soups),
            Type = typeof(Array).ToString(),
            XPathString = "/html/body/main/div/div/div[3]/div[1]/div[2]/div[1]"
        };
        public readonly XPathElement MealsOfTheDay = new XPathElement
        {
            Name = nameof(MealsOfTheDay),
            Type = typeof(Array).ToString(),
            XPathString = "/html/body/main/div/div/div[3]/div[1]/div[2]/div[2]"
        };
        public readonly XPathElement DeluxeMeals = new XPathElement
        {
            Name = nameof(DeluxeMeals),
            Type = typeof(Array).ToString(),
            XPathString = "/html/body/main/div/div/div[3]/div[1]/div[2]/div[3]"
        };
    }
}