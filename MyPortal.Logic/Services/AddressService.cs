using System;
using System.Collections.Generic;
using System.Text;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Services
{
    public class AddressService : BaseService, IAddressService
    {
        public AddressService() : base("Address")
        {
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
