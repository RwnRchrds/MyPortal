namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// [SYSTEM] Meanings of register codes in the system.
    /// </summary>
    public partial class AttendanceRegisterCodeMeaningDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
    }
}
