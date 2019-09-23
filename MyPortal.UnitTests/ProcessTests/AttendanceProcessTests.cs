using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Processes;
using NUnit.Framework;

namespace MyPortal.UnitTests.ProcessTests
{
    public class AttendanceProcessTests : MyPortalTestFixture
    {
        [Test]
        public static void CreateAttendanceWeeksForAcademicYear_CreatesAttendanceWeeks()
        {
            var academicYear = _context.CurriculumAcademicYears.FirstOrDefault();

            Assert.That(academicYear != null);

            var result = AttendanceProcesses.CreateAttendanceWeeksForAcademicYear(academicYear.Id, _context);
        }
    }
}
