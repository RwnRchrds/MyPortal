using System;
using MyPortal.Models.Database;

namespace MyPortal.Areas.Staff.ViewModels
{
    public class TakeRegisterViewModel
    {
        public int WeekId { get; set; }
        public DateTime SessionDate { get; set; }
        public CurriculumSession Session { get; set; }
    }
}