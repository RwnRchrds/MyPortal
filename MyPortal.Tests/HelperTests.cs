using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using MyPortal.Database.Attributes;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Models.Entity;
using MyPortal.Logic.Models.Requests.Curriculum;
using MyPortal.Logic.Services;
using NUnit.Framework;

namespace MyPortal.Tests
{
    [TestFixture]
    public class HelperTests
    {
        [Test]
        public void Mapping_BusinessConfigurationIsValid()
        {
            Assert.DoesNotThrow(MappingHelper.GetConfig().ConfigurationProvider.AssertConfigurationIsValid);
        }

        [Test]
        public void Encryption_String()
        {
            var plaintext = @"*Test\pl41nt3xt*";
            var secret = @"$C&F)J@NcRfUjXnZr4u7x!A%D*G-KaPdSgVkYp3s5v8y/B?E(H+MbQeThWmZq4t7";

            var encryptedText = Encryption.Encrypt(plaintext, secret);

            var decryptedText = Encryption.Decrypt(encryptedText, secret);

            Assert.That(decryptedText.Equals(plaintext, StringComparison.InvariantCulture));
        }

        [Test]
        public void Encryption_Bytes()
        {
            var plainData = new byte[] {32, 64, 51, 28, 133, 122};
            var secret = @"H+MbQeThWmZq4t7w9z$C&F)J@NcRfUjXn2r5u8x/A%D*G-KaPdSgVkYp3s6v9y$B";

            var encryptedData = Encryption.Encrypt(plainData, secret);

            var decryptedData = Encryption.Decrypt(encryptedData, secret);

            Assert.That(plainData.SequenceEqual(decryptedData));
        }

        [Test]
        public void ValidateNhsNumber_WhenValid()
        {
            var isValid = ValidationHelper.ValidateNhsNumber("643 792 7186");
            
            Assert.IsTrue(isValid);
        }

        [Test]
        public void ValidateNhsNumber_WhenInvalid()
        {
            var isValid = ValidationHelper.ValidateNhsNumber("643 792 7187");

            Assert.IsFalse(isValid);
        }

        [Test]
        public void ValidateUpn_WhenValid()
        {
            var isValid = ValidationHelper.ValidateUpn("H801200001001");
            
            Assert.IsTrue(isValid);
        }

        [Test]
        public void ValidateUpn_WhenInvalid()
        {
            var isValid = ValidationHelper.ValidateUpn("G801200001001");

            Assert.IsFalse(isValid);
        }

        [Test]
        public void BitArrayToBytes_IsValid()
        {
            var array = PermissionHelper.CreatePermissionArray();

            array.SetAll(true);

            var bytes = array.ToBytes();

            var byteString = BitConverter.ToString(bytes).Replace("-", "");

            Assert.That(bytes != null);
        }
    }
}
