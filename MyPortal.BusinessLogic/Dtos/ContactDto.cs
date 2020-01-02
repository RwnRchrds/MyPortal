using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Dtos
{
    public class ContactDto : IPersonDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public bool ParentalBallot { get; set; }

        public string PlaceOfWork { get; set; }

        public string JobTitle { get; set; }

        [StringLength(128)]
        public string NiNumber { get; set; }

        public virtual PersonDto Person { get; set; }
    }
}
