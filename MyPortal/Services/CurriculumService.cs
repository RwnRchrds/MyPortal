using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using MyPortal.Exceptions;
using MyPortal.Extensions;
using MyPortal.Interfaces;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.Services
{
    public class CurriculumService : MyPortalService
    {
        public CurriculumService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public async Task CreateClass(CurriculumClass @class)
        {
            if (!ValidationService.ModelIsValid(@class))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Invalid data");
            }

            if (!await UnitOfWork.CurriculumAcademicYears.AnyAsync(x => x.Id == @class.AcademicYearId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Academic year not found");
            }

            if (await UnitOfWork.CurriculumClasses.AnyAsync(x => x.Name == @class.Name && x.AcademicYearId == @class.AcademicYearId))
            {
                throw new ServiceException(ExceptionType.BadRequest, "Class already exists");
            }

            UnitOfWork.CurriculumClasses.Add(@class);

            await UnitOfWork.Complete();
        }

        public async Task CreateEnrolment(CurriculumEnrolment enrolment,  bool commitImmediately = true)
        {
            if (!ValidationService.ModelIsValid(enrolment))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Invalid data");
            }

            if (!await UnitOfWork.CurriculumClasses.AnyAsync(x => x.Id == enrolment.ClassId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Class not found");
            }

            if (!await UnitOfWork.Students.AnyAsync(x => x.Id == enrolment.StudentId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Student not found");
            }

            if (!await UnitOfWork.CurriculumSessions.AnyAsync(x => x.ClassId == enrolment.ClassId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Cannot add students to a class with no sessions");
            }

            if (await UnitOfWork.CurriculumEnrolments.AnyAsync(x =>
                x.ClassId == enrolment.ClassId && x.StudentId == enrolment.StudentId))
            {
                throw new ServiceException(ExceptionType.BadRequest,
                    $"{enrolment.Student.GetDisplayName()} is already enrolled in {enrolment.Class.Name}");
            }

            if (await StudentCanEnrol(enrolment.StudentId, enrolment.ClassId))
            {
                UnitOfWork.CurriculumEnrolments.Add(enrolment);
                if (commitImmediately)
                {
                    await UnitOfWork.Complete();
                }
            }

            throw new ServiceException(ExceptionType.BadRequest,"An unknown error occurred");
        }

        public async Task CreateEnrolmentsForMultipleStudents(IEnumerable<Student> students,
            int classId)
        {
            foreach (var student in students)
            {
                var studentEnrolment = new CurriculumEnrolment
                {
                    ClassId = classId,
                    StudentId = student.Id
                };

                await CreateEnrolment(studentEnrolment, false);
            }

            await UnitOfWork.Complete();
        }

        public async Task CreateEnrolmentsForRegGroup(GroupEnrolment enrolment)
        {
            var group = await UnitOfWork.PastoralRegGroups.GetByIdAsync(enrolment.GroupId);

            if (group == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Group not found");
            }

            foreach (var student in group.Students)
            {
                var studentEnrolment = new CurriculumEnrolment
                {
                    ClassId = enrolment.ClassId,
                    StudentId = student.Id
                };

                await CreateEnrolment(studentEnrolment, false);
            }

            await UnitOfWork.Complete();
        }

        public async Task CreateLessonPlan(CurriculumLessonPlan lessonPlan, string userId)
        {
            if (!ValidationService.ModelIsValid(lessonPlan))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Invalid data");
            }

            var authorId = lessonPlan.AuthorId;

            var author = new StaffMember();

            if (authorId == 0)
            {
                author = await UnitOfWork.StaffMembers.GetByUserId(userId);
                if (author == null)
                {
                    throw new ServiceException(ExceptionType.NotFound,"Staff member not found");
                }
            }

            if (authorId != 0)
            {
                author = await UnitOfWork.StaffMembers.GetByIdAsync(authorId);
            }

            if (author == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Staff member not found");
            }

            lessonPlan.AuthorId = author.Id;

            UnitOfWork.CurriculumLessonPlans.Add(lessonPlan);
            await UnitOfWork.Complete();
        }

        public async Task CreateSession(CurriculumSession session)
        {
            if (!ValidationService.ModelIsValid(session))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Invalid data");
            }

            if (!await UnitOfWork.CurriculumClasses.AnyAsync(x => x.Id == session.ClassId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Class not found");
            }

            if (await ClassHasEnrolments(session.ClassId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Cannot modify class schedule while students are enrolled");
            }

            if (await UnitOfWork.CurriculumSessions.AnyAsync(x =>
                x.ClassId == session.ClassId && x.PeriodId == session.PeriodId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Class is already assigned to this period");
            }

            UnitOfWork.CurriculumSessions.Add(session);
            await UnitOfWork.Complete();
        }

        public async Task CreateSessionForRegPeriods(CurriculumSession session)
        {
            session.PeriodId = 0;

            var regPeriods = await UnitOfWork.AttendancePeriods.GetRegPeriods();

            if (! await UnitOfWork.CurriculumClasses.AnyAsync(x => x.Id == session.ClassId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Class not found");
            }

            if (await ClassHasEnrolments(session.ClassId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Cannot modify class schedule while students are enrolled");
            }

            foreach (var period in regPeriods)
            {
                var newSession = new CurriculumSession
                {
                    ClassId = session.ClassId,
                    PeriodId = period.Id
                };

                UnitOfWork.CurriculumSessions.Add(newSession);
            }

            await UnitOfWork.Complete();

        }

        public async Task CreateStudyTopic(CurriculumStudyTopic studyTopic)
        {
            if (!ValidationService.ModelIsValid(studyTopic))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Invalid data");
            }

            UnitOfWork.CurriculumStudyTopics.Add(studyTopic);
            await UnitOfWork.Complete();
        }

        public async Task CreateSubject(CurriculumSubject subject)
        {
            if (subject.Name.IsNullOrWhiteSpace() || !ValidationService.ModelIsValid(subject))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Invalid data");
            }

            UnitOfWork.CurriculumSubjects.Add(subject);
            await UnitOfWork.Complete();
        }

        public async Task DeleteClass(int classId)
        {
            var currClass = await GetClassById(classId);

            if (await ClassHasSessions(classId) || await ClassHasEnrolments(classId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Class cannot be deleted");
            }

            UnitOfWork.CurriculumClasses.Remove(currClass);
            await UnitOfWork.Complete();
        }

        public async Task DeleteEnrolment(int enrolmentId)
        {
            var enrolment = await UnitOfWork.CurriculumEnrolments.GetByIdAsync(enrolmentId);

            if (enrolment == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Enrolment not found");
            }

            UnitOfWork.CurriculumEnrolments.Remove(enrolment);
            await UnitOfWork.Complete();
        }

        public async Task DeleteLessonPlan(int lessonPlanId, string userId, bool canDeleteAll)
        {
            var plan = await GetLessonPlanById(lessonPlanId);

            if (!canDeleteAll && plan.Author.Person.UserId != userId)
            {
                throw new ServiceException(ExceptionType.BadRequest,"Cannot delete someone else's lesson plan");
            }

            UnitOfWork.CurriculumLessonPlans.Remove(plan);
            await UnitOfWork.Complete();
        }

        public async Task DeleteSession(int sessionId)
        {
            var sessionInDb = await GetSessionById(sessionId);

            if (sessionInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Session not found");
            }

            UnitOfWork.CurriculumSessions.Remove(sessionInDb);
            await UnitOfWork.Complete();
        }

        public async Task DeleteStudyTopic(int studyTopicId)
        {
            var studyTopic = await GetStudyTopicById(studyTopicId);

            if (studyTopic == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Study topic not found");
            }

            if (!studyTopic.LessonPlans.Any())
            {
                UnitOfWork.CurriculumStudyTopics.Remove(studyTopic);
                await UnitOfWork.Complete();
            }

            throw new ServiceException(ExceptionType.BadRequest,"This study topic cannot be deleted");
        }

        public async Task DeleteSubject(int subjectId)
        {
            var subjectInDb = await UnitOfWork.CurriculumSubjects.GetByIdAsync(subjectId);

            if (subjectInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Student not found");
            }

            subjectInDb.Deleted = true; //Flag as deleted

            //Delete from database
            if (await UnitOfWork.CurriculumClasses.AnyAsync(x => x.SubjectId == subjectId) ||
                await UnitOfWork.CurriculumStudyTopics.AnyAsync(x => x.SubjectId == subjectId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"This subject cannot be deleted");
            }

            UnitOfWork.CurriculumSubjects.Remove(subjectInDb);

            await UnitOfWork.Complete();
        }

        public async Task<CurriculumAcademicYear> GetAcademicYearById(int academicYearId)
        {
            var academicYear = await UnitOfWork.CurriculumAcademicYears.GetByIdAsync(academicYearId);

            if (academicYear == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Academic year not found");
            }

            return academicYear;
        }

        public async Task<IEnumerable<CurriculumAcademicYear>> GetAcademicYears()
        {
            var academicYears = await UnitOfWork.CurriculumAcademicYears.GetAllAsync();
                
            return academicYears;
        }

        public async Task<IEnumerable<CurriculumClass>> GetAllClasses(int academicYearId)
        {
            var classes = await UnitOfWork.CurriculumClasses.GetByAcademicYear(academicYearId);

            return classes;
        }

        public async Task<IEnumerable<CurriculumClass>> GetAllClassesModel(int academicYearId)
        {
            return await UnitOfWork.CurriculumClasses.GetByAcademicYear(academicYearId);
        }

        public async Task<IEnumerable<CurriculumLessonPlan>> GetAllLessonPlans()
        {
            var lessonPlans = await UnitOfWork.CurriculumLessonPlans.GetAllAsync();

            return lessonPlans;
        }

        public async Task<IEnumerable<CurriculumStudyTopic>> GetAllStudyTopics()
        {
            return await UnitOfWork.CurriculumStudyTopics.GetAllAsync();
        }

        public async Task<IEnumerable<CurriculumStudyTopic>> GetAllStudyTopicsBySubject(int subjectId)
        {
            return await UnitOfWork.CurriculumStudyTopics.GetBySubject(subjectId);
        }

        public async Task<IEnumerable<CurriculumSubject>> GetAllSubjects()
        {
            var subjects = await UnitOfWork.CurriculumSubjects.GetAllAsync();

            return subjects;
        }

        public async Task<CurriculumClass> GetClassById(int classId)
        {
            var currClass = await UnitOfWork.CurriculumClasses.GetByIdAsync(classId);

            if (currClass == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Class not found");
            }

            return currClass;
        }

        public async Task<CurriculumEnrolment> GetEnrolmentById(int enrolmentId)
        {
            var enrolment = await UnitOfWork.CurriculumEnrolments.GetByIdAsync(enrolmentId);

            if (enrolment == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Enrolment not found");
            }

            return enrolment;
        }

        public async Task<IEnumerable<CurriculumEnrolment>> GetEnrolmentsByClass(int classId)
        {
            var list = await UnitOfWork.CurriculumEnrolments.GetByClass(classId);

            return list;
        }

        public async Task<IEnumerable<CurriculumEnrolment>> GetEnrolmentsByStudent(int studentId)
        {
            var list = await UnitOfWork.CurriculumEnrolments.GetByStudent(studentId);

            return list;
        }

        public async Task<CurriculumLessonPlan> GetLessonPlanById(int lessonPlanId)
        {
            var lessonPlan = await UnitOfWork.CurriculumLessonPlans.GetByIdAsync(lessonPlanId);

            if (lessonPlan == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Lesson plan not found");
            }

            return lessonPlan;
        }

        public async Task<IEnumerable<CurriculumLessonPlan>> GetLessonPlansByStudyTopic(int studyTopicId)
        {
            return await UnitOfWork.CurriculumLessonPlans.GetByStudyTopic(studyTopicId);
        }

        public async Task<CurriculumSession> GetSessionById(int sessionId)
        {
            var session = await UnitOfWork.CurriculumSessions.GetByIdAsync(sessionId);

            if (session == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Session not found");
            }

            return session;
        }

        public async Task<IEnumerable<CurriculumSession>> GetSessionsByClass(int classId)
        {
            var sessions = await UnitOfWork.CurriculumSessions.GetByClass(classId);

            return sessions;
        }

        public async Task<IEnumerable<CurriculumSession>> GetSessionsByDate(int staffId, int academicYearId, DateTime date)
        {
            var weekBeginning = date.StartOfWeek();

            var academicYear = await GetAcademicYearById(academicYearId);

            if (academicYear == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Academic year not found");
            }

            if (weekBeginning < academicYear.FirstDate || weekBeginning > academicYear.LastDate)
            {
                throw new ServiceException(ExceptionType.BadRequest,"Selected date is outside academic year");
            }

            var currentWeek = await UnitOfWork.AttendanceWeeks.GetByDate(academicYearId, weekBeginning);

            if (currentWeek == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Attendance week not found");
            }

            if (currentWeek.IsHoliday || currentWeek.IsNonTimetable)
            {
                //throw new ProcessException(ExceptionType.BadRequest,"Selected date is during a school holiday");
                return new List<CurriculumSession>();
            }

            var classList = await UnitOfWork.CurriculumSessions.GetByDayOfWeek(academicYearId, staffId, date.DayOfWeek);

            return classList;
        }

        public async Task<CurriculumStudyTopic> GetStudyTopicById(int studyTopicId)
        {
            var studyTopic = await UnitOfWork.CurriculumStudyTopics.GetByIdAsync(studyTopicId);

            if (studyTopic == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Study topic not found");
            }

            return studyTopic;
        }

        public async Task<CurriculumSubject> GetSubjectById(int subjectId)
        {
            var subject = await UnitOfWork.CurriculumSubjects.GetByIdAsync(subjectId);

            if (subject == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Subject not found");
            }

            return subject;
        }

        public async Task<bool> ClassHasEnrolments(int classId)
        {
            return await UnitOfWork.CurriculumEnrolments.AnyAsync(x => x.ClassId == classId);
        }

        public async Task<bool> ClassHasSessions(int classId)
        {
            return await UnitOfWork.CurriculumSessions.AnyAsync(x => x.ClassId == classId);
        }

        public async Task<bool> PeriodIsFree(int studentId, int periodId)
        {
            return !await UnitOfWork.CurriculumEnrolments.AnyAsync(x =>
                x.StudentId == studentId && x.Class.Sessions.Any(p => p.PeriodId == periodId));
        }

        public async Task<bool> StudentCanEnrol(int studentId, int classId)
        {
            if (!await UnitOfWork.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Student not found");
            }

            if (!await UnitOfWork.CurriculumClasses.AnyAsync(x => x.Id == classId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Class not found");
            }

            if (await UnitOfWork.CurriculumEnrolments.AnyAsync(x =>
                x.ClassId == classId && x.StudentId == studentId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Student is already enrolled in class");
            }

            var sessions = await GetSessionsByClass(classId);
            
                foreach (var session in sessions)
                {
                    if (!await PeriodIsFree(studentId, session.PeriodId))
                    {
                        throw new ServiceException(ExceptionType.BadRequest,$"Student is not free during period {session.Period.Name}");
                    }
                }

                return true;
        }
        public async Task UpdateClass(CurriculumClass @class)
        {
            var classInDb = await GetClassById(@class.Id);

            classInDb.Name = @class.Name;
            classInDb.SubjectId = @class.SubjectId;
            classInDb.TeacherId = @class.TeacherId;
            classInDb.YearGroupId = @class.YearGroupId;

            await UnitOfWork.Complete();
        }
        public async Task UpdateLessonPlan(CurriculumLessonPlan lessonPlan)
        {
            var planInDb = await GetLessonPlanById(lessonPlan.Id);

            planInDb.Title = lessonPlan.Title;
            planInDb.PlanContent = lessonPlan.PlanContent;
            planInDb.StudyTopicId = lessonPlan.StudyTopicId;
            planInDb.LearningObjectives = lessonPlan.LearningObjectives;
            planInDb.Homework = lessonPlan.Homework;

            await UnitOfWork.Complete();
        }

        public async Task UpdateSession(CurriculumSession session)
        {
            if (!ValidationService.ModelIsValid(session))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Invalid data");
            }

            var sessionInDb = await GetSessionById(session.Id);

            if (await ClassHasEnrolments(session.ClassId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Cannot modify class schedule while students are enrolled");
            }

            if (await UnitOfWork.CurriculumSessions.AnyAsync(x =>
                x.ClassId == session.ClassId && x.PeriodId == session.PeriodId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Class already assigned to this period");
            }

            sessionInDb.PeriodId = session.PeriodId;
            await UnitOfWork.Complete();
        }

        public async Task UpdateStudyTopic(CurriculumStudyTopic studyTopic)
        {
            if (!ValidationService.ModelIsValid(studyTopic))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Invalid data");
            }

            var studyTopicInDb = await GetStudyTopicById(studyTopic.Id);

            studyTopicInDb.Name = studyTopic.Name;
            studyTopicInDb.SubjectId = studyTopic.SubjectId;
            studyTopicInDb.YearGroupId = studyTopic.YearGroupId;

            await UnitOfWork.Complete();
        }

        public async Task UpdateSubject(CurriculumSubject subject)
        {
            var subjectInDb = await GetSubjectById(subject.Id);

            subjectInDb.Name = subject.Name;
            subjectInDb.LeaderId = subject.LeaderId;
            await UnitOfWork.Complete();
        }

        public async Task<bool> DateIsInAcademicYear(int academicYearId, DateTime date)
        {
            var academicYear = await GetAcademicYearById(academicYearId);

            return date >= academicYear.FirstDate && date <= academicYear.LastDate;
        }
        
        public async Task<int> GetCurrentAcademicYearId()
        {
            var academicYear = await UnitOfWork.CurriculumAcademicYears.GetCurrent();

            if (academicYear == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "No academic years found");
            }

            return academicYear.Id;
        }

        public async Task<int?> TryGetCurrentAcademicYearId()
        {
            var academicYear = await UnitOfWork.CurriculumAcademicYears.GetCurrent();

            return academicYear?.Id;
        }

        public async Task<int> GetCurrentOrSelectedAcademicYearId(IPrincipal user)
        {
            var academicYear = await UnitOfWork.CurriculumAcademicYears.GetCurrentOrSelected(user);
            
            if (academicYear == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "No academic years found");
            }

            return academicYear.Id;
        }
        
        public async Task<int?> TryGetCurrentOrSelectedAcademicYearId(IPrincipal user)
        {
            var academicYear = await UnitOfWork.CurriculumAcademicYears.GetCurrentOrSelected(user);

            return academicYear?.Id;
        }
    }
}