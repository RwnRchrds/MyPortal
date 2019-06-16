using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using MyPortal.Dtos.SpecialDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class PeopleProcesses
    {
        public static StaffMember GetStaffFromUserId(string userId, MyPortalDbContext context)
        {
            var person = context.Persons.SingleOrDefault(x => x.UserId == userId);

            if (person == null)
            {
                throw new EntityNotFoundException("Person not found");
            }

            var staff = context.StaffMembers.SingleOrDefault(x => x.PersonId == person.Id);

            if (staff == null)
            {
                throw new EntityNotFoundException("Staff member not found");
            }

            return staff;
        }

        public static IEnumerable<StudentSearchDto> PrepareStudentSearchResults(List<Student> students)
        {
            var results = new List<StudentSearchDto>();
            foreach (var student in students)
            {
               var result = new StudentSearchDto
               {
                   Id = student.Id,
                   DisplayName = student.Person.LastName + ", " + student.Person.FirstName,
                   RegGroupName = student.PastoralRegGroup.Name,
                   YearGroupName = student.PastoralYearGroup.Name,
                   HouseName = student.House != null ? student.House.Name : "Not Specified"
               };

               results.Add(result);
            }

            return results.OrderBy(x => x.DisplayName);
        }

        public static string GetStudentDisplayName(Student student)
        {
            return student.Person.LastName + ", " + student.Person.FirstName;
        }
    }
}