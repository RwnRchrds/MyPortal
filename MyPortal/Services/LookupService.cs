using System.Collections.Generic;
using MyPortal.Models.Misc;

namespace MyPortal.Services
{
    public static class LookupService
    {
        public static ProcessResponse<IEnumerable<string>> GetTitles()
        {
            var titles = new List<string> {"Mr", "Miss", "Mrs", "Ms", "Mx", "Prof", "Sir", "Dr", "Lady", "Lord"};

            return new ProcessResponse<IEnumerable<string>>(ResponseType.Ok, null, titles);
        }
    }
}