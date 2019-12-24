﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.BusinessLogic.Dtos
{
    public class SenProvisionDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ProvisionTypeId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        public string Note { get; set; }

        public virtual StudentDto Student { get; set; }

        public virtual SenProvisionTypeDto Type { get; set; }
    }
}