using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Query.Dynamic;
using System.Web.UI.WebControls;
using AutoMapper;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Exceptions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.Services
{
    public class PersonnelService : MyPortalService
    {
        public PersonnelService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }
        
        public async Task CreateCourse(PersonnelTrainingCourse course)
        {
            if (!ValidationService.ModelIsValid(course))
            {
                throw new ProcessException(ExceptionType.BadRequest, "Invalid data");
            }

            UnitOfWork.PersonnelTrainingCourses.Add(course);
            await UnitOfWork.Complete();
        }

        public async Task CreateObservation(PersonnelObservation observation,
            string userId)
        {
            observation.Date = DateTime.Now;

            if (!await UnitOfWork.StaffMembers.AnyAsync(x => x.Id == observation.ObserveeId))
            {
                throw new ProcessException(ExceptionType.NotFound, "Observee not found");
            }

            if (!await UnitOfWork.StaffMembers.AnyAsync(x => x.Id == observation.ObserverId))
            {
                throw new ProcessException(ExceptionType.NotFound, "Observer not found");
            }

            UnitOfWork.PersonnelObservations.Add(observation);
            await UnitOfWork.Complete();
        }

        public async Task CreateTrainingCertificate(PersonnelTrainingCertificate certificate,
                            string userId)
        {
            if (!ValidationService.ModelIsValid(certificate))
            {
                throw new ProcessException(ExceptionType.BadRequest, "Invalid data");
            }

            UnitOfWork.PersonnelTrainingCertificates.Add(certificate);
            await UnitOfWork.Complete();
        }

        public async Task DeleteCourse(int courseId)
        {
            var courseInDb = await UnitOfWork.PersonnelTrainingCourses.GetByIdAsync(courseId);

            if (courseInDb.Certificates.Any())
            {
                throw new ProcessException(ExceptionType.Forbidden, "Cannot delete a course with issued certificates");
            }

            UnitOfWork.PersonnelTrainingCourses.Remove(courseInDb);
            await UnitOfWork.Complete();
        }

        public async Task DeleteObservation(int observationId)
        {
            var observation = await UnitOfWork.PersonnelObservations.GetByIdAsync(observationId);

            UnitOfWork.PersonnelObservations.Remove(observation);
            await UnitOfWork.Complete();
        }

        public async Task DeleteTrainingCertificate(int staffId, int courseId)
        {
            var certInDb =
                await GetCertificate(staffId, courseId);

            UnitOfWork.PersonnelTrainingCertificates.Remove(certInDb);
            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<PersonnelTrainingCourse>> GetAllTrainingCourses()
        {
            return await UnitOfWork.PersonnelTrainingCourses.GetAllAsync();
        }

        public async Task<PersonnelTrainingCertificate> GetCertificate(int staffId, int courseId)
        {
            var certInDb = await UnitOfWork.PersonnelTrainingCertificates.GetCertificate(staffId, courseId);

            if (certInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Certificate not found");
            }

            return certInDb;
        }

        public async Task<IEnumerable<PersonnelTrainingCertificate>> GetCertificatesByStaffMember(
            int staffId)
        {
            var certificates = await UnitOfWork.PersonnelTrainingCertificates.GetCertificatesByStaffMember(staffId);

            return certificates;
        }
        
        public async Task<PersonnelTrainingCourse> GetCourseById(int courseId)
        {
            var courseInDb = await UnitOfWork.PersonnelTrainingCourses.GetByIdAsync(courseId);

            if (courseInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Course not found");
            }

            return courseInDb;
        }

        public async Task<PersonnelObservation> GetObservationById(int observationId)
        {
            var observation = await UnitOfWork.PersonnelObservations.GetByIdAsync(observationId);

            if (observation == null)
            {
                throw new ProcessException(ExceptionType.NotFound, "Observation not found");
            }

            return observation;
        }

        public async Task<IEnumerable<PersonnelObservation>> GetObservationsByStaffMember(
            int staffMemberId)
        {
            var observations = await UnitOfWork.PersonnelObservations.GetObservationsByStaffMember(staffMemberId);

            return observations;
        }

        public async Task UpdateCertificate(PersonnelTrainingCertificate certificate,
                                                    string userId)
        {
            var certInDb =
                await GetCertificate(certificate.StaffId, certificate.CourseId);

            if (certInDb.Status == CertificateStatus.Completed)
            {
                throw new ProcessException(ExceptionType.Forbidden, "Cannot modify a completed certificate");
            }

            certInDb.Status = certificate.Status;

            await UnitOfWork.Complete();
        }
        
        public async Task UpdateCourse(PersonnelTrainingCourse course)
        {
            var courseInDb = await GetCourseById(course.Id);

            courseInDb.Code = course.Code;
            courseInDb.Description = course.Description;

            await UnitOfWork.Complete();
        }
        
        public async Task UpdateObservation(PersonnelObservation observation)
        {
            var observationInDb = await GetObservationById(observation.Id);

            observationInDb.Outcome = observation.Outcome;
            observationInDb.ObserverId = observation.ObserverId;

            await UnitOfWork.Complete();
        }
    }
}