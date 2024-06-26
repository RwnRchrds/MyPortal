﻿using System;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Models.Data.Students;

namespace MyPortal.Logic.Models.Summary
{
    public class StudentIncidentSummaryModel
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; }
        public string RoleName { get; set; }
        public string StudentName { get; set; }
        public string Location { get; set; }
        public string RecordedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comments { get; set; }
        public int Points { get; set; }

        private StudentIncidentSummaryModel(StudentIncidentModel model)
        {
            if (model.Id.HasValue)
            {
                Id = model.Id.Value;
            }

            TypeName = model.Incident.Type.Description;
            RoleName = model.RoleType.Description;
            StudentName = model.Student.Person.GetName();
            Location = model.Incident.Location.Description;
            RecordedBy = model.Incident.CreatedBy.GetDisplayName(NameFormat.FullNameAbbreviated);
            CreatedDate = model.Incident.CreatedDate;
            Comments = model.Incident.Comments;
            Points = model.Points;
        }

        public static async Task<StudentIncidentSummaryModel> GetSummary(IUnitOfWork unitOfWork,
            StudentIncidentModel model)
        {
            await model.Incident.Load(unitOfWork);
            await model.Student.Load(unitOfWork);

            return new StudentIncidentSummaryModel(model);
        }
    }
}