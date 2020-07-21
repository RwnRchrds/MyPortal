using System;
using MyPortal.Database.Interfaces;

namespace MyPortal.Database.Search
{
    public class DetentionSearchOptions
    {
        public Guid? DetentionType { get; set; }
        public DateTime? FirstDate { get; set; }
        public DateTime? LastDate { get; set; }
    }
}