using MyPortal.Interfaces;

namespace MyPortal.Dtos.Identity
{
    public class ApplicationRoleDto : IGridDto
    {
        public string Id { get; set; }
        public bool System { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}