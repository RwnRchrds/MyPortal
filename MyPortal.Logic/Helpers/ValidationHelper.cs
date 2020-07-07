using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using MyPortal.Logic.Models.Exceptions;

namespace MyPortal.Logic.Helpers
{
    public class ValidationHelper
    {
        public static bool ValidateUpn(string upn)
        {
            var alpha = new[]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'T', 'U', 'V', 'W', 'X',
                'Y', 'Z'
            };

            var chars = upn.ToCharArray();

            if (chars.Length != 13)
            {
                return false;
            }

            var check = 0;

            for (var i = 1; i < chars.Length; i++)
            {
                if (int.TryParse(chars[i].ToString(), out var x))
                {
                    var n = x * (i + 1);
                    check += n;
                }
            }

            var alphaIndex = check % 23;

            return chars[0] == alpha[alphaIndex];
        }

        public static bool ValidateNhsNumber(string nhsNumber)
        {
            nhsNumber = nhsNumber.Replace(" ", "");

            if (nhsNumber.Length != 10)
            {
                return false;
            }
            
            if (int.TryParse(nhsNumber[9].ToString(), out var checkDigit))
            {
                var result = 0;
                for (int i = 0; i < 9; i++)
                {
                    if (int.TryParse(nhsNumber[i].ToString(), out var digitValue))
                    {
                        result += digitValue * (10 - i);
                    }
                    else
                    {
                        return false;
                    }
                }
                
                var validationResult = 11 - result % 11;

                if (validationResult == 10) return false;

                return validationResult == 11 ? checkDigit == 0 : checkDigit == validationResult;
            }

            return false;
        }

        public static void ValidateModel<T>(T model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(model, validationContext, results, true))
            {
                var errors = results.Select(x => x.ErrorMessage).ToList();

                throw new ServiceException(ExceptionType.BadRequest, errors.FirstOrDefault());
            }
        }
    }
}
