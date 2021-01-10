﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.Search;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class StudentService : BaseService, IStudentService
    {
        private IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<StudentModel> GetById(Guid studentId)
        {
            var student = await _studentRepository.GetById(studentId);
            if (student == null)
            {
                throw new NotFoundException("Student not found.");
            }

            return BusinessMapper.Map<StudentModel>(student);
        }

        public async Task<StudentModel> GetByUserId(Guid userId, bool throwNotFound = true)
        {
            var student = await _studentRepository.GetByUserId(userId);

            if (student == null && throwNotFound)
            {
                throw new NotFoundException("Student not found.");
            }

            return BusinessMapper.Map<StudentModel>(student);
        }

        public async Task<StudentModel> GetByPersonId(Guid personId, bool throwIfNotFound = true)
        {
            var student = await _studentRepository.GetByPersonId(personId);

            if (student == null && throwIfNotFound)
            {
                throw new NotFoundException("Student not found.");
            }

            return BusinessMapper.Map<StudentModel>(student);
        }

        public SelectList GetStudentStatusOptions(StudentStatus defaultStatus = StudentStatus.OnRoll)
        {
            var searchTypes = new Dictionary<string, int>();

            searchTypes.Add("Any", (int)StudentStatus.Any);
            searchTypes.Add("On Roll", (int)StudentStatus.OnRoll);
            searchTypes.Add("Leavers", (int)StudentStatus.Leavers);
            searchTypes.Add("Future", (int)StudentStatus.Future);

            return new SelectList(searchTypes, "Value", "Key", (int)defaultStatus);
        }

        public async Task<IEnumerable<StudentModel>> Get(StudentSearchOptions searchOptions)
        {
            var students = await _studentRepository.GetAll(searchOptions);

            return students.Select(BusinessMapper.Map<StudentModel>).ToList();
        }

        public async Task Create(StudentModel student)
        {
            _studentRepository.Create(BusinessMapper.Map<Student>(student));

            await _studentRepository.SaveChanges();
        }

        public async Task Update(StudentModel student)
        {
            var studentInDb = await _studentRepository.GetByIdWithTracking(student.Id);

            await _studentRepository.SaveChanges();
        }

        public override void Dispose()
        {
            _studentRepository.Dispose();
        }
    }
}