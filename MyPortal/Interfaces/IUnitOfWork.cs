using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPortal.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAssessmentAspectRepository AssessmentAspects { get; }
        IAssessmentAspectTypeRepository AssessmentAspectTypes { get; }
        IAssessmentGradeRepository AssessmentGrades { get; }
        IAssessmentGradeSetRepository AssessmentGradeSets { get; }
        IAssessmentResultRepository AssessmentResults { get; }
        IAssessmentResultSetRepository AssessmentResultSets { get; }
        IAttendanceCodeRepository AttendanceCodes { get; }
        IAttendanceMarkRepository AttendanceMarks { get; }
        IAttendanceMeaningRepository AttendanceMeanings { get; }
        IAttendancePeriodRepository AttendancePeriods { get; }
        IAttendanceWeekRepository AttendanceWeeks { get; }
        IBehaviourAchievementRepository BehaviourAchievements { get; }
        IBehaviourAchievementTypeRepository BehaviourAchievementTypes { get; }

        Task<int> Complete();
    }
}
