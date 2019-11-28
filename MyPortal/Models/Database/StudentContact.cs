using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Models.Database
{
    [Table("People_StudentContacts")]
    public class StudentContact
    {
        public int Id { get; set; }
        public int ContactTypeId { get; set; }
        public int StudentId { get; set; }
        public int ContactId { get; set; }

        public bool Correspondence { get; set; }
        public bool ParentalResponsibility { get; set; }
        public bool PupilReport { get; set; }
        public bool CourtOrder { get; set; }
    }
}