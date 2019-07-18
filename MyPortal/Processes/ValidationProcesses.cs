using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyPortal.Processes
{
    public class ValidationProcesses
    {
        public static List<string> ErrorMessages = new List<string>();

        public static bool ModelIsValid<T>(T model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            if (Validator.TryValidateObject(model, validationContext, results, true))
            {
                return true;
            }

            ErrorMessages = results.Select(x => x.ErrorMessage).ToList();
            return false;
        }
    }
}