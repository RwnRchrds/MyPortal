using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Processes;
using NUnit.Framework;

namespace MyPortal.UnitTests.ProcessTests
{
    [TestFixture]
    public class SystemProcessTests : MyPortalTestFixture
    {
        [Test]
        public static void ValidateUpn_ReturnsTrue()
        {
            //Valid UPN
            const string upn = "N936358319001";

            var result = SystemProcesses.ValidateUpn(upn);

            Assert.That(result);
        }

        [Test]
        public static void ValidateUpn_ReturnsFalse()
        {
            //Invalid UPN
            const string upn = "H936357319001";

            var result = SystemProcesses.ValidateUpn(upn);

            Assert.That(!result);
        }
    }
}
