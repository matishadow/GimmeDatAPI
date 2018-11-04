using System;
using System.Collections.Generic;
using System.Linq;
using GimmeDatAPI.Configuration.InversionOfControl.RegistrationRelated;
using GimmeDatAPI.Configuration.InversionOfControl.ScopeRelated;

namespace GimmeDatAPI.PlainOldClrObjects.Templates
{
    public class ZascianekXPathTemplates : ISingleInstanceDependency, IAsSelfRegistrationDependency
    {
        public readonly IEnumerable<ZascianekXPathTemplate> Templates;

        public ZascianekXPathTemplates()
        {
            var standardZascianekTemplate = new ZascianekXPathTemplate();
            var possibleTemplate = new ZascianekXPathTemplate();
            
            possibleTemplate.Soups.XPathString = "/html/body/main/div/div/div[3]/div/div[1]/div[3]/div[1]";
            possibleTemplate.MealsOfTheDay.XPathString = "/html/body/main/div/div/div[3]/div/div[1]/div[3]/div[2]";
            possibleTemplate.DeluxeMeals.XPathString = "/html/body/main/div/div/div[3]/div/div[1]/div[3]/div[3]";

            Templates = new List<ZascianekXPathTemplate> {standardZascianekTemplate, possibleTemplate};
        }
    }
}