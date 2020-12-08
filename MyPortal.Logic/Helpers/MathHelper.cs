using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPortal.Logic.Helpers
{
    public class MathHelper
    {
        public static double Percent(double amount, double total, int decimalPlaces)
        {
            return Math.Round((amount / total) * 100, decimalPlaces);
        }

        public static double Sum(params double[] values)
        {
            return values.Sum();
        }
    }
}
