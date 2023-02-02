using System.Collections.Generic;

namespace MyPortal.Logic.Models.Data.Attendance.Register;

public class AttendanceRegisterColumnGroupDataModel
{
    public AttendanceRegisterColumnGroupDataModel()
    {
        Columns = new List<AttendanceRegisterColumnDataModel>();
    }
    
    public string Header { get; set; }
    public int Order { get; set; }
    public ICollection<AttendanceRegisterColumnDataModel> Columns { get; set; }
}