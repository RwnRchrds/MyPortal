using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using MyPortal.Models.Database;

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
            var result = (AssessmentResult) validationContext.ObjectInstance;

            var validGrades = _context.AssessmentGrades.Where(x => x.AssessmentGradeSet.Active).ToList();

            return validGrades.Any(x => x.Grade == result.Value) 
                ? ValidationResult.Success 
                : new ValidationResult("Result grade is not valid");
        }
    }
}