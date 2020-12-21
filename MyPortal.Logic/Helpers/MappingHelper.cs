﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models;
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
            {typeof(CommentBank), typeof(CommentBankModel)},
            {typeof(CommunicationLog), typeof(CommunicationLogModel)},
            {typeof(CommunicationType), typeof(CommunicationTypeModel)},
            {typeof(Contact), typeof(ContactModel)},
            {typeof(ContactRelationshipType), typeof(ContactRelationshipTypeModel)},
            {typeof(Course), typeof(CourseModel)},
            {typeof(CoverArrangement), typeof(CoverArrangementModel)},
            {typeof(CurriculumBand), typeof(CurriculumBandModel)},
            {typeof(CurriculumBandBlockAssignment), typeof(CurriculumBandBlockAssignmentModel)},
            {typeof(CurriculumBandMembership), typeof(CurriculumBandMembershipModel)},
            {typeof(CurriculumBlock), typeof(CurriculumBlockModel)},
            {typeof(CurriculumGroup), typeof(CurriculumGroupModel)},
            {typeof(CurriculumGroupMembership), typeof(CurriculumGroupMembershipModel)},
            {typeof(CurriculumYearGroup), typeof(CurriculumYearGroupModel)},
            {typeof(Detention), typeof(DetentionModel)},
            {typeof(DetentionType), typeof(DetentionTypeModel)},
            {typeof(DiaryEvent), typeof(DiaryEventModel)},
            {typeof(DiaryEventAttendee), typeof(DiaryEventAttendeeModel)},
            {typeof(DiaryEventAttendeeResponse), typeof(DiaryEventAttendeeResponseModel)},
            {typeof(DiaryEventTemplate), typeof(DiaryEventTemplateModel)},
            {typeof(DiaryEventType), typeof(DiaryEventTypeModel)},
            {typeof(DietaryRequirement), typeof(DietaryRequirementModel)},
            {typeof(Directory), typeof(DirectoryModel)},
            {typeof(Document), typeof(DocumentModel)},
            {typeof(DocumentType), typeof(DocumentTypeModel)},
            {typeof(EmailAddress), typeof(EmailAddressModel)},
            {typeof(EmailAddressType), typeof(EmailAddressTypeModel)},
            {typeof(Ethnicity), typeof(EthnicityModel)},
            {typeof(ExamAssessment), typeof(ExamAssessmentModel)},
            {typeof(ExamAssessmentAspect), typeof(ExamAssessmentAspectModel)},
            {typeof(ExamAssessmentMode), typeof(ExamAssessmentModeModel)},
            {typeof(ExamAward), typeof(ExamAwardModel)},
            {typeof(ExamAwardElement), typeof(ExamAwardElementModel)},
            {typeof(ExamAwardSeries), typeof(ExamAwardSeriesModel)},
            {typeof(ExamBaseComponent), typeof(ExamBaseComponentModel)},
            {typeof(ExamBaseElement), typeof(ExamBaseElementModel)},
            {typeof(ExamBoard), typeof(ExamBoardModel)},
            {typeof(ExamCandidate), typeof(ExamCandidateModel)},
            {typeof(ExamCandidateSeries), typeof(ExamCandidateSeriesModel)},
            {typeof(ExamCandidateSpecialArrangement), typeof(ExamCandidateSpecialArrangementModel)},
            {typeof(ExamComponent), typeof(ExamComponentModel)},
            {typeof(ExamComponentSitting), typeof(ExamComponentSittingModel)},
            {typeof(ExamElement), typeof(ExamElementModel)},
            {typeof(ExamElementComponent), typeof(ExamElementComponentModel)},
            {typeof(ExamEnrolment), typeof(ExamEnrolmentModel)},
            {typeof(ExamQualification), typeof(ExamQualificationModel)},
            {typeof(ExamQualificationLevel), typeof(ExamQualificationLevelModel)},
            {typeof(ExamResultEmbargo), typeof(ExamResultEmbargoModel)},
            {typeof(ExamRoom), typeof(ExamRoomModel)},
            {typeof(ExamRoomSeat), typeof(ExamRoomSeatModel)},
            {typeof(ExamSeason), typeof(ExamSeasonModel)},
            {typeof(ExamSeatAllocation), typeof(ExamSeatAllocationModel)},
            {typeof(ExamSeries), typeof(ExamSeriesModel)},
            {typeof(ExamSession), typeof(ExamSessionModel)},
            {typeof(ExamSpecialArrangement), typeof(ExamSpecialArrangementModel)},
            {typeof(ExclusionReason), typeof(ExclusionReasonModel)},
            {typeof(ExclusionType), typeof(ExclusionTypeModel)},
            {typeof(File), typeof(FileModel)},
            {typeof(GiftedTalented), typeof(GiftedTalentedModel)},
            {typeof(GovernanceType), typeof(GovernanceTypeModel)},
            {typeof(Grade), typeof(GradeModel)},
            {typeof(GradeSet), typeof(GradeSetModel)},
            {typeof(HomeworkItem), typeof(HomeworkModel)},
            {typeof(HomeworkSubmission), typeof(HomeworkSubmissionModel)},
            {typeof(House), typeof(HouseModel)},
            {typeof(Incident), typeof(IncidentModel)},
            {typeof(IncidentDetention), typeof(IncidentDetentionModel)},
            {typeof(IncidentType), typeof(IncidentTypeModel)},
            {typeof(IntakeType), typeof(IntakeTypeModel)},
            {typeof(LessonPlan), typeof(LessonPlanModel)},
            {typeof(LessonPlanTemplate), typeof(LessonPlanTemplateModel)},
            {typeof(LocalAuthority), typeof(LocalAuthorityModel)},
            {typeof(Location), typeof(LocationModel)},
            {typeof(LogNote), typeof(LogNoteModel)},
            {typeof(LogNoteType), typeof(LogNoteTypeModel)},
            {typeof(MarksheetColumn), typeof(MarksheetColumnModel)},
            {typeof(MarksheetTemplate), typeof(MarksheetTemplateModel)},
            {typeof(MarksheetTemplateGroup), typeof(MarksheetTemplateGroupModel)},
            {typeof(MedicalCondition), typeof(MedicalConditionModel)},
            {typeof(MedicalEvent), typeof(MedicalEventModel)},
            {typeof(Observation), typeof(ObservationModel)},
            {typeof(ObservationOutcome), typeof(ObservationOutcomeModel)},
            {typeof(Person), typeof(PersonModel)},
            {typeof(PersonCondition), typeof(PersonConditionModel)},
            {typeof(PersonDietaryRequirement), typeof(PersonDietaryRequirementModel)},
            {typeof(PhoneNumber), typeof(PhoneNumberModel)},
            {typeof(PhoneNumberType), typeof(PhoneNumberTypeModel)},
            {typeof(Photo), typeof(PhotoModel)},
            {typeof(Product), typeof(ProductModel)},
            {typeof(ProductType), typeof(ProductTypeModel)},
            {typeof(RegGroup), typeof(RegGroupModel)},
            {typeof(Report), typeof(ReportModel)},
            {typeof(ReportCard), typeof(ReportCardModel)},
            {typeof(ReportCardSubmission), typeof(ReportCardSubmissionModel)},
            {typeof(ReportCardTarget), typeof(ReportCardTargetModel)},
            {typeof(ReportCardTargetSubmission), typeof(ReportCardTargetSubmissionModel)},
            {typeof(Result), typeof(ResultModel)},
            {typeof(ResultSet), typeof(ResultSetModel)},
            {typeof(Role), typeof(RoleModel)},
            {typeof(Room), typeof(RoomModel)},
            {typeof(RoomClosure), typeof(RoomClosureModel)},
            {typeof(RoomClosureReason), typeof(RoomClosureReasonModel)},
            {typeof(School), typeof(SchoolModel)},
            {typeof(SchoolPhase), typeof(SchoolPhaseModel)},
            {typeof(SchoolType), typeof(SchoolTypeModel)},
            {typeof(SenEvent), typeof(SenEventModel)},
            {typeof(SenEventType), typeof(SenEventTypeModel)},
            {typeof(SenProvision), typeof(SenProvisionModel)},
            {typeof(SenProvisionType), typeof(SenProvisionTypeModel)},
            {typeof(SenReview), typeof(SenReviewModel)},
            {typeof(SenReviewType), typeof(SenReviewTypeModel)},
            {typeof(SenStatus), typeof(SenStatusModel)},
            {typeof(Session), typeof(SessionModel)},
            {typeof(StaffAbsence), typeof(StaffAbsenceModel)},
            {typeof(StaffAbsenceType), typeof(StaffAbsenceTypeModel)},
            {typeof(StaffIllnessType), typeof(StaffIllnessTypeModel)},
            {typeof(StaffMember), typeof(StaffMemberModel)},
            {typeof(Student), typeof(StudentModel)},
            {typeof(StudentAgentRelationship), typeof(StudentAgentRelationshipModel)},
            {typeof(StudentContactRelationship), typeof(StudentContactRelationshipModel)},
            {typeof(StudentGroup), typeof(StudentGroupModel)},
            {typeof(StudyTopic), typeof(StudyTopicModel)},
            {typeof(Subject), typeof(SubjectModel)},
            {typeof(SubjectCode), typeof(SubjectCodeModel)},
            {typeof(SubjectCodeSet), typeof(SubjectCodeSetModel)},
            {typeof(SubjectStaffMember), typeof(SubjectStaffMemberModel)},
            {typeof(SubjectStaffMemberRole), typeof(SubjectStaffMemberRoleModel)},
            {typeof(SystemArea), typeof(SystemAreaModel)},
            {typeof(Task), typeof(TaskModel)},
            {typeof(TaskType), typeof(TaskTypeModel)},
            {typeof(TrainingCertificate), typeof(TrainingCertificateModel)},
            {typeof(TrainingCertificateStatus), typeof(TrainingCertificateStatusModel)},
            {typeof(TrainingCourse), typeof(TrainingCourseModel)},
            {typeof(User), typeof(UserModel)},
            {typeof(VatRate), typeof(VatRateModel)},
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
