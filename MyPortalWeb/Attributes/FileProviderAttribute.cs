using System;
using MyPortal.Logic.Enums;

namespace MyPortalWeb.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class FileProviderAttribute : Attribute
{
    public FileProvider[] FileProviders;

    public FileProviderAttribute(params FileProvider[] fileProviders)
    {
        FileProviders = fileProviders;
    }
}