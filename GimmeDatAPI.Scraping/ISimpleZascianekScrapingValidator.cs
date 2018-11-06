using GimmeDatAPI.PlainOldClrObjects;

namespace GimmeDatAPI.Scraping
{
    public interface ISimpleZascianekScrapingValidator
    {
        bool WasScrapingSuccessful(ZascianekData zascianekData);
    }
}