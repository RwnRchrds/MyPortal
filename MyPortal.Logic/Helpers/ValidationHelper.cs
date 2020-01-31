using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
