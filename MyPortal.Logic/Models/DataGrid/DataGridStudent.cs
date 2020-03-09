using System;

namespace MyPortal.Logic.Models.DataGrid
{
    public class DataGridStudent
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string RegGroupName { get; set; }
        public string YearGroupName { get; set; }
        public string HouseName { get; set; }
        public string Gender { get; set; }
        public string HouseColourCode { get; set; }
    }
}