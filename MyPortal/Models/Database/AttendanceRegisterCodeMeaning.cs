namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// [SYSTEM] Meanings of register codes in the system.
    /// </summary>
    [Table("Attendance_RegisterCodeMeanings")]
    public partial class AttendanceRegisterCodeMeaning
    {
        //THIS IS A SYSTEM CLASS AND SHOULD NOT HAVE FEATURES TO ADD, MODIFY OR DELETE DATABASE OBJECTS
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AttendanceRegisterCodeMeaning()
        {
            AttendanceRegisterCodes = new HashSet<AttendanceRegisterCode>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        [StringLength(50)] public string Code { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AttendanceRegisterCode> AttendanceRegisterCodes { get; set; }
    }
}
