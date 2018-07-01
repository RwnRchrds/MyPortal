using System.Collections.Generic;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class RandomStudentViewModel
    {
        public Student Student { get; set; }
        public List<Result> Results { get; set; }
    }
}