using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Services;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace MyPortal.UnitTests.Services
{
    [TestFixture]
    public class ValidationServiceTests
    {
        [Test]
        public void ValidateUpn_ConfirmsValid()
        {
            var result = ValidationService.ValidateUpn("H801200001001");

            Assert.That(result);
        }

        [Test]
        public void ValidateUpn_ConfirmsInvalid()
        {
            var result = ValidationService.ValidateUpn("M801200001001");

            Assert.That(!result);
        }
    }
}
