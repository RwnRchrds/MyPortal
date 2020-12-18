using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Requests.Admin.Users
{
    public class LinkPersonModel
    {
        public Guid UserId { get; set; }
        public Guid PersonId { get; set; }
    }
}
