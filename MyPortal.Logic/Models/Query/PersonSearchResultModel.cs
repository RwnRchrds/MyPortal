﻿using MyPortal.Database.Models.Query.Person;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.Query
{
    public class PersonSearchResultModel
    {
        public PersonModel Person { get; set; }
        public PersonTypeIndicator PersonTypes { get; set; }
    }
}
