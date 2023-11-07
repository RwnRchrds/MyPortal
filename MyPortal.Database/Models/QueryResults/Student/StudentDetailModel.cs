using System;

namespace MyPortal.Database.Models.QueryResults.Student;

public class StudentDetailModel
{
    public Guid Id { get; set; }

    public Guid PersonId { get; set; }

    public Guid? HouseId { get; set; }

    public Guid YearGroupId { get; set; }

    public Guid RegGroupId { get; set; }

    public int AdmissionNumber { get; set; }

    public DateTime? DateStarting { get; set; }

    public DateTime? DateLeaving { get; set; }

    public bool FreeSchoolMeals { get; set; }

    public Guid? SenStatusId { get; set; }

    public Guid? SenTypeId { get; set; }

    public Guid? EnrolmentStatusId { get; set; }

    public Guid? BoarderStatusId { get; set; }

    public bool PupilPremium { get; set; }

    public string Upn { get; set; }

    public bool Deleted { get; set; }
}