using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;

namespace MyPortal.Dtos.GridDtos
{
    public class GridMedicalPersonDietaryRequirementDto : IGridDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}