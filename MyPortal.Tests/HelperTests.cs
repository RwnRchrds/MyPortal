using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Helpers;
using NUnit.Framework;

namespace MyPortal.Tests
{
    [TestFixture]
    public class HelperTests
    {
        [Test]
        public async Task Encryption_String()
        {
            var plaintext = @"*Test\pl41nt3xt*";

            var key = CryptoHelper.GenerateEncryptionKey();

            var encryptionResult = await CryptoHelper.EncryptAsync(plaintext, key);

            var decryptedData =
                await CryptoHelper.DecryptAsync(encryptionResult.DataString, key, encryptionResult.IvString);

            Assert.That(decryptedData.Equals(plaintext, StringComparison.InvariantCulture));
        }

        [Test]
        public async Task Encryption_Bytes()
        {
            var plainData = new byte[] {32, 64, 51, 28, 133, 122};

            var key = CryptoHelper.GenerateEncryptionKey();

            var encryptionResult = await CryptoHelper.EncryptAsync(plainData, key);

            var decryptionResult =
                await CryptoHelper.DecryptAsync(encryptionResult.Data, key, encryptionResult.IvString);

            Assert.That(plainData.SequenceEqual(decryptionResult));
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
