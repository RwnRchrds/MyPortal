using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;

namespace MyPortal.Extensions
{
    public static class CurriculumExtensions
    {
        public static string GetSubjectName(this CurriculumClass @class)
        {
            if (@class.SubjectId == null || @class.SubjectId == 0)
            {
                return "No Subject";
            }

            return @class.Subject.Name;
        }
    }
}