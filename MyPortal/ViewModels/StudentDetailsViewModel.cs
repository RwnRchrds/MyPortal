using System.Collections.Generic;
using MyPortal.Models;
using MyPortal.Models.Database;
using MyPortal.Models.Misc;

namespace MyPortal.ViewModels
{
    public class StudentDetailsViewModel
    {
        public Student Student { get; set; }
        public IDictionary<int, string> LogTypes { get; set; }
        public ProfileLog Log { get; set; }
        public PersonDocumentUpload Upload { get; set; }
        public IDictionary<int, string> CommentBanks { get; set; }
        public bool HasAttendaceData { get; set; }
        public double? Attendance { get; set; }
        public int? AchievementCount { get; set; }
        public int? BehaviourCount { get; set; }
    }
}