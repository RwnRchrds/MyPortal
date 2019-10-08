namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// A set of comments that can be used to create log notes.
    /// </summary>
    
    public partial class ProfileCommentBankDto
    {
        public int Id { get; set; }

        
        public string Name { get; set; }

        public bool System { get; set; }

        
        
    }
}
