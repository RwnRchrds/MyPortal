using System;
using MyPortal.Database.Constants;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Identity;

public class SessionUser : BaseSessionUser, ISessionUser
{
    private readonly Guid _userId;

    public static ISessionUser System => new SessionUser(Users.System);
    
    public static ISessionUser Anonymous => new SessionUser(Users.Anonymous);

    public SessionUser(Guid userId)
    {
        _userId = userId;
    }

    public override bool IsType(int userType)
    {
        throw new NotImplementedException();
    }

    public override Guid? GetUserId()
    {
        return _userId;
    }
}