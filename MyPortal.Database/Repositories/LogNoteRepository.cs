﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class LogNoteRepository : BaseReadWriteRepository<LogNote>, ILogNoteRepository
    {
        public LogNoteRepository(ApplicationDbContext context, IDbConnection connection) : base(context, connection, "LogNote")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(LogNoteType), "LogNoteType");
            query.SelectAllColumns(typeof(User), "User");
            query.SelectAllColumns(typeof(Person), "AuthorPerson");
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(Person), "StudentPerson");
            query.SelectAllColumns(typeof(AcademicYear), "AcademicYear");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("LogNoteTypes as LogNoteType", "LogNoteType.Id", "LogNote.TypeId");
            query.LeftJoin("Users as User", "User.Id", "LogNote.CreatedById");
            query.LeftJoin("People as AuthorPerson", "AuthorPerson.Id", "User.PersonId");
            query.LeftJoin("Students as Student", "Student.Id", "LogNote.StudentId");
            query.LeftJoin("People as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("AcademicYears as AcademicYear", "AcademicYear.Id", "LogNote.AcademicYearId");
        }

        protected override async Task<IEnumerable<LogNote>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync<LogNote, LogNoteType, User, Person, Student, Person, AcademicYear,
                    LogNote>(sql.Sql,
                    (note, type, user, authorPerson, student, studentPerson, acadYear) =>
                    {
                        note.LogNoteType = type;
                        note.CreatedBy = user;
                        note.CreatedBy.Person = authorPerson;
                        note.Student = student;
                        note.Student.Person = studentPerson;
                        note.AcademicYear = acadYear;

                        return note;
                    }, sql.NamedBindings);
        }

        public Task<IEnumerable<LogNote>> GetByStudent(Guid studentId, Guid academicYearId)
        {
            var query = GenerateQuery();

            query.Where("LogNote.StudentId", studentId);
            query.Where("LogNote.AcademicYearId", academicYearId);

            return ExecuteQuery(query);
        }
    }
}