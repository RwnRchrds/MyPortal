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
            var upn = "H801200001001";

            var result = SystemProcesses.ValidateUpn(upn);

            Assert.That(result);
        }
    }
}
