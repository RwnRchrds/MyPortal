using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MyPortal.Models.Validation
{
    public class ActiveGradeSet : ValidationAttribute
    {
        private readonly MyPortalDbContext _context;

        public ActiveGradeSet()
        {
            _context = new MyPortalDbContext();
        }       
        
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var result = (Result) validationContext.ObjectInstance;

            var validGrades = _context.Grades.Where(x => x.GradeSet.Active).ToList();

            return validGrades.Any(x => x.GradeValue == result.Value) 
                ? ValidationResult.Success 
                : new ValidationResult("Result grade is not valid");
        }
    }
}