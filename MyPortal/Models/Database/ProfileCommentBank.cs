namespace MyPortal.Models.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A set of comments that can be used to create log notes.
    /// </summary>
    [Table("Profile_CommentBanks")]
    public partial class ProfileCommentBank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProfileCommentBank()
        {
            ProfileComments = new HashSet<ProfileComment>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool System { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProfileComment> ProfileComments { get; set; }
    }
}
