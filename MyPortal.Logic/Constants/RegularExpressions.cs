namespace MyPortal.Logic.Constants
{
    public static class RegularExpressions
    {
        public const string ColourCode = @"^#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$";
        public const string EmailAddress = @"/^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6})*$/";
    }
}