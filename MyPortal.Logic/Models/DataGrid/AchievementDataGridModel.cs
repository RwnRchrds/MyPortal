﻿using System;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Models.DataGrid
{
    public class AchievementDataGridModel
    {
        public Guid Id { get; set; }
        public string TypeName { get; set; }
        public string Location { get; set; }
        public string RecordedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comments { get; set; }
        public int Points { get; set; }

        public AchievementDataGridModel(AchievementModel model)
        {
            Id = model.Id;
            TypeName = model.Type.Description;
            Location = model.Location.Description;
            RecordedBy = model.RecordedBy.GetDisplayName(NameFormat.FullNameAbbreviated);
            CreatedDate = model.CreatedDate;
            Comments = model.Comments;
            Points = model.Points;
        }
    }
}