using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class AttendanceCodeMeaningRepository : BaseReadRepository<AttendanceCodeMeaning>, IAttendanceCodeMeaningRepository
    {
        public AttendanceCodeMeaningRepository(IDbConnection connection) : base(connection)
        {
        }
    }
}