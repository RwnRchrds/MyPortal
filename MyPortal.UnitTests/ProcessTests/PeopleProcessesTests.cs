using System.IO.Compression;
using System.Linq;
using AutoMapper;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using MyPortal.UnitTests.TestData;
using NUnit.Framework;

namespace MyPortal.UnitTests.ProcessTests
{
    [TestFixture]
    public class PeopleProcessesTests : MyPortalTestFixture
    {
        [Test]
        public static void GetStaffFromUserId_ReturnsStaffMember()
        {
            var result = PeopleProcesses.GetStaffFromUserId("jcobb", _context);
            
            Assert.That(result.ResponseType == ResponseType.Ok);
            
            Assert.That(result.ResponseObject.Person.LastName == "Cobb");
        }

        [Test]
        public static void GetStaffFromUserId_ReturnsNotFound()
        {
            var result = PeopleProcesses.GetStaffFromUserId("testme", _context);

            Assert.That(result.ResponseType == ResponseType.NotFound);
        }

        [Test]
        public static void GetStudentFromUserId_ReturnsStudent()
        {
            var result = PeopleProcesses.GetStudentFromUserId("aardvark", _context);

            Assert.That(result.ResponseType == ResponseType.Ok);

            Assert.That(result.ResponseObject.Person.LastName == "Aardvark");
        }

        [Test]
        public static void GetStudentFromUserId_ReturnsNotFound()
        {
            var result = PeopleProcesses.GetStudentFromUserId("testme", _context);

            Assert.That(result.ResponseType == ResponseType.NotFound);
        }

        [Test]
        public static void GetStudentDisplayName_ReturnsDisplayName()
        {
            var student = _context.Students.SingleOrDefault(x => x.Person.LastName == "Aardvark");

            var result = PeopleProcesses.GetStudentDisplayName(student);

            Assert.That(result.ResponseType == ResponseType.Ok);

            Assert.That(result.ResponseObject == "Aardvark, Aaron");
        }

        [Test]
        public static void GetStaffDisplayName_ReturnsDisplayName()
        {
            var staff = _context.StaffMembers.SingleOrDefault(x => x.Person.LastName == "Sprague");

            var result = PeopleProcesses.GetStaffDisplayName(staff);

            Assert.That(result.ResponseType == ResponseType.Ok);

            Assert.That(result.ResponseObject == "Mrs L Sprague");
        }
    }
}