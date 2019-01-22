using System.Collections.Generic;

namespace MyPortal.Models.Misc
{
    public class Gender
    {
        public static IEnumerable<Gender> GetGenderOptions()
        {
            return new List<Gender> {new Gender{Name = "Male", Value = "M"}, new Gender{Name = "Female", Value = "F"}, new Gender {Name = "Other", Value = "X"}};
        }
        
        public string Value { get; set; }
        public string Name { get; set; }
    }
}