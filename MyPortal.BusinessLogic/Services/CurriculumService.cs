using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.BusinessLogic.Extensions;
using MyPortal.BusinessLogic.Models;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Services
{
    public class CurriculumService : MyPortalService
    {
        public CurriculumService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        public CurriculumService() : base()
        {

        }

        public async Task CreateClass(Class @class)
        {
            ValidationService.ValidateModel(@class);

            if (!await UnitOfWork.AcademicYears.Any(x => x.Id == @class.AcademicYearId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Academic year not found");
            }

            if (await UnitOfWork.Classes.Any(x => x.Name == @class.Name && x.AcademicYearId == @class.AcademicYearId))
            {
                throw new ServiceException(ExceptionType.BadRequest, "Class already exists");
            }

            UnitOfWork.Classes.Add(@class);

            await UnitOfWork.Complete();
        }

        public async Task CreateEnrolment(Enrolment enrolment,  bool commitImmediately = true)
        {
            ValidationService.ValidateModel(enrolment);

            var @class = await GetClassById(enrolment.ClassId);

            if (!await UnitOfWork.Students.Any(x => x.Id == enrolment.StudentId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Student not found");
            }

            if (!@class.Sessions.Any())
            {
                throw new ServiceException(ExceptionType.NotFound,"Cannot add students to a class with no sessions");
            }

            if (await UnitOfWork.Enrolments.Any(x =>
                x.ClassId == enrolment.ClassId && x.StudentId == enrolment.StudentId))
            {
                throw new ServiceException(ExceptionType.BadRequest,
                    $"Student is already enrolled in {@class.Name}");
            }

            if (await StudentCanEnrol(enrolment.StudentId, @class.Id, @class.AcademicYearId))
            {
                UnitOfWork.Enrolments.Add(enrolment);

                if (commitImmediately)
                {
                    await UnitOfWork.Complete();
                }

                return;
            }

            throw new ServiceException(ExceptionType.BadRequest,$"Student could not be enrolled in {@class.Name}");
        }

        public async Task CreateEnrolmentsForMultipleStudents(IEnumerable<Student> students,
            int classId)
        {
            foreach (var student in students)
            {
                var studentEnrolment = new Enrolment
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
            var group = await UnitOfWork.RegGroups.GetById(enrolment.GroupId);

            if (group == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Group not found");
            }

            foreach (var student in group.Students)
            {
                var studentEnrolment = new Enrolment
                {
                    ClassId = enrolment.ClassId,
                    StudentId = student.Id
                };

                await CreateEnrolment(studentEnrolment, false);
            }

            await UnitOfWork.Complete();
        }

        public async Task CreateLessonPlan(LessonPlan lessonPlan, string userId)
        {
            ValidationService.ValidateModel(lessonPlan);

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
                author = await UnitOfWork.StaffMembers.GetById(authorId);
            }

            if (author == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Staff member not found");
            }

            lessonPlan.AuthorId = author.Id;

            UnitOfWork.LessonPlans.Add(lessonPlan);
            await UnitOfWork.Complete();
        }

        public async Task CreateSession(Session session)
        {
            ValidationService.ValidateModel(session);

            if (!await UnitOfWork.Classes.Any(x => x.Id == session.ClassId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Class not found");
            }

            if (await ClassHasEnrolments(session.ClassId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Cannot modify class schedule while students are enrolled");
            }

            if (await UnitOfWork.Sessions.Any(x =>
                x.ClassId == session.ClassId && x.PeriodId == session.PeriodId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Class is already assigned to this period");
            }

            UnitOfWork.Sessions.Add(session);
            await UnitOfWork.Complete();
        }

        public async Task CreateSessionForRegPeriods(Session session)
        {
            session.PeriodId = 0;

            var regPeriods = await UnitOfWork.Periods.GetRegPeriods();

            if (! await UnitOfWork.Classes.Any(x => x.Id == session.ClassId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Class not found");
            }

            if (await ClassHasEnrolments(session.ClassId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Cannot modify class schedule while students are enrolled");
            }

            foreach (var period in regPeriods)
            {
                var newSession = new Session
                {
                    ClassId = session.ClassId,
                    PeriodId = period.Id
                };

                UnitOfWork.Sessions.Add(newSession);
            }

            await UnitOfWork.Complete();

        }

        public async Task CreateStudyTopic(StudyTopic studyTopic)
        {
            ValidationService.ValidateModel(studyTopic);

            UnitOfWork.StudyTopics.Add(studyTopic);
            await UnitOfWork.Complete();
        }

        public async Task CreateSubject(Subject subject)
        {
            ValidationService.ValidateModel(subject);

            UnitOfWork.Subjects.Add(subject);
            await UnitOfWork.Complete();
        }

        public async Task DeleteClass(int classId)
        {
            var currClass = await GetClassById(classId);

            if (await ClassHasSessions(classId) || await ClassHasEnrolments(classId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Class cannot be deleted");
            }

            UnitOfWork.Classes.Remove(currClass);
            await UnitOfWork.Complete();
        }

        public async Task DeleteEnrolment(int enrolmentId)
        {
            var enrolment = await UnitOfWork.Enrolments.GetById(enrolmentId);

            if (enrolment == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Enrolment not found");
            }

            UnitOfWork.Enrolments.Remove(enrolment);
            await UnitOfWork.Complete();
        }

        public async Task DeleteLessonPlan(int lessonPlanId, string userId, bool canDeleteAll)
        {
            var plan = await GetLessonPlanById(lessonPlanId);

            if (!canDeleteAll && plan.Author.Person.UserId != userId)
            {
                throw new ServiceException(ExceptionType.BadRequest,"Cannot delete someone else's lesson plan");
            }

            UnitOfWork.LessonPlans.Remove(plan);
            await UnitOfWork.Complete();
        }

        public async Task DeleteSession(int sessionId)
        {
            var sessionInDb = await GetSessionById(sessionId);

            if (sessionInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Session not found");
            }

            UnitOfWork.Sessions.Remove(sessionInDb);
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
                UnitOfWork.StudyTopics.Remove(studyTopic);
                await UnitOfWork.Complete();
            }

            throw new ServiceException(ExceptionType.BadRequest,"This study topic cannot be deleted");
        }

        public async Task DeleteSubject(int subjectId)
        {
            var subjectInDb = await UnitOfWork.Subjects.GetById(subjectId);

            if (subjectInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Student not found");
            }

            //Delete from database
            if (await UnitOfWork.Classes.Any(x => x.SubjectId == subjectId) ||
                await UnitOfWork.StudyTopics.Any(x => x.SubjectId == subjectId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"This subject cannot be deleted");
            }

            UnitOfWork.Subjects.Remove(subjectInDb);

            await UnitOfWork.Complete();
        }

        public async Task<AcademicYear> GetAcademicYearById(int academicYearId)
        {
            var academicYear = await UnitOfWork.AcademicYears.GetById(academicYearId);

            if (academicYear == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Academic year not found");
            }

            return academicYear;
        }

        public async Task<IEnumerable<AcademicYear>> GetAcademicYears()
        {
            var academicYears = await UnitOfWork.AcademicYears.GetAll(x => x.FirstDate);
                
            return academicYears;
        }

        public async Task<IEnumerable<Class>> GetAllClasses(int academicYearId)
        {
            var classes = await UnitOfWork.Classes.GetByAcademicYear(academicYearId);

            return classes;
        }

        public async Task<IEnumerable<Class>> GetClassesBySubject(int subjectId, int academicYearId)
        {
            var classes = await UnitOfWork.Classes.GetBySubject(subjectId, academicYearId);

            return classes;
        }

        public async Task<IEnumerable<LessonPlan>> GetAllLessonPlans()
        {
            var lessonPlans = await UnitOfWork.LessonPlans.GetAll(x => x.Title);

            return lessonPlans;
        }

        public async Task<IEnumerable<StudyTopic>> GetAllStudyTopics()
        {
            return await UnitOfWork.StudyTopics.GetAll(x => x.Name);
        }

        public async Task<IEnumerable<StudyTopic>> GetAllStudyTopicsBySubject(int subjectId)
        {
            return await UnitOfWork.StudyTopics.GetBySubject(subjectId);
        }

        public async Task<IEnumerable<Subject>> GetAllSubjects()
        {
            var subjects = await UnitOfWork.Subjects.GetAll(x => x.Name);

            return subjects;
        }

        public async Task<IEnumerable<SubjectStaffMember>> GetSubjectStaffBySubject(int subjectId)
        {
            var staff = await UnitOfWork.SubjectStaffMembers.GetBySubject(subjectId);

            return staff;
        }

        public async Task<SubjectStaffMember> GetSubjectStaffById(int subjectStaffId)
        {
            var staff = await UnitOfWork.SubjectStaffMembers.GetById(subjectStaffId);

            if (staff == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Subject staff member not found");
            }

            return staff;
        }

        public async Task<IDictionary<int, string>> GetAllSubjectRolesLookup()
        {
            var roles = await UnitOfWork.SubjectStaffMemberRoles.GetAll(x => x.Description);

            return roles.ToDictionary(x => x.Id, x => x.Description);
        }

        public async Task<Class> GetClassById(int classId)
        {
            var currClass = await UnitOfWork.Classes.GetById(classId);

            if (currClass == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Class not found");
            }

            return currClass;
        }

        public async Task<Enrolment> GetEnrolmentById(int enrolmentId)
        {
            var enrolment = await UnitOfWork.Enrolments.GetById(enrolmentId);

            if (enrolment == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Enrolment not found");
            }

            return enrolment;
        }

        public async Task<IEnumerable<Enrolment>> GetEnrolmentsByClass(int classId)
        {
            var list = await UnitOfWork.Enrolments.GetByClass(classId);

            return list;
        }

        public async Task<IEnumerable<Enrolment>> GetEnrolmentsByStudent(int studentId)
        {
            var list = await UnitOfWork.Enrolments.GetByStudent(studentId);

            return list;
        }

        public async Task<LessonPlan> GetLessonPlanById(int lessonPlanId)
        {
            var lessonPlan = await UnitOfWork.LessonPlans.GetById(lessonPlanId);

            if (lessonPlan == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Lesson plan not found");
            }

            return lessonPlan;
        }

        public async Task<IEnumerable<LessonPlan>> GetLessonPlansByStudyTopic(int studyTopicId)
        {
            return await UnitOfWork.LessonPlans.GetByStudyTopic(studyTopicId);
        }

        public async Task<Session> GetSessionById(int sessionId)
        {
            var session = await UnitOfWork.Sessions.GetById(sessionId);

            if (session == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Session not found");
            }

            return session;
        }

        public async Task<Session> GetSessionByIdWithRelated(int sessionId, params Expression<Func<Session, object>>[] includeProperties)
        {
            var session = await UnitOfWork.Sessions.GetByIdWithRelated(sessionId, includeProperties);

            if (session == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Session not found");
            }

            return session;
        }

        public async Task<IEnumerable<Session>> GetSessionsByClass(int classId)
        {
            var sessions = await UnitOfWork.Sessions.GetByClass(classId);

            return sessions;
        }

        public async Task<IEnumerable<Session>> GetSessionsByDate(int staffId, int academicYearId, DateTime date)
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

            var currentWeek = await UnitOfWork.AttendanceWeeks.GetByWeekBeginning(academicYearId, weekBeginning);

            if (currentWeek == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Attendance week not found");
            }

            if (currentWeek.IsHoliday || currentWeek.IsNonTimetable)
            {
                //throw new ProcessException(ExceptionType.BadRequest,"Selected date is during a school holiday");
                return new List<Session>();
            }

            var classList = await UnitOfWork.Sessions.GetByDayOfWeek(academicYearId, staffId, date.DayOfWeek);

            return classList;
        }

        public async Task<StudyTopic> GetStudyTopicById(int studyTopicId)
        {
            var studyTopic = await UnitOfWork.StudyTopics.GetById(studyTopicId);

            if (studyTopic == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Study topic not found");
            }

            return studyTopic;
        }

        public async Task<Subject> GetSubjectById(int subjectId)
        {
            var subject = await UnitOfWork.Subjects.GetById(subjectId);

            if (subject == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Subject not found");
            }

            return subject;
        }

        public async Task<bool> ClassHasEnrolments(int classId)
        {
            return await UnitOfWork.Enrolments.Any(x => x.ClassId == classId);
        }

        public async Task<bool> ClassHasSessions(int classId)
        {
            return await UnitOfWork.Sessions.Any(x => x.ClassId == classId);
        }

        public async Task<bool> PeriodIsFree(int studentId, int periodId, int academicYearId)
        {
            return !await UnitOfWork.Enrolments.Any(x =>
                x.StudentId == studentId && x.Class.Sessions.Any(p => p.PeriodId == periodId && p.Class.AcademicYearId == academicYearId));
        }

        public async Task<bool> StudentCanEnrol(int studentId, int classId, int academicYearId)
        {
            if (!await UnitOfWork.Students.Any(x => x.Id == studentId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Student not found");
            }

            if (!await UnitOfWork.Classes.Any(x => x.Id == classId))
            {
                throw new ServiceException(ExceptionType.NotFound,"Class not found");
            }

            if (await UnitOfWork.Enrolments.Any(x =>
                x.ClassId == classId && x.StudentId == studentId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Student is already enrolled in class");
            }

            var sessions = await GetSessionsByClass(classId);
            
                foreach (var session in sessions)
                {
                    if (!await PeriodIsFree(studentId, session.PeriodId, academicYearId))
                    {
                        throw new ServiceException(ExceptionType.BadRequest,$"Student is not free during period {session.Period.Name}");
                    }
                }

                return true;
        }
        public async Task UpdateClass(Class @class)
        {
            var classInDb = await GetClassById(@class.Id);

            classInDb.Name = @class.Name;
            classInDb.SubjectId = @class.SubjectId;
            classInDb.TeacherId = @class.TeacherId;
            classInDb.YearGroupId = @class.YearGroupId;

            await UnitOfWork.Complete();
        }
        public async Task UpdateLessonPlan(LessonPlan lessonPlan)
        {
            var planInDb = await GetLessonPlanById(lessonPlan.Id);

            planInDb.Title = lessonPlan.Title;
            planInDb.PlanContent = lessonPlan.PlanContent;
            planInDb.StudyTopicId = lessonPlan.StudyTopicId;
            planInDb.LearningObjectives = lessonPlan.LearningObjectives;
            planInDb.Homework = lessonPlan.Homework;

            await UnitOfWork.Complete();
        }

        public async Task UpdateSession(Session session)
        {
            ValidationService.ValidateModel(session);

            var sessionInDb = await GetSessionById(session.Id);

            if (await ClassHasEnrolments(session.ClassId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Cannot modify class schedule while students are enrolled");
            }

            if (await UnitOfWork.Sessions.Any(x =>
                x.ClassId == session.ClassId && x.PeriodId == session.PeriodId))
            {
                throw new ServiceException(ExceptionType.BadRequest,"Class already assigned to this period");
            }

            sessionInDb.PeriodId = session.PeriodId;
            await UnitOfWork.Complete();
        }

        public async Task UpdateStudyTopic(StudyTopic studyTopic)
        {
            ValidationService.ValidateModel(studyTopic);

            var studyTopicInDb = await GetStudyTopicById(studyTopic.Id);

            studyTopicInDb.Name = studyTopic.Name;
            studyTopicInDb.SubjectId = studyTopic.SubjectId;
            studyTopicInDb.YearGroupId = studyTopic.YearGroupId;

            await UnitOfWork.Complete();
        }

        public async Task UpdateSubject(Subject subject)
        {
            var subjectInDb = await GetSubjectById(subject.Id);

            subjectInDb.Name = subject.Name;
            await UnitOfWork.Complete();
        }

        public async Task<bool> DateIsInAcademicYear(int academicYearId, DateTime date)
        {
            var academicYear = await GetAcademicYearById(academicYearId);

            return date >= academicYear.FirstDate && date <= academicYear.LastDate;
        }
        
        public async Task<int> GetCurrentAcademicYearId()
        {
            var academicYear = await UnitOfWork.AcademicYears.GetCurrent();

            if (academicYear == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "No academic years found");
            }

            return academicYear.Id;
        }

        public async Task<int?> TryGetCurrentAcademicYearId()
        {
            var academicYear = await UnitOfWork.AcademicYears.GetCurrent();

            return academicYear?.Id;
        }

        public async Task CreateSubjectStaff(SubjectStaffMember subjectStaff)
        {
            ValidationService.ValidateModel(subjectStaff);

            UnitOfWork.SubjectStaffMembers.Add(subjectStaff);

            await UnitOfWork.Complete();
        }

        public async Task UpdateSubjectStaff(SubjectStaffMember subjectStaff)
        {
            var subjectStaffInDb = await GetSubjectStaffById(subjectStaff.Id);

            subjectStaffInDb.RoleId = subjectStaff.RoleId;

            await UnitOfWork.Complete();
        }

        public async Task DeleteSubjectStaff(int subjectStaffId)
        {
            var subjectStaffInDb = await GetSubjectStaffById(subjectStaffId);

            UnitOfWork.SubjectStaffMembers.Remove(subjectStaffInDb);

            await UnitOfWork.Complete();
        }
    }
}