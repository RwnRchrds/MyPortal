using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Extensions
{
    internal static class MathExtensions
    {
        internal static bool IsInt(this double value)
        {
            return Math.Abs(value % 1) <= (double.Epsilon * 100);
        }

        internal static bool IsInt(this decimal value)
        {
            return Math.Abs(value % 1) == 0;
        }
    }
}
