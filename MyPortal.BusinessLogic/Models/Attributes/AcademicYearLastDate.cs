using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Models.Attributes
{
    public class AcademicYearLastDate : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var academicYear = (AcademicYear) validationContext.ObjectInstance;

            return academicYear.LastDate > academicYear.FirstDate
                ? ValidationResult.Success
                : new ValidationResult("Last date must be greater than first date.");
        }
    }
}
