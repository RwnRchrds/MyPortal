namespace MyPortal.Logic.Models.Dtos
{
    public class SessionDto
    {
        public int Id { get; set; }

        public int ClassId { get; set; }

        public int PeriodId { get; set; }

        public virtual PeriodDto Period { get; set; }

        public virtual ClassDto Class { get; set; }
    }
}
