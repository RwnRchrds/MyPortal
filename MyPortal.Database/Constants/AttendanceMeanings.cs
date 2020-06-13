using System;
using System.Collections.Generic;

namespace MyPortal.Database.Constants
{
    public static class AttendanceMeanings
    {
        public enum PhysicalMeaning
        {
            InWholeSession,
            LateForSession,
            LeftEarly,
            NoMark,
            OutWholeSession
        }

        private static Dictionary<Guid, PhysicalMeaning> _physicalMappings = new Dictionary<Guid, PhysicalMeaning>
        {
            {Present, PhysicalMeaning.InWholeSession},
            {ApprovedEdActivity, PhysicalMeaning.OutWholeSession},
            {AuthorisedAbsence, PhysicalMeaning.OutWholeSession},
            {UnauthorisedAbsence, PhysicalMeaning.OutWholeSession},
            {Late, PhysicalMeaning.LateForSession},
            {AttendanceNotRequired, PhysicalMeaning.OutWholeSession}
        };

        public static Guid Present { get; } = Guid.Parse("59036717-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid ApprovedEdActivity { get; } = Guid.Parse("59036717-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid AuthorisedAbsence { get; } = Guid.Parse("59036718-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid UnauthorisedAbsence { get; } = Guid.Parse("59036720-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid Late { get; } = Guid.Parse("59036723-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid AttendanceNotRequired { get; } = Guid.Parse("59036721-D349-46D3-B8A6-60FFA9263DB3");

        public static PhysicalMeaning GetPhysicalMeaning(Guid statsMeaningId)
        {
            var result = _physicalMappings.TryGetValue(statsMeaningId, out var phys);

            if (result)
            {
                return phys;
            }

            throw new Exception("Physical meaning not found for meaning ID.");
        }
    }


}
