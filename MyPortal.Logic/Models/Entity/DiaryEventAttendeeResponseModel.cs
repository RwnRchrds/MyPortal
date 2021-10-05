﻿using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class DiaryEventAttendeeResponseModel : LookupItemModel
    {
        public DiaryEventAttendeeResponseModel(DiaryEventAttendeeResponse model) : base(model)
        {
        }
    }
}