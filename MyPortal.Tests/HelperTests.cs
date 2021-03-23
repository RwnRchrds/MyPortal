﻿using System;
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
        public void EntityModel_NoMissingProperties()
        {
            var mappingValid = true;

            Dictionary<Type, Type> faultyMappings = new Dictionary<Type, Type>();

            foreach (var type in from type in MappingHelper.MappingDictionary
                let entityProperties = type.Key.GetProperties().Where(x =>
                    (x.PropertyType == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(x.PropertyType)) && !x.GetCustomAttributes(typeof(EntityOnlyAttribute)).Any()).ToList()
                let modelProperties = type.Value.GetProperties().ToList()
                let exceptions = new List<(Type type, string propertyName)>
                {
                    (typeof(UserModel), "NormalizedUserName"),
                    (typeof(UserModel), "NormalizedEmail"),
                    (typeof(UserModel), "PasswordHash"),
                    (typeof(UserModel), "SecurityStamp"),
                    (typeof(UserModel), "ConcurrencyStamp"),
                    (typeof(UserModel), "TwoFactorEnabled"),
                }
                from entityProperty in entityProperties.Where(entityProperty =>
                    !exceptions.Contains((type.Value, entityProperty.Name)) && modelProperties.All(m =>
                        !String.Equals(m.Name, entityProperty.Name, StringComparison.CurrentCultureIgnoreCase)))
                select type)
            {
                mappingValid = false;

                if (!faultyMappings.ContainsKey(type.Key))
                {
                    faultyMappings.Add(type.Key, type.Value);
                }
            }

            if (mappingValid)
            {
                Assert.Pass();
            }

            var message =
                $"Missing properties were found in the following mappings: {string.Join(", ", faultyMappings.Keys.Select(x => x.Name))}.";

            Assert.Fail(message);
        }

        [Test]
        public void EntityModel_NoSurplusProperties()
        {
            var mappingValid = true;

            Dictionary<Type, Type> faultyMappings = new Dictionary<Type, Type>();

            foreach (var type in from type in MappingHelper.MappingDictionary
                let entityProperties = type.Key.GetProperties().Where(x =>
                    x.PropertyType == typeof(string) || x.PropertyType == typeof(byte[]) || !typeof(IEnumerable).IsAssignableFrom(x.PropertyType)).ToList()
                let modelProperties = type.Value.GetProperties().ToList()
                let exceptions = new List<(Type type, string propertyName)>
                {
                    (typeof(CoverArrangementModel), "StaffChanged"),
                    (typeof(CoverArrangementModel), "RoomChanged"),
                    (typeof(TaskModel), "Overdue")
                }
                from modelProperty in modelProperties.Where(modelProperty =>
                    !exceptions.Contains((type.Value, modelProperty.Name)) && entityProperties.All(e =>
                        !String.Equals(e.Name, modelProperty.Name, StringComparison.CurrentCultureIgnoreCase)))
                select type)
            {
                mappingValid = false;

                if (!faultyMappings.ContainsKey(type.Key))
                {
                    faultyMappings.Add(type.Key, type.Value);
                }
            }

            if (mappingValid)
            {
                Assert.Pass();
            }

            var message =
                $"Surplus properties were found in the following mappings: {string.Join(", ", faultyMappings.Keys.Select(x => x.Name))}.";

            Assert.Fail(message);
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
