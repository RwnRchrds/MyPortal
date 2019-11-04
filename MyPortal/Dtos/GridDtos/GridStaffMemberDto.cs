using MyPortal.Interfaces;

namespace MyPortal.Dtos.GridDtos
{
    public class GridStaffMemberDto : IGridDto
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Code { get; set; }
    }
}