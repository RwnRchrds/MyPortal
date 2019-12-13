using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Extensions
{
    public static class StudentExtensions
    {
        public static string GetDisplayName(this Student student)
        {
            return $"{student.Person.LastName}, {student.Person.FirstName}";
        }

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
    }
}