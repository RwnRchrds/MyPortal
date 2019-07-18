using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class LookupProcesses
    {
        public static ProcessResponse<IEnumerable<string>> GetTitles()
        {
            var titles = new List<string> {"Mr", "Miss", "Mrs", "Ms", "Mx", "Prof", "Sir", "Dr", "Lady", "Lord"};

            return new ProcessResponse<IEnumerable<string>>(ResponseType.Ok, null, titles);
        }
    }
}