using System;

namespace GimmeDatAPI.PlainOldClrObjects
{
    public class ZascianekData
    {
        public ZascianekData()
        {
            CurrentDateTime = DateTime.Now;
        }

        public DateTime CurrentDateTime { get; }

        public DateTime MenuDate { get; set; }

        public Menu Menu { get; set; }
    }
}