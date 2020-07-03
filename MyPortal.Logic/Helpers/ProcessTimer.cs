using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Helpers
{
    public class ProcessTimer : IDisposable
    {
        private string _name;
        private DateTime _start;

        public ProcessTimer(string name)
        {
            _name = name;
            _start = DateTime.Now;
            Console.WriteLine($"Process '{_name}' started.");
        }

        public void Dispose()
        {
            var end = DateTime.Now;
            Console.WriteLine($"Process '{_name}' ended. Time taken: {(end - _start).TotalMilliseconds}ms");
        }
    }
}
