using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Helpers;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace MyPortal.Tests
{
    [TestFixture]
    public class Mapping
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
    }
}
