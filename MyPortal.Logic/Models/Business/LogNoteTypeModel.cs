using System;
using System.ComponentModel.DataAnnotations;
using MyPortal.Logic.Constants;

namespace MyPortal.Logic.Models.Business
{
    public class LogNoteTypeModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(128)] 
        public string ColourCode { get; set; }

        public string GetIcon()
        {
            if (Id == LogNoteTypes.AcademicSupport)
            {
                return "fa-comments";
            }

            if (Id == LogNoteTypes.Behaviour)
            {
                return "fa-exclamation-triangle";
            }

            if (Id == LogNoteTypes.MedEvent)
            {
                return "fa-first-aid";
            }

            if (Id == LogNoteTypes.Praise)
            {
                return "fa-smile";
            }

            if (Id == LogNoteTypes.Report)
            {
                return "fa-clipboard";
            }

            if (Id == LogNoteTypes.SenNote)
            {
                return "fa-hands-helping";
            }

            if (Id == LogNoteTypes.StudentFeed)
            {
                return "fa-user-graduate";
            }

            if (Id == LogNoteTypes.TutorNote)
            {
                return "fa-user-tie";
            }

            return string.Empty;
        }
    }
}
