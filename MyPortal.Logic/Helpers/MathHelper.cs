using System;
using System.Linq;

namespace MyPortal.Logic.Helpers
{
    internal class MathHelper
    {
        internal static double Percent(double amount, double total, int decimalPlaces)
        {
            return Math.Round((amount / total) * 100, decimalPlaces);
        }

        internal static double Sum(params double[] values)
        {
            return values.Sum();
        }
    }
}
