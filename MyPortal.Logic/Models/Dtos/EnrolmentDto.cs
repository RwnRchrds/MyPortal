using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class EnrolmentDto
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Guid ClassId { get; set; }

        public virtual ClassDto Class { get; set; }

        public virtual StudentDto Student { get; set; }
    }
}
