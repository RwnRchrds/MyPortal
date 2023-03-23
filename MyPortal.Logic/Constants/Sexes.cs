using System.Collections.Generic;

namespace MyPortal.Logic.Constants
{
    public static class Sexes
    {
        public const string Male = "M";
        public const string Female = "F";
        public const string Other = "X";
        public const string Unknown = "U";

        private static readonly Dictionary<string, string> SexLabels = new Dictionary<string, string>
        {
            { Male, "Male" },
            { Female, "Female" },
            { Other, "Other" },
            { Unknown, "Unknown" }
        };

        internal static string GetGenderLabel(string gender)
        {
            var result = SexLabels.TryGetValue(gender, out string genderLabel);

            return genderLabel;
        }
    }
}