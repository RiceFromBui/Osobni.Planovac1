using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osobni.Planovac1
{
    public class EventModel //třída reprezentující jednu událost
    {
        public string Text { get; set; } = "";
        public string Category { get; set; } = "Obecné"; 
    }
}
