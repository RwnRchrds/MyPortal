using System;

namespace MyPortal.Logic.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class EagerLoadAttribute : Attribute
{
    public bool Deep { get; set; }
}