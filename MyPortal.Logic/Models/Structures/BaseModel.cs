using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Attributes;

namespace MyPortal.Logic.Models.Structures
{
    public abstract class BaseModel
    {
        protected BaseModel(IEntity model)
        {
            Id = model.Id;
        }

        protected BaseModel()
        {
            
        }

        public Guid? Id { get; set; }
    }

    public abstract class BaseModelWithLoad : BaseModel
    {
        protected BaseModelWithLoad(IEntity model) : base(model)
        {
            Id = model.Id;
        }

        protected BaseModelWithLoad()
        {
            
        }
        
        protected abstract Task LoadFromDatabase(IUnitOfWork unitOfWork);

        internal virtual async Task Load(IUnitOfWork unitOfWork)
        {
            await LoadFromDatabase(unitOfWork);
            await EagerLoadItems(unitOfWork);
        }

        protected virtual async Task EagerLoadItems(IUnitOfWork unitOfWork)
        {
            var eagerProperties = GetType().GetProperties()
                .Where(p => p.IsDefined(typeof(EagerLoadAttribute), false));

            foreach (var eagerProperty in eagerProperties)
            {
                var value = eagerProperty.GetValue(this);

                if (value is BaseModelWithTreeLoad treeLoadable)
                {
                    var eagerLoad = (EagerLoadAttribute)eagerProperty.GetCustomAttribute(typeof(EagerLoadAttribute));
                    await treeLoadable.Load(unitOfWork, eagerLoad?.Deep ?? false);
                }
                else if (value is BaseModelWithLoad loadable)
                {
                    await loadable.Load(unitOfWork);
                }
            }
        }
    }

    public abstract class BaseModelWithTreeLoad : BaseModel
    {
        protected BaseModelWithTreeLoad(IEntity model) : base(model)
        {
            Id = model.Id;
        }
        
        protected virtual BaseModelWithTreeLoad ParentModel { get; set; }
        
        protected abstract Task LoadFromDatabase(IUnitOfWork unitOfWork);

        internal virtual async Task Load(IUnitOfWork unitOfWork, bool deep = false)
        {
            await LoadFromDatabase(unitOfWork);
            await EagerLoadItems(unitOfWork);

            if (deep && ParentModel != null)
            {
                await ParentModel.Load(unitOfWork, true);
            }
        }

        protected virtual async Task EagerLoadItems(IUnitOfWork unitOfWork)
        {
            var eagerProperties = GetType().GetProperties()
                .Where(p => p.IsDefined(typeof(EagerLoadAttribute), false));

            foreach (var eagerProperty in eagerProperties)
            {
                var value = eagerProperty.GetValue(this);

                if (value is BaseModelWithTreeLoad treeLoadable)
                {
                    var eagerLoad = (EagerLoadAttribute)eagerProperty.GetCustomAttribute(typeof(EagerLoadAttribute));
                    await treeLoadable.Load(unitOfWork, eagerLoad?.Deep ?? false);
                }
                else if (value is BaseModelWithLoad loadable)
                {
                    await loadable.Load(unitOfWork);
                }
            }
        }
    }
}
