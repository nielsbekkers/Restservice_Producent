using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleRestSvc
{
    
        public class JSONData2Result
        {
            public double benodigd { get; set; }
            public int prijs { get; set; }
        }

        public class RootObject
        {
            public JSONData2Result JSONData2Result { get; set; }
        }
    
}
