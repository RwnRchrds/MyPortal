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
        [NotEmpty]
        public Guid ResultSetId { get; set; }

        [NotEmpty]
        public Guid StudentId { get; set; }

        [NotEmpty]
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
