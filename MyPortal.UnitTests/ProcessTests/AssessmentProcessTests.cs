using System.Linq;
using System.Threading.Tasks;
using MyPortal.Dtos;
using MyPortal.Models.Database;
using MyPortal.Exceptions;
using MyPortal.Models.Misc;
using MyPortal.Services;
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

            await AssessmentService.CreateResultSet(resultSet, _context);

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

            await AssessmentService.UpdateResultSet(resultSet, _context);

            var final = _context.AssessmentResultSets.SingleOrDefault(x => x.Id == resultSetInDb.Id);
            
            Assert.That(final.Name == "CurrentUpdated");
        }

        [Test]
        public static async Task DeleteResultSet_DeletesResultSet()
        {
            var resultSetInDb = _context.AssessmentResultSets.SingleOrDefault(x => x.Name == "DeleteMe");
            
            Assert.IsNotNull(resultSetInDb);

            var initial = _context.AssessmentResultSets.Count();

            await AssessmentService.DeleteResultSet(resultSetInDb.Id, _context);

            var final = _context.AssessmentResultSets.Count();
            
            Assert.That(final == initial - 1);
        }

        [Test]
        public static async Task GetResultSetById_ReturnsResultSet()
        {
            var resultSetName = "DeleteMe";
            var resultSetInDb = _context.AssessmentResultSets.SingleOrDefault(x => x.Name == resultSetName);
            
            Assert.IsNotNull(resultSetInDb);

            var result = await AssessmentService.GetResultSetById(resultSetInDb.Id, _context);
            
            Assert.That(result.GetType() == typeof(AssessmentResultSetDto));
            Assert.That(result.Name == resultSetName);
        }

        [Test]
        public static async Task GetAllResultSets_DataGrid_ReturnsResultSets()
        {
            var result = await AssessmentService.GetAllResultSetsDataGrid(_context);
            
            Assert.That(result.Count() == 2);
        }
    }
}