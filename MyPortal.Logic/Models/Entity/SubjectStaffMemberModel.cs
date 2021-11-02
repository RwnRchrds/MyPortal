﻿using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Entity
{
    public class SubjectStaffMemberModel : BaseModel, ILoadable
    {
        public SubjectStaffMemberModel(SubjectStaffMember model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(SubjectStaffMember model)
        {
            SubjectId = model.SubjectId;
            StaffMemberId = model.StaffMemberId;
            RoleId = model.RoleId;

            if (model.Subject != null)
            {
                Subject = new SubjectModel(model.Subject);
            }

            if (model.StaffMember != null)
            {
                StaffMember = new StaffMemberModel(model.StaffMember);
            }

            if (model.Role != null)
            {
                Role = new SubjectStaffMemberRoleModel(model.Role);
            }
        }
        
        public Guid SubjectId { get; set; }
        
        public Guid StaffMemberId { get; set; }
        
        public Guid RoleId { get; set; }

        public virtual SubjectModel Subject { get; set; }
        public virtual StaffMemberModel StaffMember { get; set; }
        public virtual SubjectStaffMemberRoleModel Role { get; set; }
        public async Task Load(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.SubjectStaffMembers.GetById(Id.Value);
                
                LoadFromModel(model);
            }
        }
    }
}