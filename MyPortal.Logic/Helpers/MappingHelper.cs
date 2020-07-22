using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.ListModels;

namespace MyPortal.Logic.Helpers
{
    public static class MappingHelper
    {
        public static Dictionary<Type, Type> MappingDictionary = new Dictionary<Type, Type>
        {
            {typeof(AcademicYear), typeof(AcademicYearModel)},
            {typeof(Achievement), typeof(AchievementModel)},
            {typeof(AchievementOutcome), typeof(AchievementOutcomeModel)},
            {typeof(AchievementType), typeof(AchievementTypeModel)},
            {typeof(Aspect), typeof(AspectModel)},
            {typeof(AspectType), typeof(AspectTypeModel)},
            {typeof(ApplicationRole), typeof(RoleModel)},
            {typeof(ApplicationUser), typeof(UserModel)},
            {typeof(AttendanceCodeMeaning), typeof(AttendanceCodeMeaningModel)},
            {typeof(AttendanceCode), typeof(AttendanceCodeModel)},
            {typeof(AttendanceMark), typeof(AttendanceMarkModel)},
            {typeof(AttendanceWeek), typeof(AttendanceWeekModel)},
            {typeof(AttendanceWeekPattern), typeof(AttendanceWeekPatternModel)},
            {typeof(BehaviourOutcome), typeof(BehaviourOutcomeModel)},
            {typeof(BehaviourStatus), typeof(BehaviourStatusModel)},
            {typeof(Bulletin), typeof(BulletinModel)},
            {typeof(Contact), typeof(ContactModel)},
            {typeof(Detention), typeof(DetentionModel)},
            {typeof(DetentionType), typeof(DetentionTypeModel)},
            {typeof(DiaryEvent), typeof(DiaryEventModel)},
            {typeof(DiaryEventType), typeof(DiaryEventTypeModel)},
            {typeof(Directory), typeof(DirectoryModel)},
            {typeof(Document), typeof(DocumentModel)},
            {typeof(DocumentType), typeof(DocumentTypeModel)},
            {typeof(Grade), typeof(GradeModel)},
            {typeof(GradeSet), typeof(GradeSetModel)},
            {typeof(Homework), typeof(HomeworkModel)},
            {typeof(HomeworkSubmission), typeof(HomeworkSubmissionModel)},
            {typeof(House), typeof(HouseModel)},
            {typeof(Incident), typeof(IncidentModel)},
            {typeof(IncidentType), typeof(IncidentTypeModel)},
            {typeof(LessonPlan), typeof(LessonPlanModel)},
            {typeof(Location), typeof(LocationModel)},
            {typeof(LogNote), typeof(LogNoteModel)},
            {typeof(LogNoteType), typeof(LogNoteTypeModel)},
            {typeof(AttendancePeriod), typeof(AttendancePeriodModel)},
            {typeof(Person), typeof(PersonModel)},
            {typeof(RegGroup), typeof(RegGroupModel)},
            {typeof(ResultSet), typeof(ResultSetModel)},
            {typeof(Room), typeof(RoomModel)},
            {typeof(SenStatus), typeof(SenStatusModel)},
            {typeof(StaffMember), typeof(StaffMemberModel)},
            {typeof(Student), typeof(StudentModel)},
            {typeof(StudyTopic), typeof(StudyTopicModel)},
            {typeof(Subject), typeof(SubjectModel)},
            {typeof(Task), typeof(TaskModel)},
            {typeof(TaskType), typeof(TaskTypeModel)},
            {typeof(YearGroup), typeof(YearGroupModel)}
        };
        
        public static IMapper GetBusinessConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                foreach (var entityMapping in MappingDictionary)
                {
                    cfg.CreateMap(entityMapping.Key, entityMapping.Value).ReverseMap();
                }
            });

            return new Mapper(config);
        }
    }
}
