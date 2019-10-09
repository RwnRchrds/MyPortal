using System.Linq;
using System.Threading.Tasks;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Models.Exceptions;
using MyPortal.Models.Misc;
using MyPortal.Processes;
using NUnit.Framework;

namespace MyPortal.UnitTests.ProcessTests
{
    [TestFixture]
    public class AssessmentProcessTests : MyPortalTestFixture
    {
        [Test]
        public static async Task CreateResultSet_CreatesResultSet()
        {
            var academicYear = _context.CurriculumAcademicYears.SingleOrDefault(x => x.Name == "Current");
            
            Assert.IsNotNull(academicYear);

            var resultSet = new AssessmentResultSet
            {
                Name = "TestAddition",
                IsCurrent = false,
            };

            var initial = _context.AssessmentResultSets.Count();

            await AssessmentProcesses.CreateResultSet(resultSet, _context);

            var final = _context.AssessmentResultSets.Count();
            
            Assert.That(final == initial + 1);
        }

        [Test]
        public static async Task UpdateResultSet_UpdatesResultSet()
        {
            var resultSetInDb = _context.AssessmentResultSets.SingleOrDefault(x => x.Name == "Current");
            
            Assert.IsNotNull(resultSetInDb);

            var resultSet = new AssessmentResultSet
            {
                Id = resultSetInDb.Id,
                Name = "CurrentUpdated",
                IsCurrent = resultSetInDb.IsCurrent
            };

            await AssessmentProcesses.UpdateResultSet(resultSet, _context);

            var final = _context.AssessmentResultSets.SingleOrDefault(x => x.Id == resultSetInDb.Id);
            
            Assert.That(final.Name == "CurrentUpdated");
        }

        [Test]
        public static async Task DeleteResultSet_DeletesResultSet()
        {
            var resultSetInDb = _context.AssessmentResultSets.SingleOrDefault(x => x.Name == "DeleteMe");
            
            Assert.IsNotNull(resultSetInDb);

            var initial = _context.AssessmentResultSets.Count();

            await AssessmentProcesses.DeleteResultSet(resultSetInDb.Id, _context);

            var final = _context.AssessmentResultSets.Count();
            
            Assert.That(final == initial - 1);
        }

        [Test]
        public static async void GetResultSetById_ReturnsResultSet()
        {
            var resultSetName = "DeleteMe";
            var resultSetInDb = _context.AssessmentResultSets.SingleOrDefault(x => x.Name == resultSetName);
            
            Assert.IsNotNull(resultSetInDb);

            var result = await AssessmentProcesses.GetResultSetById(resultSetInDb.Id, _context);
            
            Assert.That(result.GetType() == typeof(AssessmentResultSetDto));
            Assert.That(result.Name == resultSetName);
        }

        [Test]
        public static async void GetAllResultSets_DataGrid_ReturnsResultSets()
        {
            var result = await AssessmentProcesses.GetAllResultSetsDataGrid(_context);
            
            Assert.That(result.Count() == 2);
        }
    }
}