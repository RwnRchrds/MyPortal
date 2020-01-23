﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Models;

namespace MyPortal.Database.Interfaces
{
    public interface IPersonRepository : IReadWriteRepository<Person, int>
    {
        Task<Person> GetByUserId(string userId);

        Task<IEnumerable<Person>> Search(Person person);

        Task<int> GetNumberOfBirthdaysThisWeek(DateTime weekBeginning);
    }
}
