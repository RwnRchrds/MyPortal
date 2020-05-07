using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Drive.v3;
using Google.Apis.Services;

namespace MyPortal.Logic.Interfaces
{
    public interface IGoogleService
    {
        BaseClientService.Initializer GetInitializer();
    }
}
