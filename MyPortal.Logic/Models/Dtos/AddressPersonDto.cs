namespace MyPortal.Logic.Models.Dtos
{
    public class AddressPersonDto
    {
        public int Id { get; set; }

        public int AddressId { get; set; }

        public int PersonId { get; set; }

        public virtual AddressDto Address { get; set; }
        public virtual PersonDto Person { get; set; }
    }
}
