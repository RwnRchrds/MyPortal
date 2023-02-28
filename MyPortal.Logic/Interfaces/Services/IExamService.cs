using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Requests.Assessment;

namespace MyPortal.Logic.Interfaces.Services;

public interface IExamService : IService
{
    Task CreateResultEmbargo(params ResultEmbargoRequestModel[] models);
    Task UpdateResultEmbargo(params ResultEmbargoRequestModel[] models);
}