using System;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Models.Entity;
using NUnit.Framework;

namespace MyPortal.Tests.ModelTests
{
    [TestFixture]
    public class PersonModelTests
    {
        private PersonModel _model;

        [OneTimeSetUp]
        public void Setup()
        {
            _model = new PersonModel
            {
                Id = Guid.NewGuid(),
                FirstName = "Ewan",
                MiddleName = "James",
                Title = "Mr",
                LastName = "Benson",
                Dob = new DateTime(1982, 4, 22),
                Gender = "M",
                UpdatedDate = DateTime.Now,
                DirectoryId = Guid.NewGuid(),
                Deleted = false,
                ChosenFirstName = "Charlie"
            };
        }

        [Test]
        public void DisplayName_Default()
        {
            var output = _model.GetDisplayName();

            StringAssert.AreEqualIgnoringCase("Benson, Ewan James", output);
        }

        [Test]
        public void DisplayName_FullLegalName()
        {
            var output = _model.GetDisplayName(NameFormat.FullName);

            StringAssert.AreEqualIgnoringCase("Mr Ewan James Benson", output);
        }

        [Test]
        public void DisplayName_FullChosenName()
        {
            var output = _model.GetDisplayName(NameFormat.FullName, false);

            StringAssert.AreEqualIgnoringCase("Mr Charlie James Benson", output);
        }

        [Test]
        public void DisplayName_FullLegalAbbreviated()
        {
            var output = _model.GetDisplayName(NameFormat.FullNameAbbreviated);

            StringAssert.AreEqualIgnoringCase("Mr E J Benson", output);
        }

        [Test]
        public void DisplayName_Initials()
        {
            var output = _model.GetDisplayName(NameFormat.Initials);

            StringAssert.AreEqualIgnoringCase("EJB", output);
        }

        [Test]
        public void DisplayName_FullLegalNoTitle()
        {
            var output = _model.GetDisplayName(NameFormat.FullNameNoTitle);

            StringAssert.AreEqualIgnoringCase("Ewan James Benson", output);
        }
    }
}
