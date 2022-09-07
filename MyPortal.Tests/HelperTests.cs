using System;
using System.Linq;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using NUnit.Framework;

namespace MyPortal.Tests
{
    [TestFixture]
    public class HelperTests
    {
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
        [TestCase("643 792 7186", true)]
        [TestCase("643 792 7187", false)]
        public void ValidateNhsNumber_ReturnsCorrectResult(string nhsNumber, bool expectedResult)
        {
            var isValid = ValidationHelper.ValidateNhsNumber(nhsNumber);
            
            Assert.That(isValid, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("H801200001001", true)]
        [TestCase("G801200001001", false)]
        public void ValidateUpn_ReturnsCorrectResult(string upn, bool expectedResult)
        {
            var isValid = ValidationHelper.ValidateUpn(upn);
            
            Assert.That(isValid, Is.EqualTo(expectedResult));
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
