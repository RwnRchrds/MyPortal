using System;
using System.Linq;
using System.Security.Claims;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Curriculum;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class CurriculumService : BaseService, ICurriculumService
    {
        public CurriculumService(ClaimsPrincipal user) : base(user)
        {
            
        }
    }
}