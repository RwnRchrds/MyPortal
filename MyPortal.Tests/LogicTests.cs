using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPortal.Database.Constants;
using MyPortal.Database.Models;
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
        public void Encryption_Asymmetric()
        {
            var plaintext = @"*Test\pl41nt3xt*";
            var secret = @"64867486t";
            var salt = Encoding.ASCII.GetBytes(",./09&fd");

            var encryptedText = Logic.Helpers.Encryption.EncryptString(plaintext, salt, secret);

            var decryptedText = Logic.Helpers.Encryption.DecryptString(encryptedText, salt, secret);

            Assert.That(decryptedText == plaintext);
        }

        [Test]
        public void ValidateNhsNumber_WhenValid()
        {
            var isValid = ValidationHelper.ValidateNhsNumber("643 792 7186");
            
            Assert.IsTrue(isValid);
        }

        [Test]
        public void ValidateUpn_WhenValid()
        {
            var isValid = ValidationHelper.ValidateUpn("H801200001001");
            
            Assert.IsTrue(isValid);
        }

        [Test]
        [Ignore("This test is only used during development.")]
        public void EntityModel_MissingProperties()
        {
            var mappingValid = true;
            
            foreach (var type in MappingHelper.MappingDictionary)
            {
                var entityProperties = type.Key.GetProperties().Where(x =>
                    x.PropertyType == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(x.PropertyType)).ToList();
                
                var modelProperties = type.Value.GetProperties().ToList();

                foreach (var entityProperty in entityProperties)
                {
                    if (modelProperties.All(m => !String.Equals(m.Name, entityProperty.Name, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        mappingValid = false;
                    }
                }
            }
            
            Assert.IsTrue(mappingValid);
        }

        [Test]
        [Ignore("This test is only used during development.")]
        public void EntityModel_SurplusProperties()
        {
            var mappingValid = true;
            
            foreach (var type in MappingHelper.MappingDictionary)
            {
                var entityProperties = type.Key.GetProperties().Where(x =>
                    x.PropertyType == typeof(string) || !typeof(IEnumerable).IsAssignableFrom(x.PropertyType)).ToList();
                
                var modelProperties = type.Value.GetProperties().ToList();

                foreach (var modelProperty in modelProperties)
                {
                    if (entityProperties.All(e => !String.Equals(e.Name, modelProperty.Name, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        mappingValid = false;
                    }    
                }
            }
            
            Assert.IsTrue(mappingValid);
        }
    }
}
