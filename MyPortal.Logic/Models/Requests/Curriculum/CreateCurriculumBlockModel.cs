using System;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Requests.Curriculum
{
    public class CreateCurriculumBlockModel
    {
        public CurriculumBlockModel BlockModel { get; set; }
        public Guid[] BandIds { get; set; }
    }
}