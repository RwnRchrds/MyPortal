using System.Linq;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using NUnit.Framework;

namespace MyPortal.UnitTests.ProcessTests
{
    [TestFixture]
    public class AssessmentProcessesTests : MyPortalTestFixture
    {
        [Test]
        public static void CreateResultSet_CreatesResultSet()
        {
            var academicYear = _context.CurriculumAcademicYears.SingleOrDefault(x => x.Name == "Current");
            
            Assert.IsNotNull(academicYear);

            var resultSet = new AssessmentResultSet
            {
                Name = "TestAddition",
                IsCurrent = false,
                AcademicYearId = academicYear.Id
            };

            var initial = _context.AssessmentResultSets.Count();

            AssessmentProcesses.CreateResultSet(resultSet, _context);

            var final = _context.AssessmentResultSets.Count();
            
            Assert.That(final == initial + 1);
        }

        [Test]
        public static void UpdateResultSet_UpdatesResultSet()
        {
            var resultSetInDb = _context.AssessmentResultSets.SingleOrDefault(x => x.Name == "Current");
            
            Assert.IsNotNull(resultSetInDb);

            var resultSet = new AssessmentResultSet
            {
                Id = resultSetInDb.Id,
                Name = "CurrentUpdated",
                IsCurrent = resultSetInDb.IsCurrent
            };

            AssessmentProcesses.UpdateResultSet(resultSet, _context);

            var final = _context.AssessmentResultSets.SingleOrDefault(x => x.Id == resultSetInDb.Id);
            
            Assert.That(final.Name == "CurrentUpdated");
        }

        [Test]
        public static void DeleteResultSet_DeletesResultSet()
        {
            var resultSetInDb = _context.AssessmentResultSets.SingleOrDefault(x => x.Name == "DeleteMe");
            
            Assert.IsNotNull(resultSetInDb);

            var initial = _context.AssessmentResultSets.Count();

            AssessmentProcesses.DeleteResultSet(resultSetInDb.Id, _context);

            var final = _context.AssessmentResultSets.Count();
            
            Assert.That(final == initial - 1);
        }

        [Test]
        public static void GetResultSetById_ReturnsResultSet()
        {
            var resultSetName = "DeleteMe";
            var resultSetInDb = _context.AssessmentResultSets.SingleOrDefault(x => x.Name == resultSetName);
            
            Assert.IsNotNull(resultSetInDb);

            var result = AssessmentProcesses.GetResultSetById(resultSetInDb.Id, _context);
            
            Assert.That(result.ResponseType == ResponseType.Ok);
            Assert.That(result.ResponseObject.GetType() == typeof(AssessmentResultSetDto));
            Assert.That(result.ResponseObject.Name == resultSetName);
        }

        [Test]
        public static void GetResultSetById_ReturnsNotFound()
        {
            var result = AssessmentProcesses.GetResultSetById(9999, _context);
            
            Assert.That(result.ResponseType == ResponseType.NotFound);
        }
    }
}