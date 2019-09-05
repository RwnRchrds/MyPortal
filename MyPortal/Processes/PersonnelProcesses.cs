using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Resources;
using System.Web;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class PersonnelProcesses
    {
        public static ProcessResponse<object> CreateTrainingCertificate(PersonnelTrainingCertificate certificate,
            string userId, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(certificate))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            context.PersonnelTrainingCertificates.Add(certificate);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Certificate created", null);
        }

        public static ProcessResponse<object> DeleteTrainingCertificate(int staffId, int courseId, string userId, MyPortalDbContext context)
        {
            var certInDb =
                context.PersonnelTrainingCertificates.SingleOrDefault(l => l.StaffId == staffId && l.CourseId == courseId);

            if (certInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Certificate not found", null);
            }

            context.PersonnelTrainingCertificates.Remove(certInDb);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Certificate deleted", null);
        }

        public static ProcessResponse<PersonnelTrainingCertificateDto> GetCertificate(int staffId, int courseId,
            MyPortalDbContext context)
        {
            var certInDb = context.PersonnelTrainingCertificates.Single(x => x.StaffId == staffId && x.CourseId == courseId);

            if (certInDb == null)
            {
                return new ProcessResponse<PersonnelTrainingCertificateDto>(ResponseType.NotFound, "Certificate not found", null);
            }

            return new ProcessResponse<PersonnelTrainingCertificateDto>(ResponseType.Ok, null,
                Mapper.Map<PersonnelTrainingCertificate, PersonnelTrainingCertificateDto>(certInDb));
        }

        public static ProcessResponse<IEnumerable<PersonnelTrainingCertificate>> GetCertificatesForStaffMember_Model(
            int staffId, MyPortalDbContext context)
        {
            var staffInDb = context.StaffMembers.Single(x => x.Id == staffId);

            if (staffInDb == null)
            {
                return new ProcessResponse<IEnumerable<PersonnelTrainingCertificate>>(ResponseType.NotFound, "Staff member not found", null);
            }

            return new ProcessResponse<IEnumerable<PersonnelTrainingCertificate>>(ResponseType.Ok, null,
                context.PersonnelTrainingCertificates
                    .Where(c => c.StaffId == staffId)
                    .ToList());
        }

        public static ProcessResponse<IEnumerable<PersonnelTrainingCertificateDto>> GetCertificatesByStaffMember(
            int staffId, MyPortalDbContext context)
        {
            var staffInDb = context.StaffMembers.Single(x => x.Id == staffId);

            if (staffInDb == null)
            {
                return new ProcessResponse<IEnumerable<PersonnelTrainingCertificateDto>>(ResponseType.NotFound, "Staff member not found", null);
            }

            return new ProcessResponse<IEnumerable<PersonnelTrainingCertificateDto>>(ResponseType.Ok, null,
                GetCertificatesForStaffMember_Model(staffId, context).ResponseObject
                    .Select(Mapper.Map<PersonnelTrainingCertificate, PersonnelTrainingCertificateDto>));
        }

        public static ProcessResponse<IEnumerable<GridPersonnelTrainingCertificateDto>> GetCertificatesForStaffMember_DataGrid(
            int staffId, MyPortalDbContext context)
        {
            var staffInDb = context.StaffMembers.Single(x => x.Id == staffId);

            if (staffInDb == null)
            {
                return new ProcessResponse<IEnumerable<GridPersonnelTrainingCertificateDto>>(ResponseType.NotFound, "Staff member not found", null);
            }

            return new ProcessResponse<IEnumerable<GridPersonnelTrainingCertificateDto>>(ResponseType.Ok, null,
                GetCertificatesForStaffMember_Model(staffId, context).ResponseObject
                    .Select(Mapper.Map<PersonnelTrainingCertificate, GridPersonnelTrainingCertificateDto>));
        }

        public static ProcessResponse<object> UpdateCertificate(PersonnelTrainingCertificate certificate,
            string userId, MyPortalDbContext context)
        {
            var certInDb =
                context.PersonnelTrainingCertificates.Single(x => x.StaffId == certificate.StaffId && x.CourseId == certificate.CourseId);

            if (certInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Certificate not found", null);
            }

            if (certInDb.Status == CertificateStatus.Completed)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Cannot modify a completed certificate", null);
            }

            certInDb.Status = certificate.Status;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Certificate updated", null);
        }
        
        

        public static ProcessResponse<object> DeleteCourse(int courseId, MyPortalDbContext context)
        {
            var courseInDb = context.PersonnelTrainingCourses.Single(x => x.Id == courseId);

            if (courseInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Course not found", null);
            }

            if (courseInDb.PersonnelTrainingCertificates.Any())
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Cannot delete a course with issued certificates", null);
            }

            context.PersonnelTrainingCourses.Remove(courseInDb);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Course deleted", null);
        }

        public static ProcessResponse<PersonnelTrainingCourseDto> GetCourseById(int courseId, MyPortalDbContext context)
        {
            var courseInDb = context.PersonnelTrainingCourses.Single(x => x.Id == courseId);

            if (courseInDb == null)
            {
                return new ProcessResponse<PersonnelTrainingCourseDto>(ResponseType.NotFound, "Course not found", null);
            }

            return new ProcessResponse<PersonnelTrainingCourseDto>(ResponseType.Ok, null,
                Mapper.Map<PersonnelTrainingCourse, PersonnelTrainingCourseDto>(courseInDb));
        }
        
        public static ProcessResponse<IEnumerable<PersonnelTrainingCourse>> GetAllTrainingCourses_Model(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<PersonnelTrainingCourse>>(ResponseType.Ok, null,
                context.PersonnelTrainingCourses
                    .ToList());
        }

        public static ProcessResponse<IEnumerable<PersonnelTrainingCourseDto>> GetAllTrainingCourses(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<PersonnelTrainingCourseDto>>(ResponseType.Ok, null,
                GetAllTrainingCourses_Model(context).ResponseObject
                    .Select(Mapper.Map<PersonnelTrainingCourse, PersonnelTrainingCourseDto>));
        }
        
        public static ProcessResponse<IEnumerable<GridPersonnelTrainingCourseDto>> GetAllTrainingCourses_DataGrid(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridPersonnelTrainingCourseDto>>(ResponseType.Ok, null,
                GetAllTrainingCourses_Model(context).ResponseObject
                    .Select(Mapper.Map<PersonnelTrainingCourse, GridPersonnelTrainingCourseDto>));
        }

        public static ProcessResponse<object> UpdateCourse(PersonnelTrainingCourse course, MyPortalDbContext context)
        {
            var courseInDb = context.PersonnelTrainingCourses.Single(x => x.Id == course.Id);

            if (courseInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Course not found", null);
            }

            courseInDb.Code = course.Code;
            courseInDb.Description = course.Description;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Course updated", null);
        }

        public static ProcessResponse<object> CreateCourse(PersonnelTrainingCourse course, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(course))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            context.PersonnelTrainingCourses.Add(course);
            context.SaveChanges();
            
            return new ProcessResponse<object>(ResponseType.Ok, "Course created", null);
        }

        public static ProcessResponse<object> CreateObservation(PersonnelObservation observation,
            string userId, MyPortalDbContext context)
        {
            observation.Date = DateTime.Now;

            var observer = context.StaffMembers.SingleOrDefault(x => x.Id == observation.ObserverId);

            var observee = context.StaffMembers.Single(x => x.Id == observation.ObserveeId);

            if (observee == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Observee not found", null);
            }

            if (observer == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Observer not found", null);
            }

            observation.ObserverId = observer.Id;

            context.PersonnelObservations.Add(observation);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Observation created", null);
        }

        public static ProcessResponse<PersonnelObservationDto> GetObservationById(int observationId,
            MyPortalDbContext context)
        {
            var observation = context.PersonnelObservations.SingleOrDefault(x => x.Id == observationId);

            if (observation == null)
            {
                return new ProcessResponse<PersonnelObservationDto>(ResponseType.NotFound, "Observation not found", null);
            }

            return new ProcessResponse<PersonnelObservationDto>(ResponseType.Ok, null,
                Mapper.Map<PersonnelObservation, PersonnelObservationDto>(observation));
        }

        public static ProcessResponse<IEnumerable<PersonnelObservationDto>> GetObservationsByStaffMember(
            int staffMemberId, MyPortalDbContext context)
        {
            var staff = context.StaffMembers.Single(x => x.Id == staffMemberId);

            if (staff == null)
            {
                return new ProcessResponse<IEnumerable<PersonnelObservationDto>>(ResponseType.NotFound, "Staff member not found", null);
            }

            var observations = GetObservationsForStaffMember_Model(staffMemberId, context).ResponseObject
                .Select(Mapper.Map<PersonnelObservation, PersonnelObservationDto>);

            return new ProcessResponse<IEnumerable<PersonnelObservationDto>>(ResponseType.Ok, null, observations);
        }

        public static ProcessResponse<IEnumerable<GridPersonnelObservationDto>> GetObservationsForStaffMember_DataGrid(
            int staffMemberId, MyPortalDbContext context)
        {
            var staff = context.StaffMembers.Single(x => x.Id == staffMemberId);

            if (staff == null)
            {
                return new ProcessResponse<IEnumerable<GridPersonnelObservationDto>>(ResponseType.NotFound, "Staff member not found", null);
            }

            var observations = GetObservationsForStaffMember_Model(staffMemberId, context).ResponseObject
                .Select(Mapper.Map<PersonnelObservation, GridPersonnelObservationDto>);

            return new ProcessResponse<IEnumerable<GridPersonnelObservationDto>>(ResponseType.Ok, null, observations);
        }

        public static ProcessResponse<IEnumerable<PersonnelObservation>> GetObservationsForStaffMember_Model(
            int staffMemberId, MyPortalDbContext context)
        {
            var staff = context.StaffMembers.Single(x => x.Id == staffMemberId);

            if (staff == null)
            {
                return new ProcessResponse<IEnumerable<PersonnelObservation>>(ResponseType.NotFound, "Staff member not found", null);
            }

            var observations = context.PersonnelObservations
                .Where(x => x.ObserveeId == staffMemberId)
                .ToList();

            return new ProcessResponse<IEnumerable<PersonnelObservation>>(ResponseType.Ok, null, observations);
        }

        public static ProcessResponse<object> DeleteObservation(int observationId, string userId, MyPortalDbContext context)
        {
            var observation = context.PersonnelObservations.Single(x => x.Id == observationId);

            var staff = context.StaffMembers.SingleOrDefault(x => x.Person.UserId == userId);

            if (staff == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Staff member not found", null);
            }

            if (observation == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Observation not found", null);
            }

            if (observation.ObserveeId == staff.Id)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Cannot delete an observation for yourself", null);
            }

            context.PersonnelObservations.Remove(observation);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Observation deleted", null);
        }

        public static ProcessResponse<object> UpdateObservation(PersonnelObservation observation,
            string userId, MyPortalDbContext context)
        {
            var observationInDb = context.PersonnelObservations.Single(x => x.Id == observation.Id);

            var observer = context.StaffMembers.SingleOrDefault(x => x.Id == observation.ObserverId);

            var staff = context.StaffMembers.SingleOrDefault(x => x.Person.UserId == userId);

            if (staff == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Staff member not found", null);
            }

            if (observer == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Observer not found", null);
            }

            if (observationInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Observation not found", null);
            }

            if (observationInDb.ObserveeId == staff.Id)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Cannot update an observation for yourself", null);
            }

            observationInDb.Outcome = observation.Outcome;
            observationInDb.ObserverId = observer.Id;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Observation updated", null);
        }
    }
}