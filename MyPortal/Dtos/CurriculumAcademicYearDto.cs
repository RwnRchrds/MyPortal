using System;
namespace MyPortal.Dtos
{
    public class CurriculumAcademicYearDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime FirstDate { get; set; }

        public DateTime LastDate { get; set; }
    }
}