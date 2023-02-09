using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Requests.Assessment
{
    public class ResultRequestModel
    {
        [NotDefault]
        public Guid ResultSetId { get; set; }

        [NotDefault]
        public Guid StudentId { get; set; }

        [NotDefault]
        public Guid AspectId { get; set; }
        
        public Guid CreatedById { get; set; }

        public DateTime Date { get; set; }

        public Guid? GradeId { get; set; }

        public decimal? Mark { get; set; }

        public string Comment { get; set; }

        public string ColourCode { get; set; }

        public string Note { get; set; }
    }
}
