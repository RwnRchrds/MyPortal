using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MyPortal.Dtos.LiteDtos
{
    [DataContract(Namespace = "")]
    public class AttendanceRegisterMarkLite
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int StudentId { get; set; }

        [DataMember]
        public int WeekId { get; set; }

        [DataMember]
        public string Mark { get; set; }
        
        [DataMember] public string MeaningCode { get; set; }
    }
}