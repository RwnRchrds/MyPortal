namespace MyPortal.Database.Interfaces
{
    /// <summary>
    /// Defines an entity that is able to be created by MyPortal.
    /// </summary>
    public interface ISystemEntity : IEntity
    {
        /// <summary>
        /// The entity is managed by MyPortal and should not be modified by users.
        /// </summary>
        public bool System { get; set; }
    }
}