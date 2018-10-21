namespace GimmeDatAPI.PlainOldClrObjects.Templates
{
    public class ZascianekPricesXPathTemplate 
    {
        public XPathElement SoupPrice = new XPathElement()
        {
            Name = nameof(SoupPrice),
            Type = typeof(string).ToString(),
            XPathString = "/html/body/main/div/div/div[3]/div/div[1]/div[3]/div[1]/p"
        };
        public XPathElement StandardMealPrice = new XPathElement()
        {
            Name = nameof(StandardMealPrice),
            Type = typeof(string).ToString(),
            XPathString = "/html/body/main/div/div/div[3]/div/div[1]/div[3]/div[2]/p"
        };
        public XPathElement DeluxeMealPrice = new XPathElement()
        {
            Name = nameof(DeluxeMealPrice),
            Type = typeof(string).ToString(),
            XPathString = "/html/body/main/div/div/div[3]/div/div[1]/div[3]/div[3]/p"
        };
    }
}