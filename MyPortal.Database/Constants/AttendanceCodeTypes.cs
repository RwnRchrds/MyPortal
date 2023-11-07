using System;

namespace MyPortal.Database.Constants
{
    public static class AttendanceCodeTypes
    {
        public static Guid Present { get; } = Guid.Parse("59036717-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid ApprovedEdActivity { get; } = Guid.Parse("59036717-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid AuthorisedAbsence { get; } = Guid.Parse("59036718-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid UnauthorisedAbsence { get; } = Guid.Parse("59036720-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid AttendanceNotRequired { get; } = Guid.Parse("59036721-D349-46D3-B8A6-60FFA9263DB3");
    }
}