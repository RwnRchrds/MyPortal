﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Interfaces
{
    public interface IAcademicYearService : IService
    {
        Task<AcademicYearModel> GetCurrent();
        Task<AcademicYearModel> GetById(Guid academicYearId);
        Task<AcademicYearModel> GetAll();
        Task Create(params AcademicYearModel[] academicYearModels);
        Task Update(params AcademicYearModel[] academicYearModels);
        Task Delete(params Guid[] academicYearIds);
        Task<bool> IsLocked(Guid academicYearId);
        
    }
}
