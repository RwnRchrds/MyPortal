using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyPortal.Logic.Models.Business
{
    public class ContactModel
    {
        public Guid Id { get; set; }

        public Guid PersonId { get; set; }

        public bool ParentalBallot { get; set; }

        [StringLength(256)]
        public string PlaceOfWork { get; set; }

        [StringLength(256)]
        public string JobTitle { get; set; }

        [StringLength(128)]
        public string NiNumber { get; set; }

        public virtual PersonModel Person { get; set; }
    }
}
