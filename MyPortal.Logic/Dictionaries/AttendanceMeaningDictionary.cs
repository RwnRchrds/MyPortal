using System;

namespace MyPortal.Logic.Dictionaries
{
    public static class AttendanceMeaningDictionary
    {
        public static Guid Present = Guid.Parse("59036717-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid ApprovedEdActivity = Guid.Parse("59036717-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid AuthorisedAbsence = Guid.Parse("59036718-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid UnauthorisedAbsence = Guid.Parse("59036720-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid Late = Guid.Parse("59036723-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid AttendanceNotRequired = Guid.Parse("59036721-D349-46D3-B8A6-60FFA9263DB3");
    }
}
