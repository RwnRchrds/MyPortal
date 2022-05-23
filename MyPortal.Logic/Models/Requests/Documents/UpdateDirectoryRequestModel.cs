using System;

namespace MyPortal.Logic.Models.Requests.Documents
{
    public class UpdateDirectoryRequestModel : CreateDirectoryRequestModel
    {
        public Guid Id { get; set; }
    }
}
