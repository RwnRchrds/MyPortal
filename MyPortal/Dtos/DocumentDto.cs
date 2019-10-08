namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// Represents an online document in the system.
    /// </summary>
    
    public partial class DocumentDto
    {

        public int Id { get; set; }

        public int TypeId { get; set; }

        
        public string Description { get; set; }

        
        
        public string Url { get; set; }

        public int UploaderId { get; set; }

        
        public DateTime Date { get; set; }

        public bool IsGeneral { get; set; }

        public bool Approved { get; set; }

        public bool Deleted { get; set; }

        public virtual StaffMemberDto Uploader { get; set; }

        public virtual DocumentTypeDto DocumentType { get; set; }

        
        

    }
}
