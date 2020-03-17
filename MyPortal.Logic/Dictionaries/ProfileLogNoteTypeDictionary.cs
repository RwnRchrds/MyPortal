using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace MyPortal.Logic.Dictionaries
{
    public class ProfileLogNoteTypeDictionary
    {
        public static Guid AcademicSupport = Guid.Parse("C6C718BE-8255-4D26-96C1-3B92815F358E");
        public static Guid Behaviour = Guid.Parse("C6C728BE-8255-4D26-96C1-3B92815F358E");
        public static Guid MedEvent = Guid.Parse("C6C738BE-8255-4D26-96C1-3B92815F358E");
        public static Guid Praise = Guid.Parse("C6C748BE-8255-4D26-96C1-3B92815F358E");
        public static Guid Report = Guid.Parse("C6C758BE-8255-4D26-96C1-3B92815F358E");
        public static Guid SenNote = Guid.Parse("C6C768BE-8255-4D26-96C1-3B92815F358E");
        public static Guid StudentFeed = Guid.Parse("C6C778BE-8255-4D26-96C1-3B92815F358E");
        public static Guid TutorNote = Guid.Parse("C6C788BE-8255-4D26-96C1-3B92815F358E");
    }
}
