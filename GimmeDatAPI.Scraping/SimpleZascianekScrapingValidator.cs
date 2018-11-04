using System.Linq;
using GimmeDatAPI.Configuration.InversionOfControl.RegistrationRelated;
using GimmeDatAPI.Configuration.InversionOfControl.ScopeRelated;
using GimmeDatAPI.PlainOldClrObjects;

namespace GimmeDatAPI.Scraping
{
    public class SimpleZascianekScrapingValidator : ISimpleZascianekScrapingValidator,
        IInstancePerLifetimeScopeDependency, IAsImplementedInterfacesDependency
    {
        public bool WasScrapingSuccessful(ZascianekData zascianekData)
        {
            if (zascianekData?.Menu.Soups == null || !zascianekData.Menu.Soups.Any()) return false;
            if (zascianekData.Menu.MealsOfTheDay == null || !zascianekData.Menu.MealsOfTheDay.Any()) return false;
            if (zascianekData.Menu.DeluxeMeals == null || !zascianekData.Menu.DeluxeMeals.Any()) return false;

            return true;
        }
    }
}