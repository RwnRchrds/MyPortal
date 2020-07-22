using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Attributes
{
    public class NotNegativeAttribute : RangeAttribute
    {
        public NotNegativeAttribute() : base(0, int.MaxValue)
        {

        }
    }
}
