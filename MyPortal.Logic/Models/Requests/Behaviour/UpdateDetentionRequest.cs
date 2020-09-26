using System;

namespace MyPortal.Logic.Models.Requests.Behaviour
{
    public class UpdateDetentionRequest : CreateDetentionRequest
    {
        public Guid Id { get; set; }
    }
}
