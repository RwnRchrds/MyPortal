using System;
using MyPortal.Database.Enums;

namespace MyPortal.Database.Models.Search
{
    public class StudentSearchOptions : PersonSearchOptions
    {
        public StudentSearchOptions()
        {
            Status = StudentStatus.OnRoll;
        }

        public StudentStatus Status { get; set; }
        public Guid? CurriculumGroupId { get; set; }
        public Guid? RegGroupId { get; set; }
        public Guid? YearGroupId { get; set; }
        public Guid? HouseId { get; set; }
        public Guid? SenStatusId { get; set; }

        internal void ApplySearch(SqlKata.Query query, string studentAlias,
            string personAlias, string studentHouseCteAlias, string studentRegGroupCteAlias,
            string studentYearGroupCteAlias)
        {
            base.ApplySearch(query, personAlias);

            switch (Status)
            {
                case StudentStatus.OnRoll:
                    query.Where(q =>
                        q.WhereNull($"{studentAlias}.DateLeaving")
                            .OrWhereDate($"{studentAlias}.DateLeaving", ">", DateTime.Today));
                    break;
                case StudentStatus.Leavers:
                    query.WhereDate($"{studentAlias}.DateLeaving", "<=", DateTime.Today);
                    break;
                case StudentStatus.Future:
                    query.WhereDate($"{studentAlias}.DateStarting", ">", DateTime.Today);
                    break;
                default:
                    break;
            }

            if (SenStatusId != null)
            {
                query.Where($"{studentAlias}.SenStatusId", SenStatusId.Value);
            }

            if (HouseId.HasValue)
            {
                query.Where($"{studentHouseCteAlias}.HouseId", HouseId.Value);
            }

            if (RegGroupId.HasValue)
            {
                query.Where($"{studentRegGroupCteAlias}.RegGroupId", RegGroupId.Value);
            }

            if (YearGroupId.HasValue)
            {
                query.Where($"{studentYearGroupCteAlias}.YearGroupId", YearGroupId.Value);
            }
        }
    }
}
