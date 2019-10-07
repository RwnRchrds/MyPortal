﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MyPortal.Models.Database
{
    [Table("People_ContactTypes")]
    public class ContactType
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }
    }
}