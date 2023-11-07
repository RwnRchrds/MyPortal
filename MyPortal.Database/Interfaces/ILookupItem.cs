namespace MyPortal.Database.Interfaces
{
    public interface ILookupItem : IEntity
    {
        public string Description { get; set; }
        public bool Active { get; set; }
    }
}