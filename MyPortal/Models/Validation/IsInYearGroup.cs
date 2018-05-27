using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http.Results;

namespace MyPortal.Models.Validation
{
    public class IsInYearGroup : ValidationAttribute
    {
        private MyPortalDbContext _context;

        public IsInYearGroup()
        {
            _context = new MyPortalDbContext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var student = (Student) validationContext.ObjectInstance;

            var regGroup = _context.RegGroups.SingleOrDefault(x => x.Id == student.RegGroup);

            var yearGroup = _context.YearGroups.SingleOrDefault(x => x.Id == student.YearGroup);

            if (regGroup == null || yearGroup == null)
            {
                return new ValidationResult("Year group or reg group do not exist");
            }

            return (regGroup.YearGroup == yearGroup.Id)
                ? ValidationResult.Success
                : new ValidationResult("Reg group is not in selected year group");
        }
    }
}