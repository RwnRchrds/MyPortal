﻿namespace MyPortal.Logic.Models.ListModels
{
    public class TrainingCertificateListModel
    {
        public int CourseId { get; set; }
        public int StaffId { get; set; }
        public string CourseCode { get; set; }  
        public string CourseDescription { get; set; }
        public string Status { get; set; }
        public string StatusColourCode { get; set; }
    }
}