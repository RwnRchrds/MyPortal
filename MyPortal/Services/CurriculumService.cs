using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Ajax.Utilities;
using MyPortal.Dtos;
using MyPortal.Dtos.GridDtos;
using MyPortal.Exceptions;
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
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            if (!await _unitOfWork.CurriculumAcademicYears.AnyAsync(x => x.Id == @class.AcademicYearId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Academic year not found");
            }

            if (await _unitOfWork.CurriculumClasses.AnyAsync(x => x.Name == @class.Name && x.AcademicYearId == @class.AcademicYearId))
            {
                throw new ProcessException(ExceptionType.BadRequest, "Class already exists");
            }

            _unitOfWork.CurriculumClasses.Add(@class);

            await _unitOfWork.Complete();
        }

        public async Task CreateEnrolment(CurriculumEnrolment enrolment,  bool commitImmediately = true)
        {
            if (!ValidationService.ModelIsValid(enrolment))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            if (!await _unitOfWork.CurriculumClasses.AnyAsync(x => x.Id == enrolment.ClassId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            if (!await _unitOfWork.Students.AnyAsync(x => x.Id == enrolment.StudentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            if (!await _unitOfWork.CurriculumSessions.AnyAsync(x => x.ClassId == enrolment.ClassId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Cannot add students to a class with no sessions");
            }

            if (await _unitOfWork.CurriculumEnrolments.AnyAsync(x =>
                x.ClassId == enrolment.ClassId && x.StudentId == enrolment.StudentId))
            {
                throw new ProcessException(ExceptionType.BadRequest,
                    $"{enrolment.Student.GetDisplayName()} is already enrolled in {enrolment.Class.Name}");
            }

            if (await StudentCanEnrol(_unitOfWork, enrolment.StudentId, enrolment.ClassId))
            {
                _unitOfWork.CurriculumEnrolments.Add(enrolment);
                if (commitImmediately)
                {
                    await _unitOfWork.Complete();
                }
            }

            throw new ProcessException(ExceptionType.BadRequest,"An unknown error occurred");
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

            await _unitOfWork.Complete();
        }

        public async Task CreateEnrolmentsForRegGroup(GroupEnrolment enrolment)
        {
            var group = await _unitOfWork.PastoralRegGroups.GetByIdAsync(enrolment.GroupId);

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

                await CreateEnrolment(studentEnrolment, false);
            }

            await _unitOfWork.Complete();
        }

        public async Task CreateLessonPlan(CurriculumLessonPlan lessonPlan, string userId)
        {
            if (!ValidationService.ModelIsValid(lessonPlan))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            var authorId = lessonPlan.AuthorId;

            var author = new StaffMember();

            if (authorId == 0)
            {
                author = await _unitOfWork.StaffMembers.GetByUserIdAsync(userId);
                if (author == null)
                {
                    throw new ProcessException(ExceptionType.NotFound,"Staff member not found");
                }
            }

            if (authorId != 0)
            {
                author = await _unitOfWork.StaffMembers.GetByIdAsync(authorId);
            }

            if (author == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Staff member not found");
            }

            lessonPlan.AuthorId = author.Id;

            _unitOfWork.CurriculumLessonPlans.Add(lessonPlan);
            await _unitOfWork.Complete();
        }

        public async Task CreateSession(CurriculumSession session)
        {
            if (!ValidationService.ModelIsValid(session))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            if (!await _unitOfWork.CurriculumClasses.AnyAsync(x => x.Id == session.ClassId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            if (await HasEnrolments(session.ClassId))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot modify class schedule while students are enrolled");
            }

            if (await _unitOfWork.CurriculumSessions.AnyAsync(x =>
                x.ClassId == session.ClassId && x.PeriodId == session.PeriodId))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Class is already assigned to this period");
            }

            _unitOfWork.CurriculumSessions.Add(session);
            await _unitOfWork.Complete();
        }

        public async Task CreateSessionForRegPeriods(CurriculumSession session)
        {
            session.PeriodId = 0;

            var regPeriods = await _unitOfWork.AttendancePeriods.GetRegPeriods();

            if (! await _unitOfWork.CurriculumClasses.AnyAsync(x => x.Id == session.ClassId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            if (await HasEnrolments(session.ClassId, _unitOfWork))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot modify class schedule while students are enrolled");
            }

            foreach (var newAssignment in regPeriods.Select(period => new {period, period1 = period})
                .Where(@t =>
                    !_unitOfWork.CurriculumSessions.Any(x => x.ClassId == session.ClassId && x.PeriodId == @t.period1.Id))
                .Select(@t => new CurriculumSession {PeriodId = @t.period.Id, ClassId = session.ClassId}))
            {
                _unitOfWork.CurriculumSessions.Add(newAssignment);
            }

            await _unitOfWork.Complete();

        }

        public async Task CreateStudyTopic(CurriculumStudyTopic studyTopic)
        {
            if (!ValidationService.ModelIsValid(studyTopic))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            _unitOfWork.CurriculumStudyTopics.Add(studyTopic);
            await _unitOfWork.Complete();
        }

        public async Task CreateSubject(CurriculumSubject subject)
        {
            if (subject.Name.IsNullOrWhiteSpace() || !ValidationService.ModelIsValid(subject))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            _unitOfWork.CurriculumSubjects.Add(subject);
            await _unitOfWork.Complete();
        }

        public async Task DeleteClass(int classId)
        {
            var currClass = await _unitOfWork.CurriculumClasses.GetByIdAsync(classId);

            if (currClass == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            if (await HasSessions(classId) || await HasEnrolments(classId))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Class cannot be deleted");
            }

            _unitOfWork.CurriculumClasses.Remove(currClass);
            await _unitOfWork.Complete();
        }

        public async Task DeleteEnrolment(int enrolmentId)
        {
            var enrolment = await _unitOfWork.CurriculumEnrolments.GetByIdAsync(enrolmentId);

            if (enrolment == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Enrolment not found");
            }

            _unitOfWork.CurriculumEnrolments.Remove(enrolment);
            await _unitOfWork.Complete();
        }

        public async Task DeleteLessonPlan(int lessonPlanId, string userId, bool canDeleteAll)
        {
            var plan = await _unitOfWork.CurriculumLessonPlans.GetByIdAsync(lessonPlanId);

            if (plan == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Lesson plan not found");
            }

            if (!canDeleteAll && plan.Author.Person.UserId != userId)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot delete someone else's lesson plan");
            }

            _unitOfWork.CurriculumLessonPlans.Remove(plan);
            await _unitOfWork.Complete();
        }

        public async Task DeleteSession(int sessionId)
        {
            var sessionInDb = await _unitOfWork.CurriculumSessions.GetByIdAsync(sessionId);

            if (sessionInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Session not found");
            }

            _unitOfWork.CurriculumSessions.Remove(sessionInDb);
            await _unitOfWork.Complete();
        }

        public async Task DeleteStudyTopic(int studyTopicId)
        {
            var studyTopic = await _unitOfWork.CurriculumStudyTopics.GetByIdAsync(studyTopicId);

            if (studyTopic == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Study topic not found");
            }

            if (!studyTopic.LessonPlans.Any())
            {
                _unitOfWork.CurriculumStudyTopics.Remove(studyTopic);
                await _unitOfWork.Complete();
            }

            throw new ProcessException(ExceptionType.BadRequest,"This study topic cannot be deleted");
        }

        public async Task DeleteSubject(int subjectId)
        {
            var subjectInDb = await _unitOfWork.CurriculumSubjects.GetByIdAsync(subjectId);

            if (subjectInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            subjectInDb.Deleted = true; //Flag as deleted

            //Delete from database
            if (await _unitOfWork.CurriculumClasses.AnyAsync(x => x.SubjectId == subjectId) ||
                await _unitOfWork.CurriculumStudyTopics.AnyAsync(x => x.SubjectId == subjectId))
            {
                throw new ProcessException(ExceptionType.BadRequest,"This subject cannot be deleted");
            }

            _unitOfWork.CurriculumSubjects.Remove(subjectInDb);

            await _unitOfWork.Complete();
        }

        public async Task<CurriculumAcademicYearDto> GetAcademicYearById(int academicYearId)
        {
            var academicYear = await _unitOfWork.CurriculumAcademicYears.GetByIdAsync(academicYearId);

            if (academicYear == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Academic year not found");
            }

            return Mapper.Map<CurriculumAcademicYear, CurriculumAcademicYearDto>(academicYear);
        }

        public async Task<IEnumerable<CurriculumAcademicYearDto>> GetAcademicYears()
        {
            var academicYears = await _unitOfWork.CurriculumAcademicYears.GetAllAsync();
                
            return academicYears.Select(Mapper.Map<CurriculumAcademicYear, CurriculumAcademicYearDto>);
        }

        public async Task<IEnumerable<CurriculumAcademicYear>> GetAcademicYearsModel()
        {
            var academicYears = await _unitOfWork.CurriculumAcademicYears.GetAllAsync();

            return academicYears;
        }

        public async Task<IEnumerable<CurriculumClassDto>> GetAllClasses(int academicYearId)
        {
            var classes = await _unitOfWork.CurriculumClasses.GetByAcademicYear(academicYearId);

            return classes.Select(Mapper.Map<CurriculumClass, CurriculumClassDto>);
        }

        public async Task<IEnumerable<GridCurriculumClassDto>> GetAllClassesDataGrid(int academicYearId)
        {
            var classes = await _unitOfWork.CurriculumClasses.GetByAcademicYear(academicYearId);

            return classes.Select(Mapper.Map<CurriculumClass, GridCurriculumClassDto>);
        }

        public async Task<IEnumerable<CurriculumClass>> GetAllClassesModel(int academicYearId)
        {
            return await _unitOfWork.CurriculumClasses.GetByAcademicYear(academicYearId);
        }

        public async Task<IEnumerable<CurriculumLessonPlanDto>> GetAllLessonPlans()
        {
            var lessonPlans = await _unitOfWork.CurriculumLessonPlans.GetAllAsync();

            return lessonPlans.Select(Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>);
        }

        public async Task<IEnumerable<CurriculumStudyTopicDto>> GetAllStudyTopicsDto()
        {
            var studyTopics = await _unitOfWork.CurriculumStudyTopics.GetAllAsync();

            return studyTopics.Select(Mapper.Map<CurriculumStudyTopic, CurriculumStudyTopicDto>);
        }

        public async Task<IEnumerable<GridCurriculumStudyTopicDto>> GetAllStudyTopicsDataGrid()
        {
            var studyTopics = await _unitOfWork.CurriculumStudyTopics.GetAllAsync();

            return studyTopics.Select(Mapper.Map<CurriculumStudyTopic, GridCurriculumStudyTopicDto>);
        }

        public async Task<IEnumerable<CurriculumStudyTopic>> GetAllStudyTopics()
        {
            return await _unitOfWork.CurriculumStudyTopics.GetAllAsync();
        }

        public async Task<IEnumerable<CurriculumSubjectDto>> GetAllSubjectsDto()
        {
            var subjects = await _unitOfWork.CurriculumSubjects.GetAllAsync();

            return subjects.Select(Mapper.Map<CurriculumSubject, CurriculumSubjectDto>);
        }

        public async Task<IDictionary<int, string>> GetAllSubjectsLookup()
        {
            var subjects = await _unitOfWork.CurriculumSubjects.GetAllAsync();

            return subjects.ToDictionary(x => x.Id, x => x.Name);
        }

        public async Task<IEnumerable<GridCurriculumSubjectDto>> GetAllSubjectsDataGrid()
        {
            var subjects = await _unitOfWork.CurriculumSubjects.GetAllAsync();

            return subjects.Select(Mapper.Map<CurriculumSubject, GridCurriculumSubjectDto>);
        }

        public async Task<IEnumerable<CurriculumSubject>> GetAllSubjects()
        {
            var subjects = await _unitOfWork.CurriculumSubjects.GetAllAsync();

            return subjects;
        }

        public async Task<CurriculumClassDto> GetClassById(int classId)
        {
            var currClass = await _unitOfWork.CurriculumClasses.GetByIdAsync(classId);

            if (currClass == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            return Mapper.Map<CurriculumClass, CurriculumClassDto>(currClass);
        }

        public async Task<CurriculumEnrolmentDto> GetEnrolmentById(int enrolmentId)
        {
            var enrolment = await _unitOfWork.CurriculumEnrolments.GetByIdAsync(enrolmentId);

            if (enrolment == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Enrolment not found");
            }

            return Mapper.Map<CurriculumEnrolment, CurriculumEnrolmentDto>(enrolment);
        }

        public async Task<IEnumerable<CurriculumEnrolmentDto>> GetEnrolmentsForClassDto(int classId)
        {
            var list = await _unitOfWork.CurriculumEnrolments.GetEnrolmentsByClass(classId);

            return list.Select(Mapper.Map<CurriculumEnrolment, CurriculumEnrolmentDto>);
        }

        public async Task<IEnumerable<GridCurriculumEnrolmentDto>> GetEnrolmentsForClassDataGrid(int classId)
        {
            var list = await _unitOfWork.CurriculumEnrolments.GetEnrolmentsByClass(classId);

            return list.Select(Mapper.Map<CurriculumEnrolment, GridCurriculumEnrolmentDto>);
        }

        public async Task<IEnumerable<CurriculumEnrolment>> GetEnrolmentsForClass(int classId)
        {
            var list = await _unitOfWork.CurriculumEnrolments.GetEnrolmentsByClass(classId);

            return list;
        }

        public async Task<IEnumerable<CurriculumEnrolmentDto>> GetEnrolmentsForStudentDto(int studentId)
        {
            var list = await _unitOfWork.CurriculumEnrolments.GetEnrolmentsByStudent(studentId);

            return list.Select(Mapper.Map<CurriculumEnrolment, CurriculumEnrolmentDto>);
        }

        public async Task<IEnumerable<GridCurriculumEnrolmentDto>> GetEnrolmentsForStudentDataGrid(int studentId)
        {
            var list = await _unitOfWork.CurriculumEnrolments.GetEnrolmentsByStudent(studentId);

            return list.Select(Mapper.Map<CurriculumEnrolment, GridCurriculumEnrolmentDto>);
        }

        public async Task<IEnumerable<CurriculumEnrolment>> GetEnrolmentsForStudent(int studentId)
        {
            var list = await _unitOfWork.CurriculumEnrolments.GetEnrolmentsByStudent(studentId);

            return list;
        }

        public async Task<CurriculumLessonPlanDto> GetLessonPlanById(int lessonPlanId)
        {
            var lessonPlan = await _unitOfWork.CurriculumLessonPlans.GetByIdAsync(lessonPlanId);

            if (lessonPlan == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Lesson plan not found");
            }

            return Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>(lessonPlan);
        }

        public async Task<IEnumerable<CurriculumLessonPlanDto>> GetLessonPlansByStudyTopicDto(int studyTopicId)
        {
            var lessonPlans = await _unitOfWork.CurriculumLessonPlans.GetLessonPlansByStudyTopic(studyTopicId);

            return lessonPlans.Select(Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>);
        }

        public async Task<IEnumerable<GridCurriculumLessonPlanDto>> GetLessonPlansByStudyTopicDataGrid(int studyTopicId)
        {
            var lessonPlans = await _unitOfWork.CurriculumLessonPlans.GetLessonPlansByStudyTopic(studyTopicId);

            return lessonPlans.Select(Mapper.Map<CurriculumLessonPlan, GridCurriculumLessonPlanDto>);
        }

        public async Task<IEnumerable<CurriculumLessonPlan>> GetLessonPlansByStudyTopic(int studyTopicId)
        {
            return await _unitOfWork.CurriculumLessonPlans.GetLessonPlansByStudyTopic(studyTopicId);
        }

        

        public async Task<CurriculumSessionDto> GetSessionById(int sessionId)
        {
            var session = await _unitOfWork.CurriculumSessions.GetByIdAsync(sessionId);

            if (session == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Session not found");
            }

            return Mapper.Map<CurriculumSession, CurriculumSessionDto>(session);
        }

        public async Task<IEnumerable<CurriculumSessionDto>> GetSessionsByTeacherOnDayOfWeek(int staffId, int academicYearId, DateTime date)
        {
            var classes = await GetSessionsByTeacherOnDayOfWeekModel(staffId, academicYearId, date);

            return classes.Select(Mapper.Map<CurriculumSession, CurriculumSessionDto>);
        }

        public async Task<IEnumerable<GridCurriculumSessionDto>> GetSessionsByTeacherOnDayOfWeekDataGrid(int staffId, int academicYearId, DateTime date,
            MyPortalDb_unitOfWork _unitOfWork)
        {
            var classes = await GetSessionsByTeacherOnDayOfWeekModel(staffId, academicYearId, date, _unitOfWork);

            return classes.Select(Mapper.Map<CurriculumSession, GridCurriculumSessionDto>);
        }

        public async Task<IEnumerable<CurriculumSessionDto>> GetSessionsByClass(int classId,
            MyPortalDb_unitOfWork _unitOfWork)
        {
            var sessions = await GetSessionsForClassModel(classId, _unitOfWork);

            return sessions.Select(Mapper.Map<CurriculumSession, CurriculumSessionDto>);
        }

        public async Task<IEnumerable<GridCurriculumSessionDto>> GetSessionsForClass_DataGrid(int classId,
            MyPortalDb_unitOfWork _unitOfWork)
        {
            var sessions = await GetSessionsForClassModel(classId, _unitOfWork);

            return sessions.Select(Mapper.Map<CurriculumSession, GridCurriculumSessionDto>);
        }

        public async Task<IEnumerable<CurriculumSession>> GetSessionsForClassModel(int classId,
            MyPortalDb_unitOfWork _unitOfWork)
        {
            return await _unitOfWork.CurriculumSessions.Where(x => x.ClassId == classId).ToListAsync();
        }

        public async Task<IEnumerable<CurriculumSession>> GetSessionsByTeacherOnDayOfWeekModel(int staffId, int academicYearId, DateTime date,
            MyPortalDb_unitOfWork _unitOfWork)
        {
            var weekBeginning = date.StartOfWeek();

            var academicYear = await _unitOfWork.CurriculumAcademicYears.SingleOrDefaultAsync(x => x.Id == academicYearId);

            if (academicYear == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Academic year not found");
            }

            if (weekBeginning < academicYear.FirstDate || weekBeginning > academicYear.LastDate)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Selected date is outside academic year");
            }

            var currentWeek = await _unitOfWork.AttendanceWeeks.SingleOrDefaultAsync(x => x.Beginning == weekBeginning && x.AcademicYearId == academicYearId);

            if (currentWeek == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Attendance week not found");
            }

            if (currentWeek.IsHoliday)
            {
                throw new ProcessException(ExceptionType.BadRequest,"Selected date is during a school holiday");
            }

            var classList = await _unitOfWork.CurriculumSessions
                .Where(x =>
                    x.Class.AcademicYearId == academicYearId && x.Period.Weekday ==
                    date.DayOfWeek && x.Class.TeacherId == staffId)
                .OrderBy(x => x.Period.StartTime)
                .ToListAsync();

            return classList;
        }

        public async Task<CurriculumStudyTopicDto> GetStudyTopicById(int studyTopicId,
            MyPortalDb_unitOfWork _unitOfWork)
        {
            var studyTopic = await _unitOfWork.CurriculumStudyTopics.SingleOrDefaultAsync(x => x.Id == studyTopicId);

            if (studyTopic == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Study topic not found");
            }

            return Mapper.Map<CurriculumStudyTopic, CurriculumStudyTopicDto>(studyTopic);
        }

        public async Task<CurriculumSubjectDto> GetSubjectById(int subjectId)
        {
            var subject = await _unitOfWork.CurriculumSubjects.SingleOrDefaultAsync(x => x.Id == subjectId);

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

            return @class.Subject.Name;
        }

        public async Task<bool> HasEnrolments(int classId)
        {
            return await _unitOfWork.CurriculumEnrolments.AnyAsync(x => x.ClassId == classId);
        }

        public async Task<bool> HasSessions(int classId)
        {
            return await _unitOfWork.CurriculumSessions.AnyAsync(x => x.ClassId == classId);
        }

        public async Task<bool> IsInAcademicYear(this DateTime date,  int academicYearId)
        {
            var academicYear = await _unitOfWork.CurriculumAcademicYears.SingleOrDefaultAsync(x => x.Id == academicYearId);

            if (academicYear == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Academic year not found");
            }

            return date >= academicYear.FirstDate && date <= academicYear.LastDate;
        }

        public async Task<bool> PeriodIsFree( int studentId, int periodId)
        {
            return !await _unitOfWork.CurriculumEnrolments.AnyAsync(x =>
                x.StudentId == studentId && x.Class.Sessions.Any(p => p.PeriodId == periodId));
        }

        public async Task<bool> StudentCanEnrol(int studentId, int classId)
        {
            if (!await _unitOfWork.Students.AnyAsync(x => x.Id == studentId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Student not found");
            }

            if (!await _unitOfWork.CurriculumClasses.AnyAsync(x => x.Id == classId))
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            if (await _unitOfWork.CurriculumEnrolments.AnyAsync(x =>
                x.ClassId == classId && x.StudentId == studentId))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Student is already enrolled in class");
            }

            var periods = await GetPeriodsByClass(_unitOfWork, classId);
            
                foreach (var period in periods)
                {
                    if (!await PeriodIsFree(_unitOfWork, studentId, period.Id))
                    {
                        throw new ProcessException(ExceptionType.BadRequest,$"Student is not free during period {period.Name}");
                    }
                }

                return true;
        }
        public async Task UpdateClass(CurriculumClass @class)
        {
            var classInDb = await _unitOfWork.CurriculumClasses.SingleOrDefaultAsync(x => x.Id == @class.Id);

            if (classInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Class not found");
            }

            classInDb.Name = @class.Name;
            classInDb.SubjectId = @class.SubjectId;
            classInDb.TeacherId = @class.TeacherId;
            classInDb.YearGroupId = @class.YearGroupId;

            await _unitOfWork.Complete();
        }
        public async Task UpdateLessonPlan(CurriculumLessonPlan lessonPlan,
            MyPortalDb_unitOfWork _unitOfWork)
        {
            var planInDb = await _unitOfWork.CurriculumLessonPlans.SingleOrDefaultAsync(x => x.Id == lessonPlan.Id);

            if (planInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Lesson plan not found");
            }

            planInDb.Title = lessonPlan.Title;
            planInDb.PlanContent = lessonPlan.PlanContent;
            planInDb.StudyTopicId = lessonPlan.StudyTopicId;
            planInDb.LearningObjectives = lessonPlan.LearningObjectives;
            planInDb.Homework = lessonPlan.Homework;

            await _unitOfWork.Complete();
        }

        public async Task UpdateSession(CurriculumSession session)
        {
            if (!ValidationService.ModelIsValid(session))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            var sessionInDb = await _unitOfWork.CurriculumSessions.SingleOrDefaultAsync(x => x.Id == session.Id);

            if (sessionInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Session not found");
            }

            if (await HasEnrolments(session.ClassId, _unitOfWork))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Cannot modify class schedule while students are enrolled");
            }

            if (await _unitOfWork.CurriculumSessions.AnyAsync(x =>
                x.ClassId == session.ClassId && x.PeriodId == session.PeriodId))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Class already assigned to this period");
            }

            sessionInDb.PeriodId = session.PeriodId;
            await _unitOfWork.Complete();
        }
        public async Task UpdateStudyTopic(CurriculumStudyTopic studyTopic,
            MyPortalDb_unitOfWork _unitOfWork)
        {
            if (!ValidationService.ModelIsValid(studyTopic))
            {
                throw new ProcessException(ExceptionType.BadRequest,"Invalid data");
            }

            var studyTopicInDb = await _unitOfWork.CurriculumStudyTopics.SingleOrDefaultAsync(x => x.Id == studyTopic.Id);

            if (studyTopicInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Study topic not found");
            }

            studyTopicInDb.Name = studyTopic.Name;
            studyTopicInDb.SubjectId = studyTopic.SubjectId;
            studyTopicInDb.YearGroupId = studyTopic.YearGroupId;

            await _unitOfWork.Complete();
        }

        public async Task UpdateSubject(CurriculumSubject subject)
        {
            var subjectInDb = await _unitOfWork.CurriculumSubjects.SingleOrDefaultAsync(x => x.Id == subject.Id);

            if (subjectInDb == null)
            {
                throw new ProcessException(ExceptionType.NotFound,"Subject not found");
            }

            subjectInDb.Name = subject.Name;
            subjectInDb.LeaderId = subject.LeaderId;
            await _unitOfWork.Complete();
        }
    }
}