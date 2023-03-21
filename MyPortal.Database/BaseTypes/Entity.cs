using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.BaseTypes
{
    public abstract class Entity : IEntity
    {
        [Column(Order = 0)]
        public Guid Id { get; set; }
        
        [Column(Order = 1)] 
        public int ClusterId { get; set; }
    }
}
