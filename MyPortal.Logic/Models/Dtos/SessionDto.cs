using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models;

namespace MyPortal.Logic.Models.Dtos
{
    public class SessionDto
    {
        public Guid Id { get; set; }

        public Guid ClassId { get; set; }

        public Guid PeriodId { get; set; }

        public virtual PeriodDto Period { get; set; }

        public virtual ClassDto Class { get; set; }
    }
}
