using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.ViewModels
{
    public class TakeRegisterViewModel
    {
        public int WeekId { get; set; }
        public DateTime SessionDate { get; set; }
        public CurriculumSession Session { get; set; }
    }
}