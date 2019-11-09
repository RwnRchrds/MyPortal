using MyPortal.Interfaces;

namespace MyPortal.Dtos.DataGrid
{
    public class GridApplicationUserDto : IGridDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }
}