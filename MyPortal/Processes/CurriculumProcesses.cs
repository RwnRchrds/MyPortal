using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Dtos.LiteDtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public static class CurriculumProcesses
    {
        public static async Task CreateClass(CurriculumClass @class, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(@class))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            if (!await context.CurriculumAcademicYears.AnyAsync(x => x.Id == @class.AcademicYearId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Academic year not found");
            }

            if (await context.CurriculumClasses.AnyAsync(x => x.Name == @class.Name && x.AcademicYearId == @class.AcademicYearId))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Class already exists");
            }

            context.CurriculumClasses.Add(@class);

            await context.SaveChangesAsync();
        }

        public static async Task CreateEnrolment(CurriculumEnrolment enrolment, MyPortalDbContext context, bool commitImmediately = true)
        {
            if (!ValidationProcesses.ModelIsValid(enrolment))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            if (!await context.CurriculumClasses.AnyAsync(x => x.Id == enrolment.ClassId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            if (!await context.Students.AnyAsync(x => x.Id == enrolment.StudentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            if (!await context.CurriculumSessions.AnyAsync(x => x.ClassId == enrolment.ClassId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Cannot add students to a class with no sessions");
            }

            if (await context.CurriculumEnrolments.AnyAsync(x =>
                x.ClassId == enrolment.ClassId && x.StudentId == enrolment.StudentId))
            {
                throw new ProcessException(ExceptionType.BadRequest,
                    $"{PeopleProcesses.GetStudentDisplayName(enrolment.Student).ResponseObject} is already enrolled in {enrolment.Class.Name}");
            }

            if (await StudentCanEnrol(context, enrolment.StudentId, enrolment.ClassId))
            {
                context.CurriculumEnrolments.Add(enrolment);
                if (commitImmediately)
                {
                    await context.SaveChangesAsync();
                }
            }

            throw new ProcessException(ExceptionType.BadRequest,"An unknown error occurred");
        }

        public static async Task CreateEnrolmentsForMultipleStudents(IEnumerable<Student> students,
            int classId, MyPortalDbContext context)
        {
            foreach (var student in students)
            {
                var studentEnrolment = new CurriculumEnrolment
                {
                    ClassId = classId,
                    StudentId = student.Id
                };

                await CreateEnrolment(studentEnrolment, context, false);
            }

            await context.SaveChangesAsync();
        }

        public static async Task CreateEnrolmentsForRegGroup(GroupEnrolment enrolment,
            MyPortalDbContext context)
        {
            var group = await context.PastoralRegGroups.SingleOrDefaultAsync(x => x.Id == enrolment.GroupId);

            if (group == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Group not found");
            }

            foreach (var student in group.Students)
            {
                var studentEnrolment = new CurriculumEnrolment
                {
                    ClassId = enrolment.ClassId,
                    StudentId = student.Id
                };

                await CreateEnrolment(studentEnrolment, context, false);
            }

            await context.SaveChangesAsync();
        }

        public static async Task CreateLessonPlan(CurriculumLessonPlan lessonPlan, string userId,
            MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(lessonPlan))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            var authorId = lessonPlan.AuthorId;

            var author = new StaffMember();

            if (authorId == 0)
            {
                author = await context.StaffMembers.SingleOrDefaultAsync(x => x.Person.UserId == userId);
                if (author == null)
                {
                    throw new ProcessException(ExceptionType.NotFound,"Staff member not found");
                }
            }

            if (authorId != 0)
            {
                author = await context.StaffMembers.SingleOrDefaultAsync(x => x.Id == authorId);
            }

            if (author == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Staff member not found");
            }

            lessonPlan.AuthorId = author.Id;

            context.CurriculumLessonPlans.Add(lessonPlan);
            await context.SaveChangesAsync();
        }

        public static async Task CreateSession(CurriculumSession session, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(session))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            if (!await context.CurriculumClasses.AnyAsync(x => x.Id == session.ClassId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            if (await HasEnrolments(session.ClassId, context))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot modify class schedule while students are enrolled");
            }

            if (context.CurriculumSessions.Any(x =>
                x.ClassId == session.ClassId && x.PeriodId == session.PeriodId))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Class is already assigned to this period");
            }

            context.CurriculumSessions.Add(session);
            await context.SaveChangesAsync();
        }

        public static async Task CreateSessionForRegPeriods(CurriculumSession session,
            MyPortalDbContext context)
        {
            session.PeriodId = 0;

            var regPeriods = await context.AttendancePeriods.Where(x => x.IsAm || x.IsPm).ToListAsync();

            if (! await context.CurriculumClasses.AnyAsync(x => x.Id == session.ClassId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            if (await HasEnrolments(session.ClassId, context))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot modify class schedule while students are enrolled");
            }

            foreach (var newAssignment in regPeriods.Select(period => new {period, period1 = period})
                .Where(@t =>
                    !context.CurriculumSessions.Any(x => x.ClassId == session.ClassId && x.PeriodId == @t.period1.Id))
                .Select(@t => new CurriculumSession {PeriodId = @t.period.Id, ClassId = session.ClassId}))
            {
                context.CurriculumSessions.Add(newAssignment);
            }

            await context.SaveChangesAsync();

        }

        public static async Task CreateStudyTopic(CurriculumStudyTopic studyTopic,
            MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(studyTopic))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            context.CurriculumStudyTopics.Add(studyTopic);
            await context.SaveChangesAsync();
        }

        public static async Task CreateSubject(CurriculumSubject subject, MyPortalDbContext context)
        {
            if (subject.Name.IsNullOrWhiteSpace() || !ValidationProcesses.ModelIsValid(subject))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            context.CurriculumSubjects.Add(subject);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteClass(int classId, MyPortalDbContext context)
        {
            var currClass = await context.CurriculumClasses.SingleOrDefaultAsync(x => x.Id == classId);

            if (currClass == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            if (await HasSessions(classId, context) || await HasEnrolments(classId, context))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Class cannot be deleted");
            }

            context.CurriculumClasses.Remove(currClass);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteEnrolment(int enrolmentId, MyPortalDbContext context)
        {
            var enrolment = await context.CurriculumEnrolments.SingleOrDefaultAsync(x => x.Id == enrolmentId);

            if (enrolment == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Enrolment not found");
            }

            context.CurriculumEnrolments.Remove(enrolment);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteLessonPlan(int lessonPlanId, int staffId, bool canDeleteAll, MyPortalDbContext context)
        {
            var plan = await context.CurriculumLessonPlans.SingleOrDefaultAsync(x => x.Id == lessonPlanId);

            if (plan == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Lesson plan not found");
            }

            if (!canDeleteAll && plan.AuthorId != staffId)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot delete someone else's lesson plan");
            }

            context.CurriculumLessonPlans.Remove(plan);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteSession(int sessionId, MyPortalDbContext context)
        {
            var sessionInDb = await context.CurriculumSessions.SingleOrDefaultAsync(x => x.Id == sessionId);

            if (sessionInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Session not found");
            }

            context.CurriculumSessions.Remove(sessionInDb);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteStudyTopic(int studyTopicId, MyPortalDbContext context)
        {
            var studyTopic = await context.CurriculumStudyTopics.SingleOrDefaultAsync(x => x.Id == studyTopicId);

            if (studyTopic == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Study topic not found");
            }

            if (!studyTopic.LessonPlans.Any())
            {
                context.CurriculumStudyTopics.Remove(studyTopic);
                await context.SaveChangesAsync();
            }

            throw new ProcessException(ExceptionType.BadRequest,"This study topic cannot be deleted");
        }

        public static async Task DeleteSubject(int subjectId, MyPortalDbContext context)
        {
            var subjectInDb = await context.CurriculumSubjects.SingleOrDefaultAsync(x => x.Id == subjectId);

            if (subjectInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            subjectInDb.Deleted = true; //Flag as deleted

            //Delete from database
            if (await context.CurriculumClasses.AnyAsync(x => x.SubjectId == subjectId) ||
                await context.CurriculumStudyTopics.AnyAsync(x => x.SubjectId == subjectId))
            {
                throw new ProcessException(ExceptionType.BadRequest,"This subject cannot be deleted");
            }

            context.CurriculumSubjects.Remove(subjectInDb);

            await context.SaveChangesAsync();
        }

        public static async Task<CurriculumAcademicYearDto> GetAcademicYearById(int academicYearId,
            MyPortalDbContext context)
        {
            var academicYear = await context.CurriculumAcademicYears.SingleOrDefaultAsync(x => x.Id == academicYearId);

            if (academicYear == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Academic year not found");
            }

            return Mapper.Map<CurriculumAcademicYear, CurriculumAcademicYearDto>(academicYear);
        }

        public static async Task<IEnumerable<CurriculumAcademicYearDto>> GetAcademicYears(
            MyPortalDbContext context)
        {
            var academicYears = await GetAcademicYearsModel(context);
                
            return academicYears.Select(Mapper.Map<CurriculumAcademicYear, CurriculumAcademicYearDto>);
        }

        public static async Task<IEnumerable<CurriculumAcademicYear>> GetAcademicYearsModel(
            MyPortalDbContext context)
        {
            var academicYears = await context.CurriculumAcademicYears.OrderByDescending(x => x.FirstDate).ToListAsync();

            return academicYears;
        }

        public static async Task<IEnumerable<CurriculumClassDto>> GetAllClasses(int academicYearId,
            MyPortalDbContext context)
        {
            var classes = await GetAllClassesModel(academicYearId, context);

            return classes.Select(Mapper.Map<CurriculumClass, CurriculumClassDto>);
        }

        public static async Task<IEnumerable<GridCurriculumClassDto>> GetAllClassesDataGrid(int academicYearId,
            MyPortalDbContext context)
        {
            var classes = await GetAllClassesModel(academicYearId, context);

            return classes.Select(Mapper.Map<CurriculumClass, GridCurriculumClassDto>);
        }

        public static async Task<IEnumerable<CurriculumClass>> GetAllClassesModel(int academicYearId,
            MyPortalDbContext context)
        {
            return await context.CurriculumClasses.Where(x => x.AcademicYearId == academicYearId).OrderBy(x => x.Name)
                .ToListAsync();
        }

        public static async Task<IEnumerable<CurriculumLessonPlanDto>> GetAllLessonPlans(MyPortalDbContext context)
        {
            var lessonPlans = await context.CurriculumLessonPlans.OrderBy(x => x.Title).ToListAsync();

            return lessonPlans.Select(Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>);
        }

        public static async Task<IEnumerable<CurriculumStudyTopicDto>> GetAllStudyTopics(MyPortalDbContext context)
        {
            var studyTopics = await GetAllStudyTopicsModel(context);

            return studyTopics.Select(Mapper.Map<CurriculumStudyTopic, CurriculumStudyTopicDto>);
        }

        public static async Task<IEnumerable<GridCurriculumStudyTopicDto>> GetAllStudyTopicsDataGrid(MyPortalDbContext context)
        {
            var studyTopics = await GetAllStudyTopicsModel(context);

            return studyTopics.Select(Mapper.Map<CurriculumStudyTopic, GridCurriculumStudyTopicDto>);
        }

        public static async Task<IEnumerable<CurriculumStudyTopic>> GetAllStudyTopicsModel(MyPortalDbContext context)
        {
            return await context.CurriculumStudyTopics.OrderBy(x => x.Name).ToListAsync();
        }

        public static async Task<IEnumerable<CurriculumSubjectDto>> GetAllSubjects(MyPortalDbContext context)
        {
            var subjects = await GetAllSubjectsModel(context);

            return subjects.Select(Mapper.Map<CurriculumSubject, CurriculumSubjectDto>);
        }

        public static async Task<IDictionary<int, string>> GetAllSubjectsLookup(MyPortalDbContext context)
        {
            var subjects = await GetAllSubjectsModel(context);

            return subjects.ToDictionary(x => x.Id, x => x.Name);
        }

        public static async Task<IEnumerable<GridCurriculumSubjectDto>> GetAllSubjectsDataGrid(MyPortalDbContext context)
        {
            var subjects = await GetAllSubjectsModel(context);

            return subjects.Select(Mapper.Map<CurriculumSubject, GridCurriculumSubjectDto>);
        }

        public static async Task<IEnumerable<CurriculumSubject>> GetAllSubjectsModel(MyPortalDbContext context)
        {
            var subjects = await context.CurriculumSubjects.Where(x => !x.Deleted).OrderBy(x => x.Name).ToListAsync();

            return subjects;
        }

        public static async Task<CurriculumClassDto> GetClassById(int classId, MyPortalDbContext context)
        {
            var currClass = await context.CurriculumClasses.SingleOrDefaultAsync(x => x.Id == classId);

            if (currClass == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            return Mapper.Map<CurriculumClass, CurriculumClassDto>(currClass);
        }

        public static async Task<CurriculumEnrolmentDto> GetEnrolmentById(int enrolmentId,
            MyPortalDbContext context)
        {
            var enrolment = await context.CurriculumEnrolments.SingleOrDefaultAsync(x => x.Id == enrolmentId);

            if (enrolment == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Enrolment not found");
            }

            return Mapper.Map<CurriculumEnrolment, CurriculumEnrolmentDto>(enrolment);
        }

        public static async Task<IEnumerable<CurriculumEnrolmentDto>> GetEnrolmentsForClass(int classId,
            MyPortalDbContext context)
        {
            var list = await GetEnrolmentsForClassModel(classId, context);

            return list.Select(Mapper.Map<CurriculumEnrolment, CurriculumEnrolmentDto>);
        }

        public static async Task<IEnumerable<GridCurriculumEnrolmentDto>> GetEnrolmentsForClassDataGrid(int classId,
            MyPortalDbContext context)
        {
            var list = await GetEnrolmentsForClassModel(classId, context);

            return list.Select(Mapper.Map<CurriculumEnrolment, GridCurriculumEnrolmentDto>);
        }

        public static async Task<IEnumerable<CurriculumEnrolment>> GetEnrolmentsForClassModel(int classId,
            MyPortalDbContext context)
        {
            var list = await context.CurriculumEnrolments.Where(x => x.ClassId == classId)
                .OrderBy(x => x.Student.Person.LastName).ToListAsync();

            return list;
        }

        public static async Task<IEnumerable<CurriculumEnrolmentDto>> GetEnrolmentsForStudent(int studentId,
            MyPortalDbContext context)
        {
            var list = await GetEnrolmentsForStudentModel(studentId, context);

            return list.Select(Mapper.Map<CurriculumEnrolment, CurriculumEnrolmentDto>);
        }

        public static async Task<IEnumerable<GridCurriculumEnrolmentDto>> GetEnrolmentsForStudentDataGrid(int studentId,
            MyPortalDbContext context)
        {
            var list = await GetEnrolmentsForStudentModel(studentId, context);

            return list.Select(Mapper.Map<CurriculumEnrolment, GridCurriculumEnrolmentDto>);
        }

        public static async Task<IEnumerable<CurriculumEnrolment>> GetEnrolmentsForStudentModel(int studentId,
            MyPortalDbContext context)
        {
            var list = await context.CurriculumEnrolments.Where(x => x.StudentId == studentId)
                .OrderBy(x => x.Student.Person.LastName).ToListAsync();

            return list;
        }

        public static async Task<CurriculumLessonPlanDto> GetLessonPlanById(int lessonPlanId,
            MyPortalDbContext context)
        {
            var lessonPlan = await context.CurriculumLessonPlans.SingleOrDefaultAsync(x => x.Id == lessonPlanId);

            if (lessonPlan == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Lesson plan not found");
            }

            return Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>(lessonPlan);
        }

        public static async Task<IEnumerable<CurriculumLessonPlanDto>> GetLessonPlansByStudyTopic(int studyTopicId,
            MyPortalDbContext context)
        {
            var lessonPlans = await GetLessonPlansByStudyTopicModel(studyTopicId, context);

            return lessonPlans.Select(Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>);
        }

        public static async Task<IEnumerable<GridCurriculumLessonPlanDto>> GetLessonPlansByStudyTopicDataGrid(int studyTopicId,
            MyPortalDbContext context)
        {
            var lessonPlans = await GetLessonPlansByStudyTopicModel(studyTopicId, context);

            return lessonPlans.Select(Mapper.Map<CurriculumLessonPlan, GridCurriculumLessonPlanDto>);
        }

        public static async Task<IEnumerable<CurriculumLessonPlan>> GetLessonPlansByStudyTopicModel(int studyTopicId,
            MyPortalDbContext context)
        {
            return await context.CurriculumLessonPlans.Where(x => x.StudyTopicId == studyTopicId)
                .Include(x => x.StudyTopic).OrderBy(x => x.Title)
                .ToListAsync();
        }

        public static async Task<IEnumerable<AttendancePeriod>> GetPeriodsForClass(MyPortalDbContext context, int classId)
        {
            if (!await context.CurriculumClasses.AnyAsync(x => x.Id == classId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            return await context.AttendancePeriods.Where(x => x.Sessions.Any(p => p.ClassId == classId)).ToListAsync();
        }

        public static async Task<CurriculumSessionDto> GetSessionById(int sessionId, MyPortalDbContext context)
        {
            var session = await context.CurriculumSessions.SingleOrDefaultAsync(x => x.Id == sessionId);

            if (session == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Session not found");
            }

            return Mapper.Map<CurriculumSession, CurriculumSessionDto>(session);
        }

        public static async Task<IEnumerable<CurriculumSessionDto>> GetSessionsByTeacherOnDayOfWeek(int staffId, int academicYearId, DateTime date,
            MyPortalDbContext context)
        {
            var classes = await GetSessionsByTeacherOnDayOfWeekModel(staffId, academicYearId, date, context);

            return classes.Select(Mapper.Map<CurriculumSession, CurriculumSessionDto>);
        }

        public static async Task<IEnumerable<GridCurriculumSessionDto>> GetSessionsByTeacherOnDayOfWeekDataGrid(int staffId, int academicYearId, DateTime date,
            MyPortalDbContext context)
        {
            var classes = await GetSessionsByTeacherOnDayOfWeekModel(staffId, academicYearId, date, context);

            return classes.Select(Mapper.Map<CurriculumSession, GridCurriculumSessionDto>);
        }

        public static async Task<IEnumerable<CurriculumSessionDto>> GetSessionsByClass(int classId,
            MyPortalDbContext context)
        {
            var sessions = await GetSessionsForClassModel(classId, context);

            return sessions.Select(Mapper.Map<CurriculumSession, CurriculumSessionDto>);
        }

        public static async Task<IEnumerable<GridCurriculumSessionDto>> GetSessionsForClass_DataGrid(int classId,
            MyPortalDbContext context)
        {
            var sessions = await GetSessionsForClassModel(classId, context);

            return sessions.Select(Mapper.Map<CurriculumSession, GridCurriculumSessionDto>);
        }

        public static async Task<IEnumerable<CurriculumSession>> GetSessionsForClassModel(int classId,
            MyPortalDbContext context)
        {
            return await context.CurriculumSessions.Where(x => x.ClassId == classId).ToListAsync();
        }

        public static async Task<IEnumerable<CurriculumSession>> GetSessionsByTeacherOnDayOfWeekModel(int staffId, int academicYearId, DateTime date,
            MyPortalDbContext context)
        {
            var weekBeginning = date.StartOfWeek();

            var academicYear = await context.CurriculumAcademicYears.SingleOrDefaultAsync(x => x.Id == academicYearId);

            if (academicYear == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Academic year not found");
            }

            if (weekBeginning < academicYear.FirstDate || weekBeginning > academicYear.LastDate)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Selected date is outside academic year");
            }

            var currentWeek = await context.AttendanceWeeks.SingleOrDefaultAsync(x => x.Beginning == weekBeginning && x.AcademicYearId == academicYearId);

            if (currentWeek == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Attendance week not found");
            }

            if (currentWeek.IsHoliday)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Selected date is during a school holiday");
            }

            var classList = await context.CurriculumSessions
                .Where(x =>
                    x.Class.AcademicYearId == academicYearId && x.AttendancePeriod.Weekday ==
                    date.DayOfWeek && x.Class.TeacherId == staffId)
                .OrderBy(x => x.AttendancePeriod.StartTime)
                .ToListAsync();

            return classList;
        }

        public static async Task<CurriculumStudyTopicDto> GetStudyTopicById(int studyTopicId,
            MyPortalDbContext context)
        {
            var studyTopic = await context.CurriculumStudyTopics.SingleOrDefaultAsync(x => x.Id == studyTopicId);

            if (studyTopic == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Study topic not found");
            }

            return Mapper.Map<CurriculumStudyTopic, CurriculumStudyTopicDto>(studyTopic);
        }

        public static async Task<CurriculumSubjectDto> GetSubjectById(int subjectId, MyPortalDbContext context)
        {
            var subject = await context.CurriculumSubjects.SingleOrDefaultAsync(x => x.Id == subjectId);

            if (subject == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Subject not found");
            }

            return Mapper.Map<CurriculumSubject, CurriculumSubjectDto>(subject);
        }

        public static string GetSubjectNameForClass(CurriculumClass @class)
        {
            if (@class == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            if (@class.SubjectId == null || @class.SubjectId == 0)
            {
                return "No Subject";
            }

            return @class.CurriculumSubject.Name;
        }

        public static async Task<bool> HasEnrolments(int classId, MyPortalDbContext context)
        {
            return await context.CurriculumEnrolments.AnyAsync(x => x.ClassId == classId);
        }

        public static async Task<bool> HasSessions(int classId, MyPortalDbContext context)
        {
            return await context.CurriculumSessions.AnyAsync(x => x.ClassId == classId);
        }

        public static async Task<bool> IsInAcademicYear(this DateTime date, MyPortalDbContext context, int academicYearId)
        {
            var academicYear = await context.CurriculumAcademicYears.SingleOrDefaultAsync(x => x.Id == academicYearId);

            if (academicYear == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Academic year not found");
            }

            return date >= academicYear.FirstDate && date <= academicYear.LastDate;
        }

        public static async Task<bool> PeriodIsFree(MyPortalDbContext context, int studentId, int periodId)
        {
            return !await context.CurriculumEnrolments.AnyAsync(x =>
                x.StudentId == studentId && x.Class.Sessions.Any(p => p.PeriodId == periodId));
        }

        public static async Task<bool> StudentCanEnrol(MyPortalDbContext context, int studentId, int classId)
        {
            if (!await context.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            if (!await context.CurriculumClasses.AnyAsync(x => x.Id == classId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            if (await context.CurriculumEnrolments.AnyAsync(x =>
                x.ClassId == classId && x.StudentId == studentId))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Student is already enrolled in class");
            }

            var periods = await GetPeriodsForClass(context, classId);
            
                foreach (var period in periods)
                {
                    if (!await PeriodIsFree(context, studentId, period.Id))
                    {
                        throw new ProcessException(ExceptionType.BadRequest,$"Student is not free during period {period.Name}");
                    }
                }

                return true;
        }
        public static async Task UpdateClass(CurriculumClass @class, MyPortalDbContext context)
        {
            var classInDb = await context.CurriculumClasses.SingleOrDefaultAsync(x => x.Id == @class.Id);

            if (classInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            classInDb.Name = @class.Name;
            classInDb.SubjectId = @class.SubjectId;
            classInDb.TeacherId = @class.TeacherId;
            classInDb.YearGroupId = @class.YearGroupId;

            await context.SaveChangesAsync();
        }
        public static async Task UpdateLessonPlan(CurriculumLessonPlan lessonPlan,
            MyPortalDbContext context)
        {
            var planInDb = await context.CurriculumLessonPlans.SingleOrDefaultAsync(x => x.Id == lessonPlan.Id);

            if (planInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Lesson plan not found");
            }

            planInDb.Title = lessonPlan.Title;
            planInDb.PlanContent = lessonPlan.PlanContent;
            planInDb.StudyTopicId = lessonPlan.StudyTopicId;
            planInDb.LearningObjectives = lessonPlan.LearningObjectives;
            planInDb.Homework = lessonPlan.Homework;

            await context.SaveChangesAsync();
        }

        public static async Task UpdateSession(CurriculumSession session, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(session))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            var sessionInDb = await context.CurriculumSessions.SingleOrDefaultAsync(x => x.Id == session.Id);

            if (sessionInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Session not found");
            }

            if (await HasEnrolments(session.ClassId, context))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot modify class schedule while students are enrolled");
            }

            if (await context.CurriculumSessions.AnyAsync(x =>
                x.ClassId == session.ClassId && x.PeriodId == session.PeriodId))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Class already assigned to this period");
            }

            sessionInDb.PeriodId = session.PeriodId;
            await context.SaveChangesAsync();
        }
        public static async Task UpdateStudyTopic(CurriculumStudyTopic studyTopic,
            MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(studyTopic))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            var studyTopicInDb = await context.CurriculumStudyTopics.SingleOrDefaultAsync(x => x.Id == studyTopic.Id);

            if (studyTopicInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Study topic not found");
            }

            studyTopicInDb.Name = studyTopic.Name;
            studyTopicInDb.SubjectId = studyTopic.SubjectId;
            studyTopicInDb.YearGroupId = studyTopic.YearGroupId;

            await context.SaveChangesAsync();
        }

        public static async Task UpdateSubject(CurriculumSubject subject, MyPortalDbContext context)
        {
            var subjectInDb = await context.CurriculumSubjects.SingleOrDefaultAsync(x => x.Id == subject.Id);

            if (subjectInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Subject not found");
            }

            subjectInDb.Name = subject.Name;
            subjectInDb.LeaderId = subject.LeaderId;
            await context.SaveChangesAsync();
        }
    }
}