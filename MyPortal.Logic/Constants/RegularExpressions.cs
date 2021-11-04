namespace MyPortal.Logic.Constants
{
    public static class RegularExpressions
    {
        public const string ColourCode = @"^#([a-fA-F0-9]{6}|[a-fA-F0-9]{3})$";
        public const string EmailAddress = @"/^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6})*$/";

        public const string PostCode =
            @"([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})";
    }
}