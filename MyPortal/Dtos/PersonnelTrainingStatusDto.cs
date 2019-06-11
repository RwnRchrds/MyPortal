namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// The status of completion of a training course by a member of staff.
    /// </summary>
    public partial class PersonnelTrainingStatusDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
