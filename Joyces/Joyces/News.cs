using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joyces
{

    public class News
    {
        public string code { get; set; }
        public string dutyText { get; set; }
        public DateTime startDate { get; set; }
        public string imageUrl { get; set; }
        public string newsUrl { get; set; }
        public string internalDescription { get; set; }
        public string name { get; set; }
        public string note { get; set; }
        public string extendedNote { get; set; }
        public string type { get; set; }
        public DateTime endDate { get; set; }
    }
}
