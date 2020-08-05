using System;
using System.Data.Common;
using MyPortal.Database.Models;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Services;
using MyPortal.Tests.TestData;
using NUnit.Framework;

namespace MyPortal.Tests.ServiceTests
{
    [TestFixture]
    public class SchoolServiceTests
    {
        private void AddTestSchool(ApplicationDbContext context)
        {
            context.Schools.Add(new School
            {
                Id = Guid.NewGuid(),
                Name = "MyPortal Test School",
                SchoolPhase = new SchoolPhase {Id = Guid.NewGuid(),Description = "TestPhase"},
                Type = new SchoolType {Id = Guid.NewGuid(), Description = "TestType"},
                GovernanceType = new GovernanceType {Id = Guid.NewGuid(), Description = "GovernanceTest"},
                IntakeType = new IntakeType {Id = Guid.NewGuid(),Description = "IntakeTest"},
                LocalAuthority = new LocalAuthority {Id = Guid.NewGuid(), Name = "LATest", LeaCode = 999},
                EstablishmentNumber = 9999,
                Uprn = "101010",
                Urn = "101010",
                Website = "www.google.co.uk",
                EmailAddress = "admin@google.co.uk",
                TelephoneNo = "01252 999 999",
                FaxNo = "01252 999 998",
                Local = true
            });

            context.SaveChanges();
        }

        [Test]
        public async System.Threading.Tasks.Task GetLocalSchoolName_ReturnsValue()
        {
            using (var context = TestDataFactory.GetContext())
            using (ISchoolService schoolService = new SchoolService(context))
            {
                AddTestSchool(context);

                var schoolName = await schoolService.GetLocalSchoolName();
            
                Assert.AreEqual("MyPortal Test School", schoolName);
            }
        }
    }
}