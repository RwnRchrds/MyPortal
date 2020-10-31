using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using MyPortal.Database.Models;
using MyPortal.Logic.Models.Entity;

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
            {typeof(Address), typeof(AddressModel)},
            {typeof(AddressPerson), typeof(AddressPersonModel)},
            {typeof(Agency), typeof(AgencyModel)},
            {typeof(AgencyType), typeof(AgencyTypeModel)},
            {typeof(Agent), typeof(AgentModel)},
            {typeof(AgentRelationshipType), typeof(AgentRelationshipTypeModel)},
            {typeof(Aspect), typeof(AspectModel)},
            {typeof(AspectType), typeof(AspectTypeModel)},
            {typeof(AttendanceCode), typeof(AttendanceCodeModel)},
            {typeof(AttendanceCodeMeaning), typeof(AttendanceCodeMeaningModel)},
            {typeof(AttendanceMark), typeof(AttendanceMarkModel)},
            {typeof(AttendancePeriod), typeof(AttendancePeriodModel)},
            {typeof(AttendanceWeek), typeof(AttendanceWeekModel)},
            {typeof(AttendanceWeekPattern), typeof(AttendanceWeekPatternModel)},
            {typeof(BasketItem), typeof(BasketItemModel)},
            {typeof(BehaviourOutcome), typeof(BehaviourOutcomeModel)},
            {typeof(BehaviourStatus), typeof(BehaviourStatusModel)},
            {typeof(BehaviourTarget), typeof(BehaviourTargetModel)},
            {typeof(Bill), typeof(BillModel)},
            {typeof(BillItem), typeof(BillItemModel)},
            {typeof(Bulletin), typeof(BulletinModel)},
            {typeof(Class), typeof(ClassModel)},
            {typeof(Comment), typeof(CommentModel)},
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
            {typeof(HomeworkItem), typeof(HomeworkModel)},
            {typeof(HomeworkSubmission), typeof(HomeworkSubmissionModel)},
            {typeof(House), typeof(HouseModel)},
            {typeof(Incident), typeof(IncidentModel)},
            {typeof(IncidentType), typeof(IncidentTypeModel)},
            {typeof(LessonPlan), typeof(LessonPlanModel)},
            {typeof(Location), typeof(LocationModel)},
            {typeof(LogNote), typeof(LogNoteModel)},
            {typeof(LogNoteType), typeof(LogNoteTypeModel)},
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
            {typeof(User), typeof(UserModel)},
            {typeof(YearGroup), typeof(YearGroupModel)}
        };
        
        public static IMapper GetConfig()
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
