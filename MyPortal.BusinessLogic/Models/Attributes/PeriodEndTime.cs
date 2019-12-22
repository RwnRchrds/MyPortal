using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Models.Attributes
{
    public class PeriodEndTime : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var period = (Period) validationContext.ObjectInstance;

            return period.EndTime > period.StartTime
                ? ValidationResult.Success
                : new ValidationResult("End time must be greater than start time.");
        }
    }
}
