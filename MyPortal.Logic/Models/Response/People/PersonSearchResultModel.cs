using MyPortal.Database.Models.Query.Person;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Response.People
{
    public class PersonSearchResultModel
    {
        public PersonSearchResultModel(PersonSearchResult result)
        {
            Person = new PersonModel(result.Person);
            PersonTypes = result.PersonTypes;
        }
        
        public PersonModel Person { get; set; }
        public PersonTypeIndicator PersonTypes { get; set; }
    }
}
