using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;

namespace MyPortal.Extensions
{
    public static class StudentExtensions
    {
        public static string GetDisplayName(this Student student)
        {
            return $"{student.Person.LastName}, {student.Person.FirstName}";
        }
    }
}