using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Dtos;
using MyPortal.BusinessLogic.Exceptions;
using MyPortal.BusinessLogic.Extensions;
using MyPortal.BusinessLogic.Models;
using MyPortal.BusinessLogic.Models.Data;
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

        public async Task CreateClass(ClassDto @class)
        {
            ValidationService.ValidateModel(@class);

            if (await UnitOfWork.Classes.Any(x => x.Name == @class.Name && x.AcademicYearId == @class.AcademicYearId))
            {
                throw new ServiceException(ExceptionType.BadRequest, "Class already exists");
            }

            UnitOfWork.Classes.Add(Mapper.Map<Class>(@class));

            await UnitOfWork.Complete();
        }

        public async Task CreateEnrolment(EnrolmentDto enrolment,  bool commitImmediately = true)
        {
            ValidationService.ValidateModel(enrolment);

            var @class = await GetClassById(enrolment.ClassId);

            if (await StudentCanEnrol(enrolment.StudentId, @class.Id, @class.AcademicYearId))
            {
                UnitOfWork.Enrolments.Add(Mapper.Map<Enrolment>(enrolment));

                if (commitImmediately)
                {
                    await UnitOfWork.Complete();
                }

                return;
            }

            throw new ServiceException(ExceptionType.BadRequest,$"Student could not be enrolled in {@class.Name}");
        }

        public async Task CreateEnrolmentsForMultipleStudents(IEnumerable<int> studentIds,
            int classId)
        {
            foreach (var studentId in studentIds)
            {
                var studentEnrolment = new EnrolmentDto
                {
                    ClassId = classId,
                    StudentId = studentId
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
                var studentEnrolment = new EnrolmentDto
                {
                    ClassId = enrolment.ClassId,
                    StudentId = student.Id
                };

                await CreateEnrolment(studentEnrolment, false);
            }

            await UnitOfWork.Complete();
        }

        public async Task CreateLessonPlan(LessonPlanDto lessonPlan)
        {
            ValidationService.ValidateModel(lessonPlan);

            UnitOfWork.LessonPlans.Add(Mapper.Map<LessonPlan>(lessonPlan));
            await UnitOfWork.Complete();
        }

        public async Task CreateSession(SessionDto session)
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

            UnitOfWork.Sessions.Add(Mapper.Map<Session>(session));
            await UnitOfWork.Complete();
        }

        public async Task CreateSessionForRegPeriods(SessionDto session)
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

        public async Task CreateStudyTopic(StudyTopicDto studyTopic)
        {
            ValidationService.ValidateModel(studyTopic);

            UnitOfWork.StudyTopics.Add(Mapper.Map<StudyTopic>(studyTopic));
            await UnitOfWork.Complete();
        }

        public async Task CreateSubject(SubjectDto subject)
        {
            ValidationService.ValidateModel(subject);

            UnitOfWork.Subjects.Add(Mapper.Map<Subject>(subject));
            await UnitOfWork.Complete();
        }

        public async Task DeleteClass(int classId)
        {
            var currClass = await UnitOfWork.Classes.GetById(classId);

            if (currClass == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Class not found");
            }

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

        public async Task DeleteLessonPlan(int lessonPlanId)
        {
            var plan = await UnitOfWork.LessonPlans.GetById(lessonPlanId);

            UnitOfWork.LessonPlans.Remove(plan);
            await UnitOfWork.Complete();
        }

        public async Task DeleteSession(int sessionId)
        {
            var sessionInDb = await UnitOfWork.Sessions.GetById(sessionId);

            if (sessionInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Session not found");
            }

            UnitOfWork.Sessions.Remove(sessionInDb);
            await UnitOfWork.Complete();
        }

        public async Task DeleteStudyTopic(int studyTopicId)
        {
            var studyTopic = await UnitOfWork.StudyTopics.GetById(studyTopicId);

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

        public async Task<AcademicYearDto> GetAcademicYearById(int academicYearId)
        {
            var academicYear = await UnitOfWork.AcademicYears.GetById(academicYearId);

            if (academicYear == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Academic year not found");
            }

            return Mapper.Map<AcademicYearDto>(academicYear);
        }

        public async Task<IEnumerable<AcademicYearDto>> GetAcademicYears()
        {
            return (await UnitOfWork.AcademicYears.GetAll(x => x.FirstDate)).Select(Mapper.Map<AcademicYearDto>).ToList();
        }

        public async Task<IEnumerable<ClassDto>> GetAllClasses(int academicYearId)
        {
            return (await UnitOfWork.Classes.GetByAcademicYear(academicYearId)).Select(Mapper.Map<ClassDto>).ToList();
        }

        public async Task<IEnumerable<ClassDto>> GetClassesBySubject(int subjectId, int academicYearId)
        {
            return (await UnitOfWork.Classes.GetBySubject(subjectId, academicYearId)).Select(Mapper.Map<ClassDto>).ToList();
        }

        public async Task<IEnumerable<LessonPlanDto>> GetAllLessonPlans()
        {
            return (await UnitOfWork.LessonPlans.GetAll(x => x.Title)).Select(Mapper.Map<LessonPlanDto>).ToList();
        }

        public async Task<IEnumerable<StudyTopicDto>> GetAllStudyTopics()
        {
            return (await UnitOfWork.StudyTopics.GetAll(x => x.Name)).Select(Mapper.Map<StudyTopicDto>).ToList();
        }

        public async Task<IEnumerable<StudyTopicDto>> GetAllStudyTopicsBySubject(int subjectId)
        {
            return (await UnitOfWork.StudyTopics.GetBySubject(subjectId)).Select(Mapper.Map<StudyTopicDto>).ToList();
        }

        public async Task<IEnumerable<SubjectDto>> GetAllSubjects()
        {
            return (await UnitOfWork.Subjects.GetAll(x => x.Name)).Select(Mapper.Map<SubjectDto>).ToList();
        }

        public async Task<IEnumerable<SubjectStaffMemberDto>> GetSubjectStaffBySubject(int subjectId)
        {
            return (await UnitOfWork.SubjectStaffMembers.GetBySubject(subjectId)).Select(Mapper.Map<SubjectStaffMemberDto>).ToList();
        }

        public async Task<SubjectStaffMemberDto> GetSubjectStaffById(int subjectStaffId)
        {
            var staff = await UnitOfWork.SubjectStaffMembers.GetById(subjectStaffId);

            if (staff == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Subject staff member not found");
            }

            return Mapper.Map<SubjectStaffMemberDto>(staff);
        }

        public async Task<IDictionary<int, string>> GetAllSubjectRolesLookup()
        {
            return (await UnitOfWork.SubjectStaffMemberRoles.GetAll(x => x.Description)).ToDictionary(x => x.Id,
                x => x.Description);
        }

        public async Task<ClassDto> GetClassById(int classId)
        {
            var currClass = await UnitOfWork.Classes.GetById(classId);

            if (currClass == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Class not found");
            }

            return Mapper.Map<ClassDto>(currClass);
        }

        public async Task<EnrolmentDto> GetEnrolmentById(int enrolmentId)
        {
            var enrolment = await UnitOfWork.Enrolments.GetById(enrolmentId);

            if (enrolment == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Enrolment not found");
            }

            return Mapper.Map<EnrolmentDto>(enrolment);
        }

        public async Task<IEnumerable<EnrolmentDto>> GetEnrolmentsByClass(int classId)
        {
            return (await UnitOfWork.Enrolments.GetByClass(classId)).Select(Mapper.Map<EnrolmentDto>).ToList();
        }

        public async Task<IEnumerable<EnrolmentDto>> GetEnrolmentsByStudent(int studentId)
        {
            return (await UnitOfWork.Enrolments.GetByStudent(studentId)).Select(Mapper.Map<EnrolmentDto>).ToList();
        }

        public async Task<LessonPlanDto> GetLessonPlanById(int lessonPlanId)
        {
            var lessonPlan = await UnitOfWork.LessonPlans.GetById(lessonPlanId);

            if (lessonPlan == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Lesson plan not found");
            }

            return Mapper.Map<LessonPlanDto>(lessonPlan);
        }

        public async Task<IEnumerable<LessonPlanDto>> GetLessonPlansByStudyTopic(int studyTopicId)
        {
            return (await UnitOfWork.LessonPlans.GetByStudyTopic(studyTopicId)).Select(Mapper.Map<LessonPlanDto>).ToList();
        }

        public async Task<SessionDto> GetSessionById(int sessionId)
        {
            var session = await UnitOfWork.Sessions.GetById(sessionId);

            if (session == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Session not found");
            }

            return Mapper.Map<SessionDto>(session);
        }

        public async Task<SessionDto> GetSessionByIdWithRelated(int sessionId, params Expression<Func<Session, object>>[] includeProperties)
        {
            var session = await UnitOfWork.Sessions.GetByIdWithRelated(sessionId, includeProperties);

            if (session == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Session not found");
            }

            return Mapper.Map<SessionDto>(session);
        }

        public async Task<IEnumerable<SessionDto>> GetSessionsByClass(int classId)
        {
            return (await UnitOfWork.Sessions.GetByClass(classId)).Select(Mapper.Map<SessionDto>).ToList();
        }

        public async Task<IEnumerable<SessionDto>> GetSessionsByDate(int staffId, int academicYearId, DateTime date)
        {
            var weekBeginning = date.StartOfWeek();

            var currentWeek = await UnitOfWork.AttendanceWeeks.GetByWeekBeginning(academicYearId, weekBeginning);

            if (currentWeek == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Attendance week not found");
            }

            if (currentWeek.IsHoliday || currentWeek.IsNonTimetable)
            {
                //throw new ProcessException(ExceptionType.BadRequest,"Selected date is during a school holiday");
                return new List<SessionDto>();
            }

            return (await UnitOfWork.Sessions.GetByDayOfWeek(academicYearId, staffId, date.DayOfWeek)).Select(Mapper.Map<SessionDto>).ToList();
        }

        public async Task<StudyTopicDto> GetStudyTopicById(int studyTopicId)
        {
            var studyTopic = await UnitOfWork.StudyTopics.GetById(studyTopicId);

            if (studyTopic == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Study topic not found");
            }

            return Mapper.Map<StudyTopicDto>(studyTopic);
        }

        public async Task<SubjectDto> GetSubjectById(int subjectId)
        {
            var subject = await UnitOfWork.Subjects.GetById(subjectId);

            if (subject == null)
            {
                throw new ServiceException(ExceptionType.NotFound,"Subject not found");
            }

            return Mapper.Map<SubjectDto>(subject);
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
        public async Task UpdateClass(ClassDto @class)
        {
            var classInDb = await UnitOfWork.Classes.GetById(@class.Id);

            if (classInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Class not found.");
            }

            classInDb.Name = @class.Name;
            classInDb.SubjectId = @class.SubjectId;
            classInDb.TeacherId = @class.TeacherId;
            classInDb.YearGroupId = @class.YearGroupId;

            await UnitOfWork.Complete();
        }
        public async Task UpdateLessonPlan(LessonPlanDto lessonPlan)
        {
            var planInDb = await UnitOfWork.LessonPlans.GetById(lessonPlan.Id);

            if (planInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Lesson plan not found.");
            }

            planInDb.Title = lessonPlan.Title;
            planInDb.PlanContent = lessonPlan.PlanContent;
            planInDb.StudyTopicId = lessonPlan.StudyTopicId;
            planInDb.LearningObjectives = lessonPlan.LearningObjectives;
            planInDb.Homework = lessonPlan.Homework;

            await UnitOfWork.Complete();
        }

        public async Task UpdateSession(SessionDto session)
        {
            ValidationService.ValidateModel(session);

            var sessionInDb = await UnitOfWork.Sessions.GetById(session.Id);

            if (sessionInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Session not found");
            }

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

        public async Task UpdateStudyTopic(StudyTopicDto studyTopic)
        {
            ValidationService.ValidateModel(studyTopic);

            var studyTopicInDb = await UnitOfWork.StudyTopics.GetById(studyTopic.Id);

            if (studyTopicInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Study topic not found.");
            }

            studyTopicInDb.Name = studyTopic.Name;
            studyTopicInDb.SubjectId = studyTopic.SubjectId;
            studyTopicInDb.YearGroupId = studyTopic.YearGroupId;

            await UnitOfWork.Complete();
        }

        public async Task UpdateSubject(SubjectDto subject)
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

        public async Task CreateSubjectStaff(SubjectStaffMemberDto subjectStaff)
        {
            ValidationService.ValidateModel(subjectStaff);

            UnitOfWork.SubjectStaffMembers.Add(Mapper.Map<SubjectStaffMember>(subjectStaff));

            await UnitOfWork.Complete();
        }

        public async Task UpdateSubjectStaff(SubjectStaffMemberDto subjectStaff)
        {
            var subjectStaffInDb = await UnitOfWork.SubjectStaffMembers.GetById(subjectStaff.Id);

            if (subjectStaffInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Subject staff member not found.");
            }

            subjectStaffInDb.RoleId = subjectStaff.RoleId;

            await UnitOfWork.Complete();
        }

        public async Task DeleteSubjectStaff(int subjectStaffId)
        {
            var subjectStaffInDb = await UnitOfWork.SubjectStaffMembers.GetById(subjectStaffId);

            if (subjectStaffInDb == null)
            {
                throw new ServiceException(ExceptionType.NotFound, "Subject staff member not found.");
            }

            UnitOfWork.SubjectStaffMembers.Remove(subjectStaffInDb);

            await UnitOfWork.Complete();
        }
    }
}