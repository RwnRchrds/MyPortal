using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Extensions
{
    public static class StudentExtensions
    {
        public static string GetDisplayName(this Student student)
        {
            return $"{student.Person.LastName}, {student.Person.FirstName}";
        }

        
    }
}