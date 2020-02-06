﻿using System;

namespace MyPortal.Logic.Models.Dtos.DataGrid
{
    public class DataGridAchievementDto
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Location { get; set; }
        public string RecordedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comments { get; set; }
        public int Points { get; set; }
    }
}