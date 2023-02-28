using System;
using System.Threading.Tasks;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Helpers;

namespace MyPortal.Logic.Services;

public class BaseService
{
    public BaseService()
    {
        
    }
    
    public void Validate<T>(T model)
    {
        ValidationHelper.ValidateModel(model);
    }
}