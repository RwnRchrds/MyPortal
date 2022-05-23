using MyPortal.Database.Models.QueryResults.Person;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Response.People
{
    public class PersonSearchResultResponseModel
    {
        public PersonSearchResultResponseModel(PersonSearchResult result)
        {
            Person = new PersonModel(result.Person);
            PersonTypes = result.PersonTypes;
        }
        
        public PersonModel Person { get; set; }
        public PersonTypeIndicator PersonTypes { get; set; }
    }
}
