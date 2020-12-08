using System;

namespace MyPortal.Logic.Models.Requests.Behaviour.Detentions
{
    public class UpdateDetentionRequest : CreateDetentionRequest
    {
        public Guid Id { get; set; }
    }
}
