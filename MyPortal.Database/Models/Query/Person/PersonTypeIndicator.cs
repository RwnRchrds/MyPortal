namespace MyPortal.Database.Models.Query.Person
{
    public class PersonTypeIndicator
    {
        public bool IsUser { get; set; }
        public bool IsStudent { get; set; }
        public bool IsContact { get; set; }
        public bool IsStaff { get; set; }
        public bool IsApplicant { get; set; }
        public bool IsAgent { get; set; }
    }
}
