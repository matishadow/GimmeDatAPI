using System.Collections.Generic;

namespace GimmeDatAPI.PlainOldClrObjects
{
    public class Menu
    {
        public IEnumerable<string> Soups { get; set; }
        public IEnumerable<string> DeluxeMeals { get; set; }
        public IEnumerable<string> MealsOfTheDay { get; set; }
    }
}