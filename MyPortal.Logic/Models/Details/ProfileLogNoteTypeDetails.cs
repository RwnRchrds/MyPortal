using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Logic.Dictionaries;

namespace MyPortal.Logic.Models.Details
{
    public class ProfileLogNoteTypeDetails
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(128)] 
        public string ColourCode { get; set; }

        public string GetIcon()
        {
            if (Id == ProfileLogNoteTypeDictionary.AcademicSupport)
            {
                return "fa-comments";
            }

            if (Id == ProfileLogNoteTypeDictionary.Behaviour)
            {
                return "fa-exclamation-triangle";
            }

            if (Id == ProfileLogNoteTypeDictionary.MedEvent)
            {
                return "fa-first-aid";
            }

            if (Id == ProfileLogNoteTypeDictionary.Praise)
            {
                return "fa-smile";
            }

            if (Id == ProfileLogNoteTypeDictionary.Report)
            {
                return "fa-clipboard";
            }

            if (Id == ProfileLogNoteTypeDictionary.SenNote)
            {
                return "fa-hands-helping";
            }

            if (Id == ProfileLogNoteTypeDictionary.StudentFeed)
            {
                return "fa-user-graduate";
            }

            if (Id == ProfileLogNoteTypeDictionary.TutorNote)
            {
                return "fa-user-tie";
            }

            return string.Empty;
        }
    }
}
