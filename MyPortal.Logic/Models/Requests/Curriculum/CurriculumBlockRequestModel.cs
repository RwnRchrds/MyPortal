using System;
using MyPortal.Logic.Models.Data.Curriculum;


namespace MyPortal.Logic.Models.Requests.Curriculum
{
    public class CurriculumBlockRequestModel
    {
        public CurriculumBlockModel BlockModel { get; set; }
        public Guid[] BandIds { get; set; }
    }
}