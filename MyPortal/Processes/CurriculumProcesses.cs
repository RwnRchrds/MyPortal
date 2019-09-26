using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
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
        public static ProcessResponse<object> CreateClass(CurriculumClass @class, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(@class))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            if (!context.CurriculumAcademicYears.Any(x => x.Id == @class.AcademicYearId))
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Academic year not found", null);
            }

            if (context.CurriculumClasses.Any(x => x.Name == @class.Name && x.AcademicYearId == @class.AcademicYearId))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Class already exists", null);
            }

            context.CurriculumClasses.Add(@class);

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Class created", null);
        }

        public static ProcessResponse<object> CreateEnrolment(CurriculumEnrolment enrolment, MyPortalDbContext context, bool commitImmediately = true)
        {
            if (!ValidationProcesses.ModelIsValid(enrolment))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            if (!context.CurriculumClasses.Any(x => x.Id == enrolment.ClassId))
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Class not found", null);
            }

            if (!context.Students.Any(x => x.Id == enrolment.StudentId))
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Student not found", null);
            }

            if (!context.CurriculumSessions.Any(x => x.ClassId == enrolment.ClassId))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Cannot add students to a class with no sessions", null);
            }

            if (context.CurriculumEnrolments.Any(x =>
                x.ClassId == enrolment.ClassId && x.StudentId == enrolment.StudentId))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest,
                    $"{PeopleProcesses.GetStudentDisplayName(enrolment.Student).ResponseObject} is already enrolled in {enrolment.CurriculumClass.Name}",
                    null);
            }

            var canEnroll = StudentCanEnrol(context, enrolment.StudentId, enrolment.ClassId);

            if (canEnroll.ResponseType == ResponseType.BadRequest)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, canEnroll.ResponseMessage, null);
            }

            if (canEnroll.ResponseType == ResponseType.NotFound)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, canEnroll.ResponseMessage, null);
            }

            if (canEnroll.ResponseObject)
            {
                context.CurriculumEnrolments.Add(enrolment);
                if (commitImmediately)
                {
                    context.SaveChanges();
                }

                return new ProcessResponse<object>(ResponseType.Ok,
                    $"{PeopleProcesses.GetStudentDisplayName(enrolment.Student).ResponseObject} has been enrolled in {enrolment.CurriculumClass.Name}",
                    null);
            }

            return new ProcessResponse<object>(ResponseType.BadRequest, "An unknown error has occurred", null);
        }

        public static ProcessResponse<object> CreateEnrolmentsForMultipleStudents(IEnumerable<Student> students,
            int classId, MyPortalDbContext context)
        {
            foreach (var student in students)
            {
                var studentEnrolment = new CurriculumEnrolment
                {
                    ClassId = classId,
                    StudentId = student.Id
                };

                CreateEnrolment(studentEnrolment, context, false);
            }

            context.SaveChanges();
            return new ProcessResponse<object>(ResponseType.Ok, "Group enrolled in class", null);
        }

        public static ProcessResponse<object> CreateEnrolmentsForRegGroup(GroupEnrolment enrolment,
            MyPortalDbContext context)
        {
            var group = context.PastoralRegGroups.SingleOrDefault(x => x.Id == enrolment.GroupId);

            if (group == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Group not found", null);
            }

            foreach (var student in group.Students)
            {
                var studentEnrolment = new CurriculumEnrolment
                {
                    ClassId = enrolment.ClassId,
                    StudentId = student.Id
                };

                CreateEnrolment(studentEnrolment, context, false);
            }

            context.SaveChanges();
            return new ProcessResponse<object>(ResponseType.Ok, "Group enrolled in class", null);
        }

        public static ProcessResponse<object> CreateLessonPlan(CurriculumLessonPlan lessonPlan, string userId,
            MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(lessonPlan))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            var authorId = lessonPlan.AuthorId;

            var author = new StaffMember();

            if (authorId == 0)
            {
                author = context.StaffMembers.SingleOrDefault(x => x.Person.UserId == userId);
                if (author == null)
                {
                    return new ProcessResponse<object>(ResponseType.NotFound, "Staff member not found", null);
                }
            }

            if (authorId != 0)
            {
                author = context.StaffMembers.SingleOrDefault(x => x.Id == authorId);
            }

            if (author == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Staff member not found", null);
            }

            lessonPlan.AuthorId = author.Id;

            context.CurriculumLessonPlans.Add(lessonPlan);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Lesson plan created", null);
        }

        public static ProcessResponse<object> CreateSession(CurriculumSession session, MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(session))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            var currClass = context.CurriculumClasses.SingleOrDefault(x => x.Id == session.ClassId);

            if (currClass == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Class not found", null);
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

            context.CurriculumSessions.Add(session);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Session created", null);
        }

        public static ProcessResponse<object> CreateSessionForRegPeriods(CurriculumSession session,
            MyPortalDbContext context)
        {
            session.PeriodId = 0;

            var regPeriods = context.AttendancePeriods.Where(x => x.IsAm || x.IsPm);

            var currClass = context.CurriculumClasses.SingleOrDefault(x => x.Id == session.ClassId);

            if (currClass == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Class not found", null);
            }

            if (currClass.HasEnrolments().ResponseObject)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Cannot modify class schedule while students are enrolled", null);
            }

            foreach (var period in regPeriods)
            {
                if (!context.CurriculumSessions.Any(x => x.ClassId == currClass.Id && x.PeriodId == period.Id))
                {
                    var newAssignment = new CurriculumSession { PeriodId = period.Id, ClassId = currClass.Id };

                    context.CurriculumSessions.Add(newAssignment);
                }
            }

            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "All reg sessions added", null);
        }

        public static ProcessResponse<object> CreateStudyTopic(CurriculumStudyTopic studyTopic,
            MyPortalDbContext context)
        {
            if (!ValidationProcesses.ModelIsValid(studyTopic))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            context.CurriculumStudyTopics.Add(studyTopic);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Study topic created", null);
        }

        public static ProcessResponse<object> CreateSubject(CurriculumSubject subject, MyPortalDbContext context)
        {
            if (subject.Name.IsNullOrWhiteSpace() || !ValidationProcesses.ModelIsValid(subject))
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Invalid data", null);
            }

            context.CurriculumSubjects.Add(subject);
            context.SaveChanges();
            return new ProcessResponse<object>(ResponseType.Ok, "Subject created", null);
        }

        public static ProcessResponse<object> DeleteClass(int classId, MyPortalDbContext context)
        {
            var currClass = context.CurriculumClasses.SingleOrDefault(x => x.Id == classId);

            if (currClass == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Class not found", null);
            }

            if (!currClass.HasPeriods().ResponseObject && !currClass.HasEnrolments().ResponseObject)
            {
                context.CurriculumClasses.Remove(currClass);
                context.SaveChanges();

                return new ProcessResponse<object>(ResponseType.Ok, "Class deleted", null);
            }

            return new ProcessResponse<object>(ResponseType.BadRequest, "Class cannot be deleted", null);
        }

        public static ProcessResponse<object> DeleteEnrolment(int enrolmentId, MyPortalDbContext context)
        {
            var enrolment = context.CurriculumEnrolments.SingleOrDefault(x => x.Id == enrolmentId);

            if (enrolment == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Enrolment not found", null);
            }

            context.CurriculumEnrolments.Remove(enrolment);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok,
                $"{PeopleProcesses.GetStudentDisplayName(enrolment.Student).ResponseObject} has been unenrolled from {enrolment.CurriculumClass.Name}",
                null);
        }

        public static ProcessResponse<object> DeleteLessonPlan(int lessonPlanId, int staffId, bool canDeleteAll, MyPortalDbContext context)
        {
            var plan = context.CurriculumLessonPlans.SingleOrDefault(x => x.Id == lessonPlanId);

            if (plan == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Lesson plan not found", null);
            }

            if (!canDeleteAll && plan.AuthorId != staffId)
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "Cannot delete someone else's lesson plan", null);
            }

            context.CurriculumLessonPlans.Remove(plan);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Lesson plan deleted", null);
        }

        public static ProcessResponse<object> DeleteSession(int sessionId, MyPortalDbContext context)
        {
            var assignmentInDb = context.CurriculumSessions.SingleOrDefault(x => x.Id == sessionId);

            if (assignmentInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Session not found", null);
            }

            context.CurriculumSessions.Remove(assignmentInDb);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Session deleted", null);
        }

        public static ProcessResponse<object> DeleteStudyTopic(int studyTopicId, MyPortalDbContext context)
        {
            var studyTopic = context.CurriculumStudyTopics.SingleOrDefault(x => x.Id == studyTopicId);

            if (studyTopic == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Study topic not found", null);
            }

            if (studyTopic.LessonPlans.Any())
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "This study topic cannot be deleted", null);
            }

            context.CurriculumStudyTopics.Remove(studyTopic);
            context.SaveChanges();

            return new ProcessResponse<object>(ResponseType.Ok, "Study topic deleted", null);
        }

        public static ProcessResponse<object> DeleteSubject(int subjectId, MyPortalDbContext context)
        {
            var subjectInDb = context.CurriculumSubjects.SingleOrDefault(x => x.Id == subjectId);

            if (subjectInDb == null)
            {
                return new ProcessResponse<object>(ResponseType.NotFound, "Subject not found", null);
            }

            subjectInDb.Deleted = true; //Flag as deleted

            //Delete from database
            if (subjectInDb.AssessmentResults.Any() || subjectInDb.CurriculumClasses.Any() ||
                subjectInDb.CurriculumStudyTopics.Any())
            {
                return new ProcessResponse<object>(ResponseType.BadRequest, "This subject cannot be deleted", null);
            }
            context.CurriculumSubjects.Remove(subjectInDb);

            context.SaveChanges();
            return new ProcessResponse<object>(ResponseType.Ok, "Subject deleted", null);
        }

        public static ProcessResponse<CurriculumAcademicYearDto> GetAcademicYearById(int academicYearId,
            MyPortalDbContext context)
        {
            var academicYear = context.CurriculumAcademicYears.SingleOrDefault(x => x.Id == academicYearId);

            if (academicYear == null)
            {
                return new ProcessResponse<CurriculumAcademicYearDto>(ResponseType.NotFound, "Academic year not found", null);
            }

            return new ProcessResponse<CurriculumAcademicYearDto>(ResponseType.Ok, null,
                Mapper.Map<CurriculumAcademicYear, CurriculumAcademicYearDto>(academicYear));
        }

        public static ProcessResponse<IEnumerable<CurriculumAcademicYearDto>> GetAcademicYears(
            MyPortalDbContext context)
        {
            var academicYears = GetAcademicYears_Model(context).ResponseObject
                .Select(Mapper.Map<CurriculumAcademicYear, CurriculumAcademicYearDto>);

            return new ProcessResponse<IEnumerable<CurriculumAcademicYearDto>>(ResponseType.Ok, null, academicYears);
        }

        public static ProcessResponse<IEnumerable<CurriculumAcademicYear>> GetAcademicYears_Model(
            MyPortalDbContext context)
        {
            var academicYears = context.CurriculumAcademicYears.ToList().OrderByDescending(x => x.FirstDate).ToList();

            return new ProcessResponse<IEnumerable<CurriculumAcademicYear>>(ResponseType.Ok, null, academicYears);
        }

        public static ProcessResponse<IEnumerable<CurriculumClassDto>> GetAllClasses(int academicYearId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<CurriculumClassDto>>(ResponseType.Ok, null,
                GetAllClasses_Model(academicYearId, context).ResponseObject
                    .OrderBy(x => x.Name).Select(Mapper.Map<CurriculumClass, CurriculumClassDto>));
        }

        public static ProcessResponse<IEnumerable<GridCurriculumClassDto>> GetAllClasses_DataGrid(int academicYearId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridCurriculumClassDto>>(ResponseType.Ok, null,
                GetAllClasses_Model(academicYearId, context).ResponseObject
                    .OrderBy(x => x.Name).Select(Mapper.Map<CurriculumClass, GridCurriculumClassDto>));
        }

        public static ProcessResponse<IEnumerable<CurriculumClass>> GetAllClasses_Model(int academicYearId,
            MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<CurriculumClass>>(ResponseType.Ok, null, context.CurriculumClasses
                .Where(x => x.AcademicYearId == academicYearId).ToList());
        }

        public static ProcessResponse<IEnumerable<CurriculumLessonPlanDto>> GetAllLessonPlans(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<CurriculumLessonPlanDto>>(ResponseType.Ok, null,
                context.CurriculumLessonPlans.OrderBy(x => x.Title).ToList()
                    .Select(Mapper.Map<CurriculumLessonPlan, CurriculumLessonPlanDto>));
        }

        public static ProcessResponse<IEnumerable<CurriculumStudyTopicDto>> GetAllStudyTopics(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<CurriculumStudyTopicDto>>(ResponseType.Ok, null,
                GetAllStudyTopics_Model(context).ResponseObject
                    .Select(Mapper.Map<CurriculumStudyTopic, CurriculumStudyTopicDto>));
        }

        public static ProcessResponse<IEnumerable<GridCurriculumStudyTopicDto>> GetAllStudyTopics_DataGrid(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridCurriculumStudyTopicDto>>(ResponseType.Ok, null,
                GetAllStudyTopics_Model(context).ResponseObject
                    .Select(Mapper.Map<CurriculumStudyTopic, GridCurriculumStudyTopicDto>));
        }

        public static ProcessResponse<IEnumerable<CurriculumStudyTopic>> GetAllStudyTopics_Model(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<CurriculumStudyTopic>>(ResponseType.Ok, null,
                context.CurriculumStudyTopics.ToList());
        }

        public static ProcessResponse<IEnumerable<CurriculumSubjectDto>> GetAllSubjects(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<CurriculumSubjectDto>>(ResponseType.Ok, null,
                GetAllSubjects_Model(context).ResponseObject
                    .Select(Mapper.Map<CurriculumSubject, CurriculumSubjectDto>));
        }

        public static ProcessResponse<IEnumerable<GridCurriculumSubjectDto>> GetAllSubjects_DataGrid(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<GridCurriculumSubjectDto>>(ResponseType.Ok, null,
                GetAllSubjects_Model(context).ResponseObject
                    .Select(Mapper.Map<CurriculumSubject, GridCurriculumSubjectDto>));
        }

        public static ProcessResponse<IEnumerable<CurriculumSubject>> GetAllSubjects_Model(MyPortalDbContext context)
        {
            return new ProcessResponse<IEnumerable<CurriculumSubject>>(ResponseType.Ok, null,
                context.CurriculumSubjects.Where(x => !x.Deleted).OrderBy(x => x.Name).ToList());
        }

        public static ProcessResponse<CurriculumClassDto> GetClassById(int classId, MyPortalDbContext context)
        {
            var currClass = context.CurriculumClasses.SingleOrDefault(x => x.Id == classId);

            if (currClass == null)
            {
                return new ProcessResponse<CurriculumClassDto>(ResponseType.NotFound, "Class not found", null);
            }

            return new ProcessResponse<CurriculumClassDto>(ResponseType.Ok, null,
                Mapper.Map<CurriculumClass, CurriculumClassDto>(currClass));
        }

        public static ProcessResponse<CurriculumEnrolmentDto> GetEnrolmentById(int enrolmentId,
            MyPortalDbContext context)
        {
            var enrolment = context.CurriculumEnrolments.SingleOrDefault(x => x.Id == enrolmentId);

            if (enrolment == null)
            {
                return new ProcessResponse<CurriculumEnrolmentDto>(ResponseType.NotFound, "Enrolment not found", null);
            }

            return new ProcessResponse<CurriculumEnrolmentDto>(ResponseType.Ok, null,
                Mapper.Map<CurriculumEnrolment, CurriculumEnrolmentDto>(enrolment));
        }

        public static ProcessResponse<IEnumerable<CurriculumEnrolmentDto>> GetEnrolmentsForClass(int classId,
            MyPortalDbContext context)
        {
            var list = GetEnrolmentsForClass_Model(classId, context).ResponseObject
                .OrderBy(x => x.Student.Person.LastName)
                .Select(Mapper.Map<CurriculumEnrolment, CurriculumEnrolmentDto>);

            return new ProcessResponse<IEnumerable<CurriculumEnrolmentDto>>(ResponseType.Ok, null, list);
        }

        public static ProcessResponse<IEnumerable<GridCurriculumEnrolmentDto>> GetEnrolmentsForClass_DataGrid(int classId,
            MyPortalDbContext context)
        {
            var list = GetEnrolmentsForClass_Model(classId, context).ResponseObject
                .OrderBy(x => x.Student.Person.LastName)
                .Select(Mapper.Map<CurriculumEnrolment, GridCurriculumEnrolmentDto>);

            return new ProcessResponse<IEnumerable<GridCurriculumEnrolmentDto>>(ResponseType.Ok, null, list);
        }

        public static ProcessResponse<IEnumerable<CurriculumEnrolment>> GetEnrolmentsForClass_Model(int classId,
            MyPortalDbContext context)
        {
            var list = context.CurriculumEnrolments.Where(x => x.ClassId == classId).ToList()
                .OrderBy(x => x.Student.Person.LastName);

            return new ProcessResponse<IEnumerable<CurriculumEnrolment>>(ResponseType.Ok, null, list);
        }

        public static ProcessResponse<IEnumerable<CurriculumEnrolmentDto>> GetEnrolmentsForStudent(int studentId,
            MyPortalDbContext context)
        {
            var list = GetEnrolmentsForStudent_Model(studentId, context).ResponseObject
                .OrderBy(x => x.Student.Person.LastName)
                .Select(Mapper.Map<CurriculumEnrolment, CurriculumEnrolmentDto>);

            return new ProcessResponse<IEnumerable<CurriculumEnrolmentDto>>(ResponseType.Ok, null, list);
        }

        public static ProcessResponse<IEnumerable<GridCurriculumEnrolmentDto>> GetEnrolmentsForStudent_DataGrid(int studentId,
            MyPortalDbContext context)
        {
            var list = GetEnrolmentsForStudent_Model(studentId, context).ResponseObject
                .OrderBy(x => x.Student.Person.LastName)
                .Select(Mapper.Map<CurriculumEnrolment, GridCurriculumEnrolmentDto>);

            return new ProcessResponse<IEnumerable<GridCurriculumEnrolmentDto>>(ResponseType.Ok, null, list);
        }

        public static ProcessResponse<IEnumerable<CurriculumEnrolment>> GetEnrolmentsForStudent_Model(int studentId,
            MyPortalDbContext context)
        {
            var list = context.CurriculumEnrolments.Where(x => x.StudentId == studentId).ToList()
                .OrderBy(x => x.Student.Person.LastName);

            return new ProcessResponse<IEnumerable<CurriculumEnrolment>>(ResponseType.Ok, null, list);
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
                context.CurriculumLessonPlans.Where(x => x.StudyTopicId == studyTopicId).OrderBy(x => x.Title)
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
                context.AttendancePeriods.Where(x => x.CurriculumClassPeriods.Any(p => p.ClassId == classId)).ToList());
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
                    x.CurriculumClass.AcademicYearId == academicYearId && x.AttendancePeriod.Weekday ==
                    date.DayOfWeek
                        .ToString().Substring(0, 3) && x.CurriculumClass.TeacherId == staffId)
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

        public static ProcessResponse<bool> HasEnrolments(this CurriculumClass currClass)
        {
            return new ProcessResponse<bool>(ResponseType.Ok, null, currClass.Enrolments.Any());
        }

        public static ProcessResponse<bool> HasPeriods(this CurriculumClass currClass)
        {
            return new ProcessResponse<bool>(ResponseType.Ok, null, currClass.CurriculumClassPeriods.Any());
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
                x.StudentId == student.Id && x.CurriculumClass.CurriculumClassPeriods.Any(p => p.PeriodId == periodId));

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