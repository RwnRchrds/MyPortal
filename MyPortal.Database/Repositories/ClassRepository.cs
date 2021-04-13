﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ClassRepository : BaseReadWriteRepository<Class>, IClassRepository
    {
        public ClassRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "Class")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Subject), "Subject");
            query.SelectAllColumns(typeof(StaffMember), "Teacher");
            query.SelectAllColumns(typeof(Person), "TeacherPerson");
            query.SelectAllColumns(typeof(CurriculumGroup), "CurriculumGroup");
            
            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Subjects as Subject", "Subject.Id", "Class.SubjectId");
            query.LeftJoin("StaffMembers as Teacher", "Teacher.Id", "Class.TeacherId");
            query.LeftJoin("People as TeacherPerson", "TeacherPerson.Id", "Teacher.PersonId");
            query.LeftJoin("CurriculumGroups as CurriculumGroup", "CurriculumGroup.Id", "Class.GroupId");
        }

        protected override async Task<IEnumerable<Class>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<Class, Course, StaffMember, Person, CurriculumGroup, Class>(sql.Sql,
                (currClass, course, teacher, person, group) =>
                {
                    currClass.Course = course;
                    currClass.Group = group;

                    return currClass;
                }, sql.NamedBindings);
        }

        public async Task Update(Class entity)
        {
            var currClass = await Context.Classes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (currClass == null)
            {
                throw new EntityNotFoundException("Class not found.");
            }

            currClass.Code = entity.Code;
            currClass.CourseId = entity.CourseId;
            currClass.CurriculumGroupId = entity.CurriculumGroupId;
        }
    }
}