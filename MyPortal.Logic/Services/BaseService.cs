using MyPortal.Logic.Helpers;

namespace MyPortal.Logic.Services;

public class BaseService
{
    public void Validate<T>(T model)
    {
        ValidationHelper.ValidateModel(model);
    }
}