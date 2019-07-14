using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class PeopleProcesses
    {
        public static ProcessResponse<StaffMember> GetStaffFromUserId(string userId, MyPortalDbContext context)
        {
            var person = context.Persons.SingleOrDefault(x => x.UserId == userId);

            if (person == null)
            {
                return new ProcessResponse<StaffMember>(ResponseType.NotFound, "Person not found", null);
            }

            var staff = context.StaffMembers.SingleOrDefault(x => x.PersonId == person.Id);

            if (staff == null)
            {
                return new ProcessResponse<StaffMember>(ResponseType.NotFound, "Staff record not found", null);
            }

            return new ProcessResponse<StaffMember>(ResponseType.Ok, null, staff);
        }

        public static string GetStudentDisplayName(Student student)
        {
            return student.Person.LastName + ", " + student.Person.FirstName;
        }

        public static string GetStaffDisplayName(StaffMember staffMember)
        {
            return staffMember.Person.Title + " " + staffMember.Person.FirstName.Substring(0, 1) +
                   " " + staffMember.Person.LastName;
        }
    }
}