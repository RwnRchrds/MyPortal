using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Effort;
using MyPortal.Dtos;
using MyPortal.Models;
using MyPortal.Models.Database;

namespace MyPortal.UnitTests.TestData
{
    public static class ContextControl
    {
        
        public class TestDbSet<TEntity> : DbSet<TEntity>, IQueryable, IEnumerable<TEntity>, IDbAsyncEnumerable<TEntity>
            where TEntity : class
        {
            ObservableCollection<TEntity> _data;
            IQueryable _query;

            public TestDbSet()
            {
                _data = new ObservableCollection<TEntity>();
                _query = _data.AsQueryable();
            }

            public override TEntity Add(TEntity item)
            {
                _data.Add(item);
                return item;
            }

            public override TEntity Remove(TEntity item)
            {
                _data.Remove(item);
                return item;
            }

            public override TEntity Attach(TEntity item)
            {
                _data.Add(item);
                return item;
            }

            public override TEntity Create()
            {
                return Activator.CreateInstance<TEntity>();
            }

            public override TDerivedEntity Create<TDerivedEntity>()
            {
                return Activator.CreateInstance<TDerivedEntity>();
            }

            public override ObservableCollection<TEntity> Local
            {
                get { return _data; }
            }

            Type IQueryable.ElementType
            {
                get { return _query.ElementType; }
            }

            Expression IQueryable.Expression
            {
                get { return _query.Expression; }
            }

            IQueryProvider IQueryable.Provider
            {
                get { return new TestDbAsyncQueryProvider<TEntity>(_query.Provider); }
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return _data.GetEnumerator();
            }

            IEnumerator<TEntity> IEnumerable<TEntity>.GetEnumerator()
            {
                return _data.GetEnumerator();
            }

            IDbAsyncEnumerator<TEntity> IDbAsyncEnumerable<TEntity>.GetAsyncEnumerator()
            {
                return new TestDbAsyncEnumerator<TEntity>(_data.GetEnumerator());
            }
        }
        
        internal class TestDbAsyncQueryProvider<TEntity> : IDbAsyncQueryProvider
    {
        private readonly IQueryProvider _inner;

        internal TestDbAsyncQueryProvider(IQueryProvider inner)
        {
            _inner = inner;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _inner.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _inner.Execute<TResult>(expression);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(expression));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute<TResult>(expression));
        }
    }

    internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IDbAsyncEnumerable<T>, IQueryable<T>
    {
        public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        { }

        public TestDbAsyncEnumerable(Expression expression)
            : base(expression)
        { }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetAsyncEnumerator();
        }

        IQueryProvider IQueryable.Provider
        {
            get { return new TestDbAsyncQueryProvider<T>(this); }
        }
    }

    internal class TestDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public TestDbAsyncEnumerator(IEnumerator<T> inner)
        {
            _inner = inner;
        }

        public void Dispose()
        {
            _inner.Dispose();
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(_inner.MoveNext());
        }

        public T Current
        {
            get { return _inner.Current; }
        }

        object IDbAsyncEnumerator.Current
        {
            get { return Current; }
        }
    }
        
        public static MyPortalDbContext GetTestData()
        {
            var effortConnection = DbConnectionFactory.CreateTransient();
            var context = new MyPortalDbContext(effortConnection)
            {
                //Test Data
                AssessmentGradeSets = new TestDbSet<AssessmentGradeSet>
                {
                    
                },
                AssessmentGrades = new TestDbSet<AssessmentGrade>
                {
                    
                },
                AssessmentResultSets = new TestDbSet<AssessmentResultSet>
                {
                    
                },
                AssessmentResults = new TestDbSet<AssessmentResult>
                {
                    
                },
                AttendanceCodes = new TestDbSet<AttendanceRegisterCode>
                {
                    
                },
                AttendanceMarks = new TestDbSet<AttendanceRegisterMark>
                {
                    
                },
                AttendanceMeanings = new TestDbSet<AttendanceRegisterCodeMeaning>
                {
                    
                },
                AttendancePeriods = new TestDbSet<AttendancePeriod>
                {
                    
                },
                AttendanceWeeks = new TestDbSet<AttendanceWeek>
                {
                    
                },
                BehaviourAchievementTypes = new TestDbSet<BehaviourAchievementType>
                {
                    
                },
                BehaviourAchievements = new TestDbSet<BehaviourAchievement>
                {
                    
                },
                BehaviourIncidentTypes = new TestDbSet<BehaviourIncidentType>
                {
                    
                },
                BehaviourIncidents = new TestDbSet<BehaviourIncident>
                {
                    
                },
                BehaviourLocations = new TestDbSet<BehaviourLocation>
                {
                    
                },
                CommunicationLogs = new TestDbSet<CommunicationLog>
                {
                    
                },
                CommunicationTypes = new TestDbSet<CommunicationType>
                {
                    
                },
                CurriculumAcademicYears = new TestDbSet<CurriculumAcademicYear>
                {
                    new CurriculumAcademicYear {Id = 1, Name = "2019", FirstDate = new DateTime(2019, 01, 01), LastDate = new DateTime(2019,12,31)}
                },
                CurriculumClasses = new TestDbSet<CurriculumClass>
                {
                    
                },
                CurriculumEnrolments = new TestDbSet<CurriculumEnrolment>
                {
                    
                },
                CurriculumLessonPlanTemplates = new TestDbSet<CurriculumLessonPlanTemplate>
                {
                    
                },
                CurriculumLessonPlans = new TestDbSet<CurriculumLessonPlan>
                {
                    
                },
                CurriculumSessions = new TestDbSet<CurriculumSession>
                {
                    
                },
                CurriculumStudyTopics = new TestDbSet<CurriculumStudyTopic>
                {
                    
                },
                CurriculumSubjects = new TestDbSet<CurriculumSubject>
                {
                    
                },
                DocumentTypes = new TestDbSet<DocumentType>
                {
                    
                },
                Documents = new TestDbSet<Document>
                {
                    
                },
                FinanceBasketItems = new TestDbSet<FinanceBasketItem>
                {
                    
                },
                FinanceProductTypes = new TestDbSet<FinanceProductType>
                {
                    new FinanceProductType {Id = 1, Description = "Meal", IsMeal = true, System = true}   
                },
                FinanceProducts = new TestDbSet<FinanceProduct>
                {
                    new FinanceProduct {Id = 1, Deleted = false, Description = "School Dinner", Price = 1.90m, Visible = true, OnceOnly = true, ProductTypeId = 1}
                },
                FinanceSales = new TestDbSet<FinanceSale>
                {
                    
                },
                MedicalConditions = new TestDbSet<MedicalCondition>
                {
                    
                },
                MedicalEvents = new TestDbSet<MedicalEvent>
                {
                    
                },
                MedicalStudentConditions = new TestDbSet<MedicalStudentCondition>
                {
                    
                },
                PastoralHouses = new TestDbSet<PastoralHouse>
                {
                    
                },
                PastoralRegGroups = new TestDbSet<PastoralRegGroup>
                {
                    new PastoralRegGroup {Id = 1, Name = "1A", TutorId = 3, YearGroupId = 1},
                    new PastoralRegGroup {Id = 2, Name = "8A", TutorId = 4, YearGroupId = 8}
                },
                PastoralYearGroups = new TestDbSet<PastoralYearGroup>
                {
                    new PastoralYearGroup {Id = 1, Name = "Year 1", KeyStage = 1, HeadId = 1},
                    new PastoralYearGroup {Id = 8, Name = "Year 8", KeyStage = 3, HeadId = 2}
                },
                PersonDocuments = new TestDbSet<PersonDocument>
                {
                    
                },
                PersonTypes = new TestDbSet<PersonType>
                {
                    new PersonType {Id = 1, Code = "S", Description = "Student"},
                    new PersonType {Id = 2, Code = "T", Description = "Staff"},
                    new PersonType {Id = 3, Code = "C", Description = "Contact"},
                    new PersonType {Id = 4, Code = "A", Description = "Agent"}
                },
                PersonnelObservations = new TestDbSet<PersonnelObservation>
                {
                    
                },
                PersonnelTrainingCertificates = new TestDbSet<PersonnelTrainingCertificate>
                {
                    
                },
                PersonnelTrainingCourses = new TestDbSet<PersonnelTrainingCourse>
                {
                    
                },
                Persons = new TestDbSet<Person>
                {
                    //Students
                    new Person {Id = 1, FirstName = "Aaron", LastName = "Aardvark", Dob = new DateTime(2000,06,05), Deleted = false, Gender = "M", PersonTypeId = 1, UserId = "aardvark"},
                    new Person {Id = 2, FirstName = "Chloe", LastName = "Brown", Dob = new DateTime(2000,06,05), Deleted = false, Gender = "F", PersonTypeId = 1, UserId = "cbrown"},
                    
                    //Staff
                    new Person {Id = 3, Title = "Mrs", FirstName = "Lily", LastName = "Sprague", Dob = new DateTime(1987,08,05), Deleted = false, Gender = "F", PersonTypeId = 2, UserId = "l.sprague"},
                    new Person {Id = 4, Title = "Sir", FirstName = "William", LastName = "Townsend", Dob = new DateTime(1986,04,26), Deleted = false, Gender = "M", PersonTypeId = 2, UserId = "wtownsend"},
                    new Person {Id = 5, Title = "Mrs", FirstName = "Joanne", LastName = "Cobb", Dob = new DateTime(1986,04,26), Deleted = false, Gender = "F", PersonTypeId = 2, UserId = "jcobb"},
                    new Person {Id = 6, Title = "Miss", FirstName = "Ellie", LastName = "Williams", Dob = new DateTime(1986,04,26), Deleted = false, Gender = "F", PersonTypeId = 2, UserId = "ewilliams"}
                },
                ProfileCommentBanks = new TestDbSet<ProfileCommentBank>
                {
                    
                },
                ProfileComments = new TestDbSet<ProfileComment>
                {
                    
                },
                ProfileLogTypes = new TestDbSet<ProfileLogType>
                {
                    new ProfileLogType {Id = 1, Name = "Academic Support", System = true},
                    new ProfileLogType {Id = 2, Name = "Report", System = true},
                    new ProfileLogType {Id = 3, Name = "Behaviour Log", System = true},
                    new ProfileLogType {Id = 4, Name = "Praise", System = true}
                },
                ProfileLogs = new TestDbSet<ProfileLog>
                {
                    
                },
                SenEvents = new TestDbSet<SenEvent>
                {
                    
                },
                SenProvisions = new TestDbSet<SenProvision>
                {
                    
                },
                SenStatuses = new TestDbSet<SenStatus>
                {
                    new SenStatus {Id = 1, Code = "N", Description = "No SEN Status"},
                    new SenStatus {Id = 2, Code = "E", Description = "School Early Years Action"}
                },
                StaffMembers = new TestDbSet<StaffMember>
                {
                    new StaffMember {Id = 1, Code = "LSP", Deleted = false, JobTitle = "Deputy Headteacher", PersonId = 3},
                    new StaffMember {Id = 2, Code = "WTO", Deleted = false, JobTitle = "Headteacher", PersonId = 4},
                    new StaffMember {Id = 3, Code = "JCO", Deleted = false, JobTitle = "Teacher", PersonId = 5},
                    new StaffMember {Id = 4, Code = "EWI", Deleted = false, JobTitle = "Teacher", PersonId = 6}
                },
                Students = new TestDbSet<Student>
                {
                    new Student {PersonId = 1, Deleted = false, Id = 1, AccountBalance = 12.99m, PupilPremium = false, FreeSchoolMeals = false, GiftedAndTalented = false, RegGroupId = 1, SenStatusId = 1, YearGroupId = 1},
                    new Student {PersonId = 2, Deleted = false, Id = 2, AccountBalance = 50.00m, PupilPremium = true, FreeSchoolMeals = true, GiftedAndTalented = false, RegGroupId = 1, SenStatusId = 2, YearGroupId = 1}
                }
            };

            return context;
        }

        public static void InitialiseMaps()
        {
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());
        }
    }
}