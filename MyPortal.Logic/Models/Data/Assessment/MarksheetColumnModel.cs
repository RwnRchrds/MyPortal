﻿using System;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Assessment
{
    public class MarksheetColumnModel : BaseModelWithLoad
    {
        public MarksheetColumnModel(MarksheetColumn model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(MarksheetColumn model)
        {
            TemplateId = model.TemplateId;
            AspectId = model.AspectId;
            ResultSetId = model.ResultSetId;
            DisplayOrder = model.DisplayOrder;
            ReadOnly = model.ReadOnly;

            if (model.Template != null)
            {
                Template = new MarksheetTemplateModel(model.Template);
            }

            if (model.Aspect != null)
            {
                Aspect = new AspectModel(model.Aspect);
            }

            if (model.ResultSet != null)
            {
                ResultSet = new ResultSetModel(model.ResultSet);
            }
        }

        public Guid TemplateId { get; set; }
        public Guid AspectId { get; set; }
        public Guid ResultSetId { get; set; }
        public int DisplayOrder { get; set; }
        public bool ReadOnly { get; set; }

        public virtual MarksheetTemplateModel Template { get; set; }
        public virtual AspectModel Aspect { get; set; }
        public virtual ResultSetModel ResultSet { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.MarksheetColumns.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}