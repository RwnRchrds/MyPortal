using System;

namespace MyPortal.Logic.Models.List
{
    public class PersonListModel
    {
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
    }
}