using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joyces.Platform
{
    public class AppContext
    {
        public static AppContext Instance { get; set; }
        public IPlatform Platform { get; set; }
    }
}
