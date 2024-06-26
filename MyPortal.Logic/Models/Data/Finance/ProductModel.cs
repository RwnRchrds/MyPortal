﻿using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Structures;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Models.Data.Finance
{
    public class ProductModel : BaseModelWithLoad
    {
        public ProductModel(Product model) : base(model)
        {
            LoadFromModel(model);
        }

        private void LoadFromModel(Product model)
        {
            ProductTypeId = model.ProductTypeId;
            VatRateId = model.VatRateId;
            Name = model.Name;
            Description = model.Description;
            Price = model.Price;
            ShowOnStore = model.ShowOnStore;
            OrderLimit = model.OrderLimit;
            Deleted = model.Deleted;

            if (model.Type != null)
            {
                Type = new ProductTypeModel(model.Type);
            }

            if (model.VatRate != null)
            {
                VatRate = new VatRateModel(model.VatRate);
            }
        }

        public Guid ProductTypeId { get; set; }

        public Guid VatRateId { get; set; }

        [Required] [StringLength(128)] public string Name { get; set; }

        [Required] [StringLength(256)] public string Description { get; set; }

        public decimal Price { get; set; }

        public bool ShowOnStore { get; set; }

        [Range(0, Int32.MaxValue)] public int OrderLimit { get; set; }

        public bool Deleted { get; set; }

        public virtual ProductTypeModel Type { get; set; }

        public virtual VatRateModel VatRate { get; set; }

        protected override async Task LoadFromDatabase(IUnitOfWork unitOfWork)
        {
            if (Id.HasValue)
            {
                var model = await unitOfWork.Products.GetById(Id.Value);

                LoadFromModel(model);
            }
        }
    }
}