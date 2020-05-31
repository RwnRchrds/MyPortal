using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPortal.Logic.Constants
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

        public static Guid Present = Guid.Parse("59036717-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid ApprovedEdActivity = Guid.Parse("59036717-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid AuthorisedAbsence = Guid.Parse("59036718-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid UnauthorisedAbsence = Guid.Parse("59036720-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid Late = Guid.Parse("59036723-D349-46D3-B8A6-60FFA9263DB3");
        public static Guid AttendanceNotRequired = Guid.Parse("59036721-D349-46D3-B8A6-60FFA9263DB3");

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
