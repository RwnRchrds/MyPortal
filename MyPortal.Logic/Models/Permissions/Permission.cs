using MyPortal.Database.Enums;

namespace MyPortal.Logic.Models.Permissions
{
    internal class Permission
    {
        public string ShortName { get; set; }
        public string FullName { get; set; }
        public int Value { get; set; }

        public Permission(string shortName, string fullName, int value)
        {
            ShortName = shortName;
            FullName = fullName;
            Value = value;
        }

        public Permission(string shortName, string fullName, PermissionValue value)
        {
            ShortName = shortName;
            FullName = fullName;
            Value = (int)value;
        }
    }
}