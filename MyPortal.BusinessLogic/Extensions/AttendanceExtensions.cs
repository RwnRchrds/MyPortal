using MyPortal.Data.Models;

namespace MyPortal.BusinessLogic.Extensions
{
    public static class AttendanceExtensions
    {
        public static string GetTimeDisplay(this Period period)
        {
            var time = $"{period.StartTime.ToString(@"hh\:mm")} - {period.EndTime.ToString(@"hh\:mm")}";

            return time;
        }
    }
}