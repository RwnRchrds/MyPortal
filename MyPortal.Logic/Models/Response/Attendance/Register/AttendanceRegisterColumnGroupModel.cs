using System.Collections.Generic;

namespace MyPortal.Logic.Models.Response.Attendance.Register;

public class AttendanceRegisterColumnGroupModel
{
    public AttendanceRegisterColumnGroupModel()
    {
        Columns = new List<AttendanceRegisterColumnModel>();
    }
    
    public string Header { get; set; }
    public int Order { get; set; }
    public ICollection<AttendanceRegisterColumnModel> Columns { get; set; }
}