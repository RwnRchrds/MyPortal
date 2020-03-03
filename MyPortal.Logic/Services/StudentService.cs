﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Dictionaries;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;
using MyPortal.Logic.Models.Details;
using MyPortal.Logic.Models.Student;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class StudentService : BaseService, IStudentService
    {
        private IStudentRepository _repository;

        private Student GenerateSearchObject(StudentSearchParams searchParams)
        {
            return new Student
            {
                RegGroupId = searchParams.RegGroupId ?? Guid.Empty,
                YearGroupId = searchParams.YearGroupId ?? Guid.Empty,
                HouseId = searchParams.HouseId,
                SenStatusId = searchParams.SenStatusId,
                
                Person = new Person
                {
                    FirstName = searchParams.FirstName,
                    MiddleName = searchParams.MiddleName,
                    LastName = searchParams.LastName,
                    Gender = searchParams.Gender,
                    Dob = searchParams.Dob
                }
            };
        }

        public StudentService(IStudentRepository repository)
        {
            _repository = repository;
        }

        public Lookup GetSearchTypes()
        {
            var lookup = new Lookup();
            
            lookup.Add(SearchTypeDictionary.Student.All, "All");
            lookup.Add(SearchTypeDictionary.Student.OnRoll, "On Roll");
            lookup.Add(SearchTypeDictionary.Student.Leavers, "Leavers");
            lookup.Add(SearchTypeDictionary.Student.Future, "Future");

            return lookup;
        }

        public async Task<IEnumerable<StudentDetails>> Get(StudentSearchParams searchParams)
        {
            var searchObject = GenerateSearchObject(searchParams);

            IEnumerable<Student> students;
            
            if (searchParams.SearchType == SearchTypeDictionary.Student.OnRoll)
            {
                students = await _repository.GetOnRoll(searchObject);
            }
            else if (searchParams.SearchType == SearchTypeDictionary.Student.Leavers)
            {
                students = await _repository.GetLeavers(searchObject);
            }
            else if (searchParams.SearchType == SearchTypeDictionary.Student.Future)
            {
                students = await _repository.GetFuture(searchObject);
            }
            else
            {
                students = await _repository.GetAll(searchObject);
            }

            return students.Select(_businessMapper.Map<StudentDetails>).ToList();
        }

        public async Task Create(StudentDetails student)
        {
            _repository.Create(_businessMapper.Map<Student>(student));

            await _repository.SaveChanges();
        }
    }
}