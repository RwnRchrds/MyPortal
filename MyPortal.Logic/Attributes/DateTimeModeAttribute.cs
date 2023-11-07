using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Enums;

namespace MyPortal.Logic.Attributes
{
    internal class DateTimeModeAttribute : ValidationAttribute
    {
        private readonly DateTimeMode _dateTimeMode;

        public DateTimeModeAttribute(DateTimeMode dateTimeMode)
        {
            _dateTimeMode = dateTimeMode;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;
            var today = DateTime.Today;

            if (_dateTimeMode == DateTimeMode.Future)
            {
                if (date != null && date <= today)
                {
                    return new ValidationResult("Date must be in the future.");
                }
            }

            if (_dateTimeMode == DateTimeMode.Past)
            {
                if (date != null && date >= today)
                {
                    return new ValidationResult("Date must be in the past.");
                }
            }

            if (_dateTimeMode == DateTimeMode.PastOrPresent)
            {
                if (date != null && date > today)
                {
                    return new ValidationResult("Date cannot be in the future.");
                }
            }

            if (_dateTimeMode == DateTimeMode.FutureOrPresent)
            {
                if (date != null && date < today)
                {
                    return new ValidationResult("Date cannot be in the past.");
                }
            }

            return ValidationResult.Success;
        }
    }
}