namespace MyPortal.Database.Models.QueryResults.Person
{
    public class PersonSearchResult
    {
        public Entity.Person Person { get; set; }
        public PersonTypeIndicator PersonTypes { get; set; }
    }
}