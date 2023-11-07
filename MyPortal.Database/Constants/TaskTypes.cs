using System;
using System.Linq;

namespace MyPortal.Database.Constants
{
    public class TaskTypes
    {
        public static Guid Homework = Guid.Parse("606D723E-95A7-4356-AA62-1ADADBA4A1C2");

        public static bool IsReserved(Guid taskTypeId)
        {
            var reservedTypes = new[] { Homework };

            return reservedTypes.Contains(taskTypeId);
        }
    }
}