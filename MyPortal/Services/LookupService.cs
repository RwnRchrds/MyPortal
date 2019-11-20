using System;
using System.Collections.Generic;
using MyPortal.Models.Misc;

namespace MyPortal.Services
{
    public class LookupService : IDisposable
    {
        public IEnumerable<string> GetTitles()
        {
            var titles = new List<string> {"Mr", "Miss", "Mrs", "Ms", "Mx", "Prof", "Sir", "Dr", "Lady", "Lord"};

            return titles;
        }

        public void Dispose()
        {
            
        }
    }
}