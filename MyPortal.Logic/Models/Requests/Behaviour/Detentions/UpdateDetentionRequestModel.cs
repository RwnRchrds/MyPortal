using System;

namespace MyPortal.Logic.Models.Requests.Behaviour.Detentions
{
    public class UpdateDetentionRequestModel : CreateDetentionRequestModel
    {
        public Guid Id { get; set; }
    }
}
