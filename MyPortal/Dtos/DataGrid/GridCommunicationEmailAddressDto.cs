using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Interception;
using System.Linq;
using System.Web;

namespace MyPortal.Dtos.DataGrid
{
    public class GridCommunicationEmailAddressDto
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public bool Main { get; set; }
    }
}