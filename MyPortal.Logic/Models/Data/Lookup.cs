using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Mappers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPortal.Logic.Models.Data
{
    public class Lookup : Dictionary<string, Guid>
    {
        public Lookup()
        {

        }

        public Lookup(Dictionary<string, Guid> dictionary) : base(dictionary)
        {

        }
    }
}
