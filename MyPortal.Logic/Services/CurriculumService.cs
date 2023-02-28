using System;
using System.Linq;
using System.Security.Claims;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;

using MyPortal.Logic.Models.Requests.Curriculum;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class CurriculumService : BaseUserService, ICurriculumService
    {
        public CurriculumService(ICurrentUser user) : base(user)
        {
        }
    }
}