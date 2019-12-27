using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Data.Models;

namespace MyPortal.UnitTests.Data
{
    public class TestContext
    {
        public List<Aspect> Aspects { get; set; }
        public List<AspectType> AspectTypes { get; set; }
        public List<Grade> Grades { get; set; }
        public List<GradeSet> GradeSets { get; set; }
        public List<Result> Results { get; set; }
        public List<ResultSet> ResultSets { get; set; }
        public List<AttendanceCode> AttendanceCodes { get; set; }
        public List<AttendanceCodeMeaning> AttendanceCodeMeanings { get; set; }
        public List<AttendanceMark> AttendanceMarks { get; set; }
        public List<Period> Periods { get; set; }
        public List<AttendanceWeek> AttendanceWeeks { get; set; }
        public List<Achievement> Achievements { get; set; }
        public List<AchievementType> AchievementTypes { get; set; }
        public List<Incident> Incidents { get; set; }
        public List<IncidentType> IncidentTypes { get; set; }
        public List<Address> Addresses { get; set; }
        public List<AddressPerson> AddressPersons { get; set; }
        public List<EmailAddress> EmailAddresses { get; set; }
        public List<EmailAddressType> EmailAddressTypes { get; set; }
        public List<CommunicationLog> CommunicationLogs { get; set; }
        public List<PhoneNumber> PhoneNumbers { get; set; }
        public List<PhoneNumberType> PhoneNumberTypes { get; set; }
        public List<CommunicationType> CommunicationTypes { get; set; }
        public List<Contact> Contacts { get; set; }
        public List<AcademicYear> AcademicYears { get; set; }
        public List<Class> Classes { get; set; }
        public List<DetentionType> DetentionTypes { get; set; }
        public List<Detention> Detentions { get; set; }
        public List<DiaryEvent> DiaryEvents { get; set; }
        public List<Enrolment> Enrolments { get; set; }
        public List<IncidentDetention> IncidentDetentions { get; set; }
        public List<LessonPlan> LessonPlans { get; set; }
        public List<LessonPlanTemplate> LessonPlanTemplates { get; set; }
        public List<Session> Sessions { get; set; }
        public List<StudyTopic> StudyTopics { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<SubjectStaffMember> SubjectStaffMembers { get; set; }
        public List<SubjectStaffMemberRole> SubjectStaffMemberRoles { get; set; }
        public List<Document> Documents { get; set; }
        public List<DocumentType> DocumentTypes { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        public List<Product> Products { get; set; }
        public List<ProductType> ProductTypes { get; set; }
        public List<Sale> Sales { get; set; }
        public List<LocalAuthority> LocalAuthorities { get; set; }
        public List<Condition> Conditions { get; set; }
        public List<DietaryRequirement> DietaryRequirements { get; set; }
        public List<MedicalEvent> MedicalEvents { get; set; }
        public List<PersonCondition> PersonConditions { get; set; }
        public List<PersonDietaryRequirement> PersonDietaryRequirements { get; set; }
        public List<House> Houses { get; set; }
        public List<RegGroup> RegGroups { get; set; }
        public List<YearGroup> YearGroups { get; set; }
        public List<PersonAttachment> PersonAttachments { get; set; }
        public List<Observation> Observations { get; set; }
        public List<ObservationOutcome> ObservationOutcomes { get; set; }
        public List<TrainingCertificate> TrainingCertificates { get; set; }
    }
}
