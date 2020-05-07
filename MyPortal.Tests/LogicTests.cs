using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPortal.Logic.Constants;
using MyPortal.Logic.Helpers;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace MyPortal.Tests
{
    [TestFixture]
    public class LogicTests
    {
        [Test]
        public void Mapping_BusinessConfigurationIsValid()
        {
            Assert.DoesNotThrow(MappingHelper.GetBusinessConfig().ConfigurationProvider.AssertConfigurationIsValid);
        }

        [Test]
        public void Mapping_DataGridConfigurationIsValid()
        {
            Assert.DoesNotThrow(MappingHelper.GetDataGridConfig().ConfigurationProvider.AssertConfigurationIsValid);
        }

        [Test]
        public void Encryption()
        {
            var plaintext = @"*Test\pl41nt3xt*";
            var secret = @"64867486t";
            var salt = Encoding.ASCII.GetBytes(",./09&fd");

            var encryptedText = Logic.Helpers.Encryption.EncryptString(plaintext, salt, secret);

            var decryptedText = Logic.Helpers.Encryption.DecryptString(encryptedText, salt, secret);

            Assert.That(decryptedText == plaintext);
        }

        [Test]
        public void Permissions_UniqueClaimValues()
        {
            var perms = Permissions.GetAll();

            Assert.That(perms.Distinct().Count() == perms.Length);
        }
    }
}
