using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class AgencyModel : BaseModel
    {
        public Guid TypeId { get; set; }

        public Guid? AddressId { get; set; }

        public Guid DirectoryId { get; set; }

        [StringLength(256)]
        public string Name { get; set; }

        [Url]
        [StringLength(100)]
        public string Website { get; set; }

        public bool Deleted { get; set; }

        public virtual AgencyTypeModel AgencyType { get; set; }
        public virtual AddressModel Address { get; set; }
        public virtual DirectoryModel Directory { get; set; }
    }
}
