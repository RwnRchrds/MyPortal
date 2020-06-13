using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Models.Requests.Reporting;

namespace MyPortal.Logic.Extensions
{
    public static class MathExtensions
    {
        public static bool IsInt(this double value)
        {
            return Math.Abs(value % 1) <= (double.Epsilon * 100);
        }

        public static bool IsInt(this decimal value)
        {
            return Math.Abs(value % 1) == 0;
        }
    }
}
