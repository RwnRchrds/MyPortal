using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Enums;

namespace MyPortal.Logic.Helpers
{
    public class PermissionHelper
    {
        public static BitArray CreatePermissionArray()
        {
            var array = new BitArray(Enum.GetNames(typeof(PermissionValue)).Length);

            return array;
        }
    }
}
