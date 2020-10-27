using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Permissions
{
    public partial class Permissions
    {
        public static class Assessment
        {
            public static class Aspects
            {
                public static Guid ViewAspects = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6307");
                public static Guid EditAspects = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6308");
            }

            public static class Examinations
            {
                public static Guid ViewExamBasedata = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6309");
                public static Guid EditExamBasedata = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC630A");
                public static Guid RunExamAssistant = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC630B");
            }

            public static class GradeSets
            {
                public static Guid ViewGradeSets = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC630C");
                public static Guid EditGradeSets = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC630D");
            }

            public static class MarksheetTemplates
            {
                public static Guid ViewMarksheetTemplates = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC630E");
                public static Guid EditMarksheetTemplates = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC630F");
            }

            public static class Marksheets
            {
                public static Guid ViewOwnMarksheets = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6312");
                public static Guid ViewAllMarksheets = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6313");
                public static Guid UpdateOwnMarksheets = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6314");
                public static Guid UpdateAllMarksheets = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6315");
            }

            public static class ResultSets
            {
                public static Guid ViewResultSets = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6310");
                public static Guid EditResultSets = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6311");
            }

            public static class Results
            {
                public static Guid ViewResults = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6316");
                public static Guid ViewEmbargoedResults = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6317");
                public static Guid EditResults = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6318");
            }
        }
    }
}
