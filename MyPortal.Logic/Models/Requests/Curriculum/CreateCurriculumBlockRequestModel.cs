using System;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Requests.Curriculum
{
    public class CreateCurriculumBlockRequestModel
    {
        public CurriculumBlockModel BlockModel { get; set; }
        public Guid[] BandIds { get; set; }
    }
}