using System.Collections.Generic;

namespace MyPortal.Logic.Constants
{
    public static class Gender
    {
        public const string Male = "M";
        public const string Female = "F";
        public const string Other = "X";
        public const string Unknown = "U";

        private static readonly Dictionary<string, string> GenderLabels = new Dictionary<string, string>
        {
            { Male, "Male" },
            { Female, "Female" },
            { Other, "Other" },
            { Unknown, "Unknown" }
        };

        internal static string GetGenderLabel(string gender)
        {
            var result = GenderLabels.TryGetValue(gender, out string genderLabel);

            return genderLabel;
        }
    }
}