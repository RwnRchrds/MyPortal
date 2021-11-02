﻿using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class YearGroupModel : BaseModel, ILoadable
    {
        public YearGroupModel(YearGroup model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(YearGroup model)
        {
            StudentGroupId = model.StudentGroupId;
            CurriculumYearGroupId = model.CurriculumYearGroupId;

            if (model.StudentGroup != null)
            {
                StudentGroup = new StudentGroupModel(model.StudentGroup);
            }

            if (model.CurriculumYearGroup != null)
            {
                CurriculumYearGroup = new CurriculumYearGroupModel(model.CurriculumYearGroup);
            }
        }
        
        public Guid StudentGroupId { get; set; }
        
        public Guid CurriculumYearGroupId { get; set; }

        public virtual StudentGroupModel StudentGroup { get; set; }
        public virtual CurriculumYearGroupModel CurriculumYearGroup { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.YearGroups.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}