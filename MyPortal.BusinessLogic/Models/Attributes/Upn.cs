using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Services;

namespace MyPortal.BusinessLogic.Models.Attributes
{
    public class Upn : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidationService.ValidateUpn((string) value)
                ? ValidationResult.Success
                : new ValidationResult("Upn is not valid.");
        }
    }
}
