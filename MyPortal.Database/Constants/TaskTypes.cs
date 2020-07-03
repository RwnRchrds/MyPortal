using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPortal.Database.Constants
{
    public class TaskTypes
    {
        public static Guid Homework = Guid.Parse("606D723E-95A7-4356-AA62-1ADADBA4A1C2");

        public static bool IsReserved(Guid taskTypeId)
        {
            var reservedTypes = new Guid[] {Homework};

            return reservedTypes.Contains(taskTypeId);
        }
    }
}
