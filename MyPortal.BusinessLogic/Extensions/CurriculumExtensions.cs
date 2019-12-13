using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Extensions
{
    public static class CurriculumExtensions
    {
        public static string GetSubjectName(this Class @class)
        {
            if (@class.SubjectId == null || @class.SubjectId == 0)
            {
                return "No Subject";
            }

            return @class.Subject.Name;
        }
    }
}