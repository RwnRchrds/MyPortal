using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.BusinessLogic.Dtos;

namespace MyPortal.BusinessLogic.Interfaces
{
    public interface IPersonDto
    {
        PersonDto Person { get; set; }
    }
}
