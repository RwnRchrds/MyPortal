﻿using System;
using System.Collections.Generic;
using System.Linq;
using MyPortal.Models.Misc;
using Syncfusion.EJ2.Base;

namespace MyPortal.Services
{
    public class UtilityService : IDisposable
    {
        public string GenerateId()
        {
            return Guid.NewGuid().ToString("N");
        }

        public void Dispose()
        {
            
        }
    }
}