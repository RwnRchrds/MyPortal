namespace MyPortal.Database.Enums;

internal enum NameFormat
{
    /// <summary>
    /// E.g. Bloggs, Joe Thomas
    /// </summary>
    Default,
    /// <summary>
    /// E.g. Mr Joe Thomas Bloggs
    /// </summary>
    FullName,
    /// <summary>
    /// E.g. Mr J T Bloggs
    /// </summary>
    FullNameAbbreviated,
    /// <summary>
    /// E.g. Joe Thomas Bloggs
    /// </summary>
    FullNameNoTitle,
    /// <summary>
    /// E.g. JTB
    /// </summary>
    Initials
}