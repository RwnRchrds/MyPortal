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
        [Ignore("Not Working")]
        public static void GetStaffFromUserId_ReturnsStaffMember()
        {
            var context = ContextControl.GetTestData();
            var result = PeopleProcesses.GetStaffFromUserId("jcobb", context);
            
            Assert.That(result.ResponseType == ResponseType.Ok);
        }
    }
}