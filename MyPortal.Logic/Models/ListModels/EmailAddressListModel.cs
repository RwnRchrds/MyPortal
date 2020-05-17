namespace MyPortal.Logic.Models.ListModels
{
    public class EmailAddressListModel
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Address { get; set; }
        public bool Main { get; set; }
    }
}