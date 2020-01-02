using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Builders;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Dtos;
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
        
        public async Task CreateCourse(TrainingCourseDto course)
        {
            ValidationService.ValidateModel(course);

            UnitOfWork.TrainingCourses.Add(Mapper.Map<TrainingCourse>(course));
            await UnitOfWork.Complete();
        }

        public async Task CreateObservation(ObservationDto observation,
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

            UnitOfWork.Observations.Add(Mapper.Map<Observation>(observation));
            await UnitOfWork.Complete();
        }

        public async Task CreateTrainingCertificate(TrainingCertificateDto certificate)
        {
            ValidationService.ValidateModel(certificate);

            UnitOfWork.TrainingCertificates.Add(Mapper.Map<TrainingCertificate>(certificate));
            await UnitOfWork.Complete();
        }

        public async Task DeleteCourse(int courseId)
        {
            var courseInDb = await UnitOfWork.TrainingCourses.GetById(courseId);

            if (courseInDb.Certificates.Any())
            {
                throw new ServiceException(ExceptionType.Forbidden, "Cannot delete a course with issued certificates.");
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
            var certInDb = await UnitOfWork.TrainingCertificates.Get(staffId, courseId);

            UnitOfWork.TrainingCertificates.Remove(certInDb);
            await UnitOfWork.Complete();
        }

        public async Task<IEnumerable<TrainingCourseDto>> GetAllTrainingCourses()
        {
            return (await UnitOfWork.TrainingCourses.GetAll()).Select(Mapper.Map<TrainingCourseDto>);
        }

        public async Task<TrainingCertificateDto> GetCertificate(int staffId, int courseId)
        {
            var certInDb = await UnitOfWork.TrainingCertificates.Get(staffId, courseId);

            if (certInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Certificate not found");
            }

            return Mapper.Map<TrainingCertificateDto>(certInDb);
        }

        public async Task<IEnumerable<TrainingCertificateDto>> GetCertificatesByStaffMember(
            int staffId)
        {
            return (await UnitOfWork.TrainingCertificates.GetByStaffMember(staffId)).Select(Mapper.Map<TrainingCertificateDto>);
        }
        
        public async Task<TrainingCourseDto> GetCourseById(int courseId)
        {
            var courseInDb = await UnitOfWork.TrainingCourses.GetById(courseId);

            if (courseInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Course not found");
            }

            return Mapper.Map<TrainingCourseDto>(courseInDb);
        }

        public async Task<ObservationDto> GetObservationById(int observationId)
        {
            var observation = await UnitOfWork.Observations.GetById(observationId);

            if (observation == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Observation not found");
            }

            return Mapper.Map<ObservationDto>(observation);
        }

        public async Task<IEnumerable<ObservationDto>> GetObservationsByStaffMember(
            int staffMemberId)
        {
            return (await UnitOfWork.Observations.GetByStaffMember(staffMemberId)).Select(Mapper.Map<ObservationDto>);
        }

        public async Task UpdateCertificate(TrainingCertificateDto certificate)
        {
            var certInDb =
                await UnitOfWork.TrainingCertificates.Get(certificate.StaffId, certificate.CourseId);

            certInDb.Status = certificate.Status;

            await UnitOfWork.Complete();
        }
        
        public async Task UpdateCourse(TrainingCourseDto course)
        {
            var courseInDb = await UnitOfWork.TrainingCourses.GetById(course.Id);

            if (courseInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Training course not found.");
            }

            courseInDb.Code = course.Code;
            courseInDb.Description = course.Description;

            await UnitOfWork.Complete();
        }
        
        public async Task UpdateObservation(ObservationDto observation)
        {
            var observationInDb = await UnitOfWork.Observations.GetById(observation.Id);

            if (observationInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Observation not found.");
            }

            observationInDb.Outcome = observation.Outcome;
            observationInDb.ObserverId = observation.ObserverId;

            await UnitOfWork.Complete();
        }
    }
}