using System;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Services
{
    public class IncidentService : BaseService, IIncidentService
    {
        public IncidentService(string objectName) : base(objectName)
        {
        }

        public override void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}