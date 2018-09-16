using System;

namespace MyPortal.Dtos
{
    public class DocumentDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public bool IsGeneral { get; set; }
        public DateTime Date { get; set; }
        public bool Approved { get; set; }
    }
}