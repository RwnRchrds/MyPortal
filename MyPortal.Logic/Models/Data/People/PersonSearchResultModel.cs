using MyPortal.Database.Models.QueryResults.Person;

namespace MyPortal.Logic.Models.Data.People
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