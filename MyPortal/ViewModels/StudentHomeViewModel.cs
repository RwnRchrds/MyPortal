﻿using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Misc;

namespace MyPortal.ViewModels
{
    public class StudentHomeViewModel
    {
        public Student Student { get; set; }
        public List<Log> Logs { get; set; }
        public IEnumerable<Result> Results { get; set; }
        public bool IsUpperSchool { get; set; }
        public ChartData ChartData { get; set; }
    }
}