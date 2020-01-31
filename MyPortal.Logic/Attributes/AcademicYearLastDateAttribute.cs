using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Dtos;

namespace MyPortal.Logic.Attributes
{
    public class AcademicYearLastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var academicYear = (AcademicYearDto) validationContext.ObjectInstance;

            if ((DateTime) value > academicYear.FirstDate)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("Last date must be greater than first date.");
        }
    }
}
