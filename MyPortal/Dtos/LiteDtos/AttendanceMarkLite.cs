using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MyPortal.Dtos.LiteDtos
{    
    public class AttendanceMarkLite
    {        
        public int Id { get; set; }
        
        public int StudentId { get; set; }
        
        public int WeekId { get; set; }

        public int PeriodId { get; set; }
        
        public string Mark { get; set; }
        
        public string MeaningCode { get; set; }
    }
}