using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class UpdateDirectoryModel : CreateDirectoryModel
    {
        public Guid Id { get; set; }
    }
}
