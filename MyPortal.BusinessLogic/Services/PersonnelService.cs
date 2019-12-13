using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public class PersonnelService : MyPortalService
    {
        public PersonnelService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }

        public PersonnelService() : base()
        {

        }
        
        public async Task CreateCourse(TrainingCourse course)
        {
            ValidationService.ValidateModel(course);

            UnitOfWork.TrainingCourses.Add(course);
            await UnitOfWork.Complete();
        }

        public async Task CreateObservation(Observation observation,
            string userId)
        {
            observation.Date = DateTime.Now;

            if (!await UnitOfWork.StaffMembers.Any(x => x.Id == observation.ObserveeId))
            {
                throw new ServiceException(ExceptionType.NotFound, "Observee not found");
            }

            if (!await UnitOfWork.StaffMembers.Any(x => x.Id == observation.ObserverId))
            {
                throw new ServiceException(ExceptionType.NotFound, "Observer not found");
            }

            UnitOfWork.Observations.Add(observation);
            await UnitOfWork.Complete();
        }

        public async Task CreateTrainingCertificate(TrainingCertificate certificate)
        {
            ValidationService.ValidateModel(certificate);

            UnitOfWork.TrainingCertificates.Add(certificate);
            await UnitOfWork.Complete();
        }

        public async Task DeleteCourse(int courseId)
        {
            var courseInDb = await UnitOfWork.TrainingCourses.GetById(courseId);

            if (courseInDb.Certificates.Any())
            {
                throw new ServiceException(ExceptionType.Forbidden, "Cannot delete a course with issued certificates");
            }

            UnitOfWork.TrainingCourses.Remove(courseInDb);
            await UnitOfWork.Complete();
        }

        public async Task DeleteObservation(int observationId)
        {
            var observation = await UnitOfWork.Observations.GetById(observationId);

            UnitOfWork.Observations.Remove(observation);
            await UnitOfWork.Complete();
        }

        public async Task DeleteTrainingCertificate(int staffId, int courseId)
        {
            var certInDb =
                await GetCertificate(staffId, courseId);

            UnitOfWork.TrainingCertificates.Remove(certInDb);
            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<TrainingCourse>> GetAllTrainingCourses()
        {
            return await UnitOfWork.TrainingCourses.GetAll();
        }

        public async Task<TrainingCertificate> GetCertificate(int staffId, int courseId)
        {
            var certInDb = await UnitOfWork.TrainingCertificates.Get(staffId, courseId);

            if (certInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Certificate not found");
            }

            return certInDb;
        }

        public async Task<IEnumerable<TrainingCertificate>> GetCertificatesByStaffMember(
            int staffId)
        {
            var certificates = await UnitOfWork.TrainingCertificates.GetByStaffMember(staffId);

            return certificates;
        }
        
        public async Task<TrainingCourse> GetCourseById(int courseId)
        {
            var courseInDb = await UnitOfWork.TrainingCourses.GetById(courseId);

            if (courseInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Course not found");
            }

            return courseInDb;
        }

        public async Task<Observation> GetObservationById(int observationId)
        {
            var observation = await UnitOfWork.Observations.GetById(observationId);

            if (observation == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Observation not found");
            }

            return observation;
        }

        public async Task<IEnumerable<Observation>> GetObservationsByStaffMember(
            int staffMemberId)
        {
            var observations = await UnitOfWork.Observations.GetByStaffMember(staffMemberId);

            return observations;
        }

        public async Task UpdateCertificate(TrainingCertificate certificate,
                                                    string userId)
        {
            var certInDb =
                await GetCertificate(certificate.StaffId, certificate.CourseId);

            if (certInDb.Status == CertificateStatus.Completed)
            {
                throw new ServiceException(ExceptionType.Forbidden, "Cannot modify a completed certificate");
            }

            certInDb.Status = certificate.Status;

            await UnitOfWork.Complete();
        }
        
        public async Task UpdateCourse(TrainingCourse course)
        {
            var courseInDb = await GetCourseById(course.Id);

            courseInDb.Code = course.Code;
            courseInDb.Description = course.Description;

            await UnitOfWork.Complete();
        }
        
        public async Task UpdateObservation(Observation observation)
        {
            var observationInDb = await GetObservationById(observation.Id);

            observationInDb.Outcome = observation.Outcome;
            observationInDb.ObserverId = observation.ObserverId;

            await UnitOfWork.Complete();
        }
    }
}