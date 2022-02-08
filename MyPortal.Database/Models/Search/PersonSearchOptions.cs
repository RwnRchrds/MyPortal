using System;

namespace MyPortal.Database.Models.Search
{
    public class PersonSearchOptions
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime? Dob { get; set; }
        public bool? IsDeceased { get; set; }
        public Guid? EthnicityId { get; set; }
        public string NhsNumber { get; set; }

        internal void ApplySearch(SqlKata.Query query, string tblAlias)
        {
            if (!string.IsNullOrWhiteSpace(FirstName))
            {
                query.WhereStarts($"{tblAlias}.FirstName", FirstName);
            }

            if (!string.IsNullOrWhiteSpace(LastName))
            {
                query.WhereStarts($"{tblAlias}.LastName", LastName);
            }

            if (!string.IsNullOrWhiteSpace(Gender))
            {
                query.Where($"{tblAlias}.Gender", Gender);
            }

            if (Dob != null)
            {
                query.WhereDate($"{tblAlias}.Dob", Dob);
            }

            if (IsDeceased.HasValue)
            {
                if (IsDeceased.Value)
                {
                    query.WhereNotNull($"{tblAlias}.Deceased");
                }
                else
                {
                    query.WhereNull($"{tblAlias}.Deceased");
                }
            }
        }
    }
}
