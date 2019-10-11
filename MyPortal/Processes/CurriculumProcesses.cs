using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                throw new BadRequestException("Invalid data");
            }

            if (!await context.CurriculumAcademicYears.AnyAsync(x => x.Id == @class.AcademicYearId))
            {
                throw new NotFoundException("Academic year not found");
            }

            if (await context.CurriculumClasses.AnyAsync(x => x.Name == @class.Name && x.AcademicYearId == @class.AcademicYearId))
            {
                throw new BadRequestException("Class already exists");
            }

            context.CurriculumClasses.Add(@class);

            await context.SaveChangesAsync();
        }

        public static async Task CreateEnrolment(CurriculumEnrolment enrolment, MyPortalDbContext context, bool commitImmediately = true)
        {
            if (!ValidationProcesses.ModelIsValid(enrolment))
            {
                throw new BadRequestException("Invalid data");
            }

            if (!await context.CurriculumClasses.AnyAsync(x => x.Id == enrolment.ClassId))
            {
                throw new NotFoundException("Class not found");
            }

            if (!await context.Students.AnyAsync(x => x.Id == enrolment.StudentId))
            {
                throw new NotFoundException("Student not found");
            }

            if (!await context.CurriculumSessions.AnyAsync(x => x.ClassId == enrolment.ClassId))
            {
                throw new NotFoundException("Cannot add students to a class with no sessions");
            }

            if (await context.CurriculumEnrolments.AnyAsync(x =>
                x.ClassId == enrolment.ClassId && x.StudentId == enrolment.StudentId))
            {
                throw new BadRequestException(
                    $"{PeopleProcesses.GetStudentDisplayName(enrolment.Student).ResponseObject} is already enrolled in {enrolment.Class.Name}");
            }

            var canEnroll = StudentCanEnrol(context, enrolment.StudentId, enrolment.ClassId);

            if (canEnroll.ResponseObject)
            {
                context.CurriculumEnrolments.Add(enrolment);
                if (commitImmediately)
                {
                    await context.SaveChangesAsync();
                }
            }

            throw new BadRequestException("An unknown error occurred");
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
                throw new NotFoundException("Group not found");
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
                throw new BadRequestException("Invalid data");
            }

            var authorId = lessonPlan.AuthorId;

            var author = new StaffMember();

            if (authorId == 0)
            {
                author = await context.StaffMembers.SingleOrDefaultAsync(x => x.Person.UserId == userId);
                if (author == null)
                {
                    throw new NotFoundException("Staff member not found");
                }
            }

            if (authorId != 0)
            {
                author = await context.StaffMembers.SingleOrDefaultAsync(x => x.Id == authorId);
            }

            if (author == null)
            {
                throw new NotFoundException("Staff member not found");
            }

            lessonPlan.AuthorId = author.Id;

            context.CurriculumLessonPlans.Add(lessonPlan);
            await context.SaveChangesAsync();
        }

        public static async Task CreateSession(CurriculumSession session, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(session))
            {
                throw new BadRequestException("Invalid data");
            }

            if (!await context.CurriculumClasses.AnyAsync(x => x.Id == session.ClassId))
            {
                throw new NotFoundException("Class not found");
            }

            if (await HasEnrolments(session.ClassId, context))
            {
                throw new BadRequestException("Cannot modify class schedule while students are enrolled");
            }

            if (context.CurriculumSessions.Any(x =>
                x.ClassId == session.ClassId && x.PeriodId == session.PeriodId))
            {
                throw new BadRequestException("Class is already assigned to this period");
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
                throw new NotFoundException("Class not found");
            }

            if (await HasEnrolments(session.ClassId, context))
            {
                throw new BadRequestException("Cannot modify class schedule while students are enrolled");
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
                throw new BadRequestException("Invalid data");
            }

            context.CurriculumStudyTopics.Add(studyTopic);
            await context.SaveChangesAsync();
        }

        public static async Task CreateSubject(CurriculumSubject subject, MyPortalDbContext context)
        {
            if (subject.Name.IsNullOrWhiteSpace() || !ValidationProcesses.ModelIsValid(subject))
            {
                throw new BadRequestException("Invalid data");
            }

            context.CurriculumSubjects.Add(subject);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteClass(int classId, MyPortalDbContext context)
        {
            var currClass = context.CurriculumClasses.SingleOrDefault(x => x.Id == classId);

            if (currClass == null)
            {
                throw new NotFoundException("Class not found");
            }

            if (await HasSessions(classId, context) || await HasEnrolments(classId, context))
            {
                throw new BadRequestException("Class cannot be deleted");
            }

            context.CurriculumClasses.Remove(currClass);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteEnrolment(int enrolmentId, MyPortalDbContext context)
        {
            var enrolment = context.CurriculumEnrolments.SingleOrDefault(x => x.Id == enrolmentId);

            if (enrolment == null)
            {
                throw new NotFoundException("Enrolment not found");
            }

            context.CurriculumEnrolments.Remove(enrolment);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteLessonPlan(int lessonPlanId, int staffId, bool canDeleteAll, MyPortalDbContext context)
        {
            var plan = await context.CurriculumLessonPlans.SingleOrDefaultAsync(x => x.Id == lessonPlanId);

            if (plan == null)
            {
                throw new NotFoundException("Lesson plan not found");
            }

            if (!canDeleteAll && plan.AuthorId != staffId)
            {
                throw new BadRequestException("Cannot delete someone else's lesson plan");
            }

            context.CurriculumLessonPlans.Remove(plan);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteSession(int sessionId, MyPortalDbContext context)
        {
            var sessionInDb = await context.CurriculumSessions.SingleOrDefaultAsync(x => x.Id == sessionId);

            if (sessionInDb == null)
            {
                throw new NotFoundException("Session not found");
            }

            context.CurriculumSessions.Remove(sessionInDb);
            await context.SaveChangesAsync();
        }

        public static async Task DeleteStudyTopic(int studyTopicId, MyPortalDbContext context)
        {
            var studyTopic = await context.CurriculumStudyTopics.SingleOrDefaultAsync(x => x.Id == studyTopicId);

            if (studyTopic == null)
            {
                throw new NotFoundException("Study topic not found");
            }

            if (!studyTopic.LessonPlans.Any())
            {
                context.CurriculumStudyTopics.Remove(studyTopic);
                await context.SaveChangesAsync();
            }

            throw new BadRequestException("This study topic cannot be deleted");
        }

        public static async Task DeleteSubject(int subjectId, MyPortalDbContext context)
        {
            var subjectInDb = await context.CurriculumSubjects.SingleOrDefaultAsync(x => x.Id == subjectId);

            if (subjectInDb == null)
            {
                throw new NotFoundException("Student not found");
            }

            subjectInDb.Deleted = true; //Flag as deleted

            //Delete from database
            if (await context.CurriculumClasses.AnyAsync(x => x.SubjectId == subjectId) ||
                await context.CurriculumStudyTopics.AnyAsync(x => x.SubjectId == subjectId))
            {
                throw new BadRequestException("This subject cannot be deleted");
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
                throw new NotFoundException("Academic year not found");
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

        public static async Task<IEnumerable<GridCurriculumSubjectDto>> GetAllSubjects_DataGrid(MyPortalDbContext context)
        {
            var subjects = await GetAllSubjectsModel(context);

            return subjects.Select(Mapper.Map<CurriculumSubject, GridCurriculumSubjectDto>);
        }

        public static async Task<IEnumerable<CurriculumSubject>> GetAllSubjectsModel(MyPortalDbContext context)
        {
            var subjects = await context.CurriculumSubjects.Where(x => !x.Deleted).ToListAsync();

            return subjects;
        }

        public static async Task<CurriculumClassDto> GetClassById(int classId, MyPortalDbContext context)
        {
            var currClass = await context.CurriculumClasses.SingleOrDefaultAsync(x => x.Id == classId);

            if (currClass == null)
            {
                throw new NotFoundException("Class not found");
            }

            return Mapper.Map<CurriculumClass, CurriculumClassDto>(currClass);
        }

        public static async Task<CurriculumEnrolmentDto> GetEnrolmentById(int enrolmentId,
            MyPortalDbContext context)
        {
            var enrolment = await context.CurriculumEnrolments.SingleOrDefaultAsync(x => x.Id == enrolmentId);

            if (enrolment == null)
            {
                throw new NotFoundException("Enrolment not found");
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
            var list = GetEnrolmentsForStudentModel(studentId, context).ResponseObject
                .OrderBy(x => x.Student.Person.LastName)
                .Select(Mapper.Map<CurriculumEnrolment, CurriculumEnrolmentDto>);

            return new ProcessResponse<IEnumerable<CurriculumEnrolmentDto>>(ResponseType.Ok, null, list);
        }

        public static async Task<IEnumerable<GridCurriculumEnrolmentDto>> GetEnrolmentsForStudent_DataGrid(int studentId,
            MyPortalDbContext context)
        {
            var list = GetEnrolmentsForStudentModel(studentId, context).ResponseObject
                .OrderBy(x => x.Student.Person.LastName)
                .Select(Mapper.Map<CurriculumEnrolment, GridCurriculumEnrolmentDto>);

            return new ProcessResponse<IEnumerable<GridCurriculumEnrolmentDto>>(ResponseType.Ok, null, list);
        }

        public static async Task<IEnumerable<CurriculumEnrolment>> GetEnrolmentsForStudentModel(int studentId,
            MyPortalDbContext context)
        {
            var list = await context.CurriculumEnrolments.Where(x => x.StudentId == studentId)
                .OrderBy(x => x.Student.Person.LastName).ToListAsync();

            return list;
        }

        public static ProcessResponse<CurriculumLessonPlanDto> GetLessonPlanById(int lessonPlanId,
            MyPortalDbContext context)
        {
            var lessonPlan = context.CurriculumLessonPlans.SingleOrDefault(x => x.Id == lessonPlanId);

            if (lessonPlan == null)
            {
                return new ProcessResponse<CurriculumLessonPlanDto>(ResponseType.NotFound, "Lesson plan not found", null);
            }

            return new ProcessResponse<CurriculumLessonPlanDto>(ResponseType.Ok, null,
                Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>(lessonPlan));
        }

        public static ProcessResponse<IEnumerable<CurriculumLessonPlanDto>> GetLessonPlansByStudyTopic(int studyTopicId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<CurriculumLessonPlanDto>>(ResponseType.Ok, null,
                GetLessonPlansByStudyTopic_Model(studyTopicId, context).ResponseObject
                    .Select(Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>));
        }

        public static ProcessResponse<IEnumerable<GridCurriculumLessonPlanDto>> GetLessonPlansByStudyTopic_DataGrid(int studyTopicId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridCurriculumLessonPlanDto>>(ResponseType.Ok, null,
                GetLessonPlansByStudyTopic_Model(studyTopicId, context).ResponseObject
                    .Select(Mapper.Map<CurriculumLessonPlan, GridCurriculumLessonPlanDto>));
        }

        public static ProcessResponse<IEnumerable<CurriculumLessonPlan>> GetLessonPlansByStudyTopic_Model(int studyTopicId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<CurriculumLessonPlan>>(ResponseType.Ok, null,
                context.CurriculumLessonPlans.Where(x => x.StudyTopicId == studyTopicId).Include(x => x.StudyTopic).OrderBy(x => x.Title)
                    .ToList());
        }

        public static ProcessResponse<IEnumerable<AttendancePeriod>> GetPeriodsForClass(MyPortalDbContext context, int classId)
        {
            var @class = context.CurriculumClasses.SingleOrDefault(x => x.Id == classId);

            if (@class == null)
            {
                return new ProcessResponse<IEnumerable<AttendancePeriod>>(ResponseType.NotFound, "Class not found", null);
            }

            return new ProcessResponse<IEnumerable<AttendancePeriod>>(ResponseType.Ok, null,
                context.AttendancePeriods.Where(x => x.Sessions.Any(p => p.ClassId == classId)).ToList());
        }

        public static ProcessResponse<CurriculumSessionDto> GetSessionById(int sessionId, MyPortalDbContext context)
        {
            var session = context.CurriculumSessions.SingleOrDefault(x => x.Id == sessionId);

            if (session == null)
            {
                return new ProcessResponse<CurriculumSessionDto>(ResponseType.NotFound, "Session not found", null);
            }

            return new ProcessResponse<CurriculumSessionDto>(ResponseType.Ok, null,
                Mapper.Map<CurriculumSession, CurriculumSessionDto>(session));
        }

        public static ProcessResponse<IEnumerable<CurriculumSessionDto>> GetSessionsByTeacher(int staffId, int academicYearId, DateTime date,
            MyPortalDbContext context)
        {
            var classList = GetSessionsForTeacher_Model(staffId, academicYearId, date, context).ResponseObject
                .Select(Mapper.Map<CurriculumSession, CurriculumSessionDto>);

            return new ProcessResponse<IEnumerable<CurriculumSessionDto>>(ResponseType.Ok, null, classList);
        }

        public static ProcessResponse<IEnumerable<GridCurriculumSessionDto>> GetSessionsByTeacher_DataGrid(int staffId, int academicYearId, DateTime date,
            MyPortalDbContext context)
        {
            var classList = GetSessionsForTeacher_Model(staffId, academicYearId, date, context).ResponseObject
                .Select(Mapper.Map<CurriculumSession, GridCurriculumSessionDto>);

            return new ProcessResponse<IEnumerable<GridCurriculumSessionDto>>(ResponseType.Ok, null, classList);
        }

        public static ProcessResponse<IEnumerable<CurriculumSessionDto>> GetSessionsForClass(int classId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<CurriculumSessionDto>>(ResponseType.Ok, null,
                GetSessionsForClass_Model(classId, context).ResponseObject
                    .Select(Mapper.Map<CurriculumSession, CurriculumSessionDto>));
        }

        public static ProcessResponse<IEnumerable<GridCurriculumSessionDto>> GetSessionsForClass_DataGrid(int classId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridCurriculumSessionDto>>(ResponseType.Ok, null,
                GetSessionsForClass_Model(classId, context).ResponseObject
                    .Select(Mapper.Map<CurriculumSession, GridCurriculumSessionDto>));
        }

        public static ProcessResponse<IEnumerable<CurriculumSession>> GetSessionsForClass_Model(int classId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<CurriculumSession>>(ResponseType.Ok, null,
                context.CurriculumSessions.Where(x => x.ClassId == classId).ToList());
        }

        public static ProcessResponse<IEnumerable<CurriculumSession>> GetSessionsForTeacher_Model(int staffId, int academicYearId, DateTime date,
            MyPortalDbContext context)
        {
            var weekBeginning = date.StartOfWeek();

            var academicYear = context.CurriculumAcademicYears.SingleOrDefault(x => x.Id == academicYearId);

            if (academicYear == null)
            {
                return new ProcessResponse<IEnumerable<CurriculumSession>>(ResponseType.NotFound, "Academic year not found", null);
            }

            if (weekBeginning < academicYear.FirstDate || weekBeginning > academicYear.LastDate)
            {
                return new ProcessResponse<IEnumerable<CurriculumSession>>(ResponseType.Ok, null, new List<CurriculumSession>());
            }

            var currentWeek = context.AttendanceWeeks.SingleOrDefault(x => x.Beginning == weekBeginning && x.AcademicYearId == academicYearId);

            if (currentWeek == null || currentWeek.IsHoliday)
            {
                return new ProcessResponse<IEnumerable<CurriculumSession>>(ResponseType.Ok, null, new List<CurriculumSession>());
            }

            var classList = context.CurriculumSessions
                .Where(x =>
                    x.Class.AcademicYearId == academicYearId && x.AttendancePeriod.Weekday ==
                    date.DayOfWeek && x.Class.TeacherId == staffId)
                .OrderBy(x => x.AttendancePeriod.StartTime)
                .ToList();

            return new ProcessResponse<IEnumerable<CurriculumSession>>(ResponseType.Ok, null, classList);
        }

        public static ProcessResponse<CurriculumStudyTopicDto> GetStudyTopicById(int studyTopicId,
            MyPortalDbContext context)
        {
            var studyTopic = context.CurriculumStudyTopics.SingleOrDefault(x => x.Id == studyTopicId);

            if (studyTopic == null)
            {
                return new ProcessResponse<CurriculumStudyTopicDto>(ResponseType.NotFound, "Study topic not found", null);
            }

            return new ProcessResponse<CurriculumStudyTopicDto>(ResponseType.Ok, null, Mapper.Map<CurriculumStudyTopic, CurriculumStudyTopicDto>(studyTopic));
        }

        public static ProcessResponse<CurriculumSubjectDto> GetSubjectById(int subjectId, MyPortalDbContext context)
        {
            var subject = context.CurriculumSubjects.SingleOrDefault(x => x.Id == subjectId);

            if (subject == null)
            {
                return new ProcessResponse<CurriculumSubjectDto>(ResponseType.NotFound, "Subject not found", null);
            }

            return new ProcessResponse<CurriculumSubjectDto>(ResponseType.Ok, null,
                Mapper.Map<CurriculumSubject, CurriculumSubjectDto>(subject));
        }

        public static ProcessResponse<string> GetSubjectNameForClass(CurriculumClass @class)
        {
            if (@class == null)
            {
                return new ProcessResponse<string>(ResponseType.NotFound, "Class not found", null);
            }

            if (@class.SubjectId == null || @class.SubjectId == 0)
            {
                return new ProcessResponse<string>(ResponseType.Ok, null, "No Subject");
            }

            return new ProcessResponse<string>(ResponseType.Ok, null, @class.CurriculumSubject.Name);
        }

        public static async Task<bool> HasEnrolments(int classId, MyPortalDbContext context)
        {
            return await context.CurriculumEnrolments.AnyAsync(x => x.ClassId == classId);
        }

        public static async Task<bool> HasSessions(int classId, MyPortalDbContext context)
        {
            return await context.CurriculumSessions.AnyAsync(x => x.ClassId == classId);
        }

        public static ProcessResponse<bool> IsInAcademicYear(this DateTime date, MyPortalDbContext context, int academicYearId)
        {
            var academicYear = context.CurriculumAcademicYears.SingleOrDefault(x => x.Id == academicYearId);

            if (academicYear == null)
            {
                return new ProcessResponse<bool>(ResponseType.NotFound, "Academic year not found", false);
            }

            if (date >= academicYear.FirstDate && date <= academicYear.LastDate)
            {
                return new ProcessResponse<bool>(ResponseType.Ok, null, true);
            }

            return new ProcessResponse<bool>(ResponseType.Ok, null, false);
        }

        public static ProcessResponse<bool> PeriodIsFree(MyPortalDbContext context, Student student, int periodId)
        {
            bool isFree = !context.CurriculumEnrolments.Any(x =>
                x.StudentId == student.Id && x.Class.Sessions.Any(p => p.PeriodId == periodId));

            return new ProcessResponse<bool>(ResponseType.Ok, null, isFree);
        }

        public static ProcessResponse<bool> StudentCanEnrol(MyPortalDbContext context, int studentId, int classId)
        {
            var student = context.Students.SingleOrDefault(x => x.Id == studentId);

            var currClass = context.CurriculumClasses.SingleOrDefault(x => x.Id == classId);

            var alreadyEnrolled = context.CurriculumEnrolments.SingleOrDefault(x =>
                x.ClassId == classId && x.StudentId == studentId) != null;

            if (student == null)
            {
                return new ProcessResponse<bool>(ResponseType.NotFound, "Student not found", false);
            }

            if (currClass == null)
            {
                return new ProcessResponse<bool>(ResponseType.NotFound, "Class not found", false);
            }

            if (alreadyEnrolled)
            {
                return new ProcessResponse<bool>(ResponseType.BadRequest, student.Person.LastName + ", " + student.Person.FirstName + " is already enrolled in " + currClass.Name, false);
            }

            var periods = GetPeriodsForClass(context, currClass.Id);

            if (periods.ResponseType == ResponseType.Ok)
            {
                foreach (var period in periods.ResponseObject)
                {
                    if (!PeriodIsFree(context, student, period.Id).ResponseObject)
                    {
                        return new ProcessResponse<bool>(ResponseType.BadRequest, student.Person.LastName + ", " + student.Person.FirstName + " is not free during period " + period.Name, false);
                    }
                }
            }

            return new ProcessResponse<bool>(ResponseType.Ok, null, true);
        }
        public static ProcessResponse<object> UpdateClass(CurriculumClass @class, MyPortalDbContext context)
        {
            var classInDb = context.CurriculumClasses.SingleOrDefault(x => x.Id == @class.Id);

            if (classInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Class not found", null);
            }

            classInDb.Name = @class.Name;
            classInDb.SubjectId = @class.SubjectId;
            classInDb.TeacherId = @class.TeacherId;
            classInDb.YearGroupId = @class.YearGroupId;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Class updated", null);
        }
        public static ProcessResponse<object> UpdateLessonPlan(CurriculumLessonPlan lessonPlan,
            MyPortalDbContext context)
        {
            var planInDb = context.CurriculumLessonPlans.SingleOrDefault(x => x.Id == lessonPlan.Id);

            if (planInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Lesson plan not found", null);
            }

            planInDb.Title = lessonPlan.Title;
            planInDb.PlanContent = lessonPlan.PlanContent;
            planInDb.StudyTopicId = lessonPlan.StudyTopicId;
            planInDb.LearningObjectives = lessonPlan.LearningObjectives;
            planInDb.Homework = lessonPlan.Homework;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Lesson plan updated", null);
        }

        public static ProcessResponse<object> UpdateSession(CurriculumSession session, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(session))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            var sessionInDb = context.CurriculumSessions.SingleOrDefault(x => x.Id == session.Id);

            var currClass = context.CurriculumClasses.SingleOrDefault(x => x.Id == session.ClassId);

            if (sessionInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Session not found", null);
            }

            if (currClass.HasEnrolments().ResponseObject)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Cannot modify class schedule while students are enrolled", null);
            }

            if (context.CurriculumSessions.Any(x =>
                x.ClassId == session.ClassId && x.PeriodId == session.PeriodId))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Class already assigned to this period", null);
            }

            sessionInDb.PeriodId = session.PeriodId;
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Session updated", null);
        }
        public static ProcessResponse<object> UpdateStudyTopic(CurriculumStudyTopic studyTopic,
            MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(studyTopic))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            var studyTopicInDb = context.CurriculumStudyTopics.SingleOrDefault(x => x.Id == studyTopic.Id);

            if (studyTopicInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Study topic not found", null);
            }

            studyTopicInDb.Name = studyTopic.Name;
            studyTopicInDb.SubjectId = studyTopic.SubjectId;
            studyTopicInDb.YearGroupId = studyTopic.YearGroupId;

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Study topic updated", null);
        }

        public static ProcessResponse<object> UpdateSubject(CurriculumSubject subject, MyPortalDbContext context)
        {
            var subjectInDb = context.CurriculumSubjects.SingleOrDefault(x => x.Id == subject.Id);

            if (subjectInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Subject not found", null);
            }

            subjectInDb.Name = subject.Name;
            subjectInDb.LeaderId = subject.LeaderId;
            context.SaveChanges();
            return new ProcessResponse<object>(ResponseType.Ok, "Subject updated", null);
        }
    }
}