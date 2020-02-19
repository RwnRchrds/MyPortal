namespace MyPortal.Logic.Models.Dtos.DataGrid
{
    public class DataGridProductDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string TypeDescription { get; set; }
        public decimal Price { get; set; }
        public bool Visible { get; set; }
        public bool OnceOnly { get; set; }
    }
}