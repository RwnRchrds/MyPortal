using System;
using MyPortal.Logic.Enums;

namespace MyPortal.Logic.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class FileProviderAttribute : Attribute
{
    public FileProvider[] FileProviders;

    public FileProviderAttribute(params FileProvider[] fileProviders)
    {
        FileProviders = fileProviders;
    }
}