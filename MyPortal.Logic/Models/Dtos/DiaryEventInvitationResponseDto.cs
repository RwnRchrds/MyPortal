using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Dtos
{
    public class DiaryEventInvitationResponseDto
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }
    }
}
