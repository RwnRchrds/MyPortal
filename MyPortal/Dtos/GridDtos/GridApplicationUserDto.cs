using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyPortal.Interfaces;

namespace MyPortal.Dtos.GridDtos
{
    public class GridApplicationUserDto : IGridDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }
}