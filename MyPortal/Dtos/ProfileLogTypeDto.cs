namespace MyPortal.Dtos
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// [SYSTEM] A category of log notes for students.
    /// </summary>
    public partial class ProfileLogTypeDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FaIcon { get; set; }

        public string HexColour { get; set; }
        public bool System { get; set; }
    }
}
