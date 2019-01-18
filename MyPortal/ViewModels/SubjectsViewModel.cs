using System.Collections.Generic;
using MyPortal.Dtos;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class SubjectsViewModel
    {
        public Subject Subject { get; set; }
        public IEnumerable<Staff> Staff { get; set; }
    }
}