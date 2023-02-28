using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Models.Data.Settings;

namespace MyPortal.Logic.Interfaces;

public interface ICurrentUser
{
    Guid GetUserId();
}