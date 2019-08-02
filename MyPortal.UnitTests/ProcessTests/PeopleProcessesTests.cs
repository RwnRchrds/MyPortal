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
        }
    }
}