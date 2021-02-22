using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Database.Interfaces;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;

namespace MyPortal.Logic.Services
{
    public class AddressService : BaseService, IAddressService
    {
        public AddressService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
