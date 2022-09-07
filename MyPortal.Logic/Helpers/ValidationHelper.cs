using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using MyPortal.Logic.Exceptions;

namespace MyPortal.Logic.Helpers
{
    internal class ValidationHelper
    {
        public static bool ValidateUpn(string upn)
        {
            upn = upn.Replace(" ", "");
            
            if (upn.Length != 13)
            {
                return false;
            }

            var chars = upn.ToCharArray();

            var checkDigit = GetUpnCheckDigit(chars[new Range(1, 13)]);

            return chars[0] == checkDigit;
        }

        public static char GetUpnCheckDigit(string baseUpn)
        {
            return GetUpnCheckDigit(baseUpn.ToCharArray());
        }

        public static char GetUpnCheckDigit(char[] baseUpn)
        {
            if (baseUpn.Length != 12)
            {
                throw new ArgumentException("Please enter the base UPN only", nameof(baseUpn));
            }
            
            var alpha = new[]
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'T', 'U', 'V', 'W', 'X',
                'Y', 'Z'
            };
            
            var check = 0;

            for (var i = 1; i < baseUpn.Length + 1; i++)
            {
                if (int.TryParse(baseUpn[i-1].ToString(), out var x))
                {
                    var n = x * (i + 1);
                    check += n;
                }
            }

            var alphaIndex = check % 23;

            return alpha[alphaIndex];
        }

        public static bool ValidateNhsNumber(string nhsNumber)
        {
            nhsNumber = nhsNumber.Replace(" ", "");

            if (nhsNumber.Length != 10)
            {
                return false;
            }

            if (!int.TryParse(nhsNumber[9].ToString(), out var checkDigit))
            {
                return false;
            }

            var result = 0;

            for (var i = 0; i < 9; i++)
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

        public static void ValidateModel<T>(T model)
        {
            var validationContext = new ValidationContext(model, null, null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(model, validationContext, results, true))
            {
                var errors = results.Select(x => x.ErrorMessage).ToArray();
                var message = string.Join(Environment.NewLine, errors);
                throw new InvalidDataException(message);
            }
        }
    }
}
