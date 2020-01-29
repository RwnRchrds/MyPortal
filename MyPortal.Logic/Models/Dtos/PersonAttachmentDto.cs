namespace MyPortal.Logic.Models.Dtos
{
    public class PersonAttachmentDto
    {
        public int Id { get; set; }

        public int PersonId { get; set; }

        public int DocumentId { get; set; }

        public virtual DocumentDto Document { get; set; }

        public virtual PersonDto Person { get; set; }
    }
}
