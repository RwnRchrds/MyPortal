using System;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Identity;

public class CustomCurrentUser : ICurrentUser
{
    private readonly Guid _userId;

    public CustomCurrentUser(Guid userId)
    {
        _userId = userId;
    }

    public Guid GetUserId()
    {
        return _userId;
    }
}