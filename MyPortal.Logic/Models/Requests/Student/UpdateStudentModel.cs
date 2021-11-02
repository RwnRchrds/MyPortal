﻿using System;
using MyPortal.Logic.Models.Requests.Person;

namespace MyPortal.Logic.Models.Requests.Student
{
    public class UpdateStudentModel : UpdatePersonModel
    {
        public new Guid Id { get; set; }
        
        public Guid? HouseId { get; set; }
        
        public Guid YearGroupId { get; set; }
        
        public Guid RegGroupId { get; set; }

        public DateTime? DateStarting { get; set; }

        public Guid? SenStatusId { get; set; }
        
        public Guid? SenTypeId { get; set; }
        
        public Guid? EnrolmentStatusId { get; set; }
        
        public Guid? BoarderStatusId { get; set; }
        
        public bool PupilPremium { get; set; }
    }
}