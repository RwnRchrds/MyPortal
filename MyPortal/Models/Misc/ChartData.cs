using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Misc
{
    public class ChartData
    {
        public string X { get; set; }
        public double Y { get; set; }

        public ChartData(string x, double y)
        {
            X = x;
            Y = y;
        }
    }
}