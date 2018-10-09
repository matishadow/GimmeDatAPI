using System.Collections.Generic;

namespace GimmeDatAPI.PlainOldClrObjects
{
    public class Menu
    {
        public string Soup { get; set; }
        public string DeluxeMeal { get; set; }
        
        public IEnumerable<string> MealsOfTheDay { get; set; }
    }
}