﻿using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IChargeRepository : IReadWriteRepository<Charge>, IUpdateRepository<Charge>
    {
        
    }
}
