﻿using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class StudentGroupRepository : BaseReadWriteRepository<StudentGroup>, IStudentGroupRepository
    {
        public StudentGroupRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        public async Task Update(StudentGroup entity)
        {
            var studentGroup = await DbUser.Context.StudentGroups.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (studentGroup == null)
            {
                throw new EntityNotFoundException("Student group not found.");
            }

            if (studentGroup.System)
            {
                throw ExceptionHelper.UpdateSystemEntityException;
            }

            studentGroup.Code = entity.Code;
            studentGroup.Notes = entity.Notes;
            studentGroup.Description = entity.Description;
            studentGroup.MaxMembers = entity.MaxMembers;
            studentGroup.Active = entity.Active;
            studentGroup.PromoteToGroupId = entity.PromoteToGroupId;
        }
    }
}