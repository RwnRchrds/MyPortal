using System;

namespace MyPortal.Database.Models.Search
{
    public class DetentionSearchOptions
    {
        public Guid? DetentionType { get; set; }
        public DateTime? FirstDate { get; set; }
        public DateTime? LastDate { get; set; }
    }
}