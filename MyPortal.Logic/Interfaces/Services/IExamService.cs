using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Requests.Assessment;

namespace MyPortal.Logic.Interfaces.Services;

public interface IExamService
{
    Task CreateResultEmbargo(params CreateResultEmbargoModel[] models);
}