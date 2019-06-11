using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos
{
    public class SenReviewTypeDto
    {
        public int Id { get; set; }

        public string Description { get; set; }
    }
}