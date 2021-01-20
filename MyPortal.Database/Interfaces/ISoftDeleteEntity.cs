namespace MyPortal.Database.Interfaces
{
    public interface ISoftDeleteEntity : IEntity
    {
        bool Deleted { get; set; }
    }
}
