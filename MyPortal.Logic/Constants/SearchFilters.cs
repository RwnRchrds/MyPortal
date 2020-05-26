using System;

namespace MyPortal.Logic.Constants
{
    public static class SearchFilters
    {
        public static class Students
        {
            public static Guid OnRoll = new Guid("9BB8514C-A341-4461-8009-900C9E9388B2");
            public static Guid Leavers = new Guid("3E338ECE-FB61-4550-B182-B57585A9C789");
            public static Guid Future = new Guid("647F601E-E256-4182-ADFE-B25D6AE5C4D7");
        }

        public static class Tasks
        {
            public static Guid Active = new Guid("D8A51FBD-3193-443D-B1F1-E9F0D67D01E3");
            public static Guid Overdue = new Guid("10266DC3-6704-4078-9288-03BD1BCCD452");
            public static Guid Completed = new Guid("96C402B0-B997-429F-906C-9CCA8DD7EAB7");
        }

        public static class DocumentTypes
        {
            public static Guid Staff = new Guid("3EFB6FAB-C4A5-4812-AB93-CFF354247171");
            public static Guid Student = new Guid("FEE1373C-DF95-4872-B3BF-14FDEFAA7B55");
            public static Guid Contact = new Guid("99086E7B-C431-45D6-8DF5-C49DB365AC79");
            public static Guid General = new Guid("612EA702-2657-4494-928A-28A84A8B2A5E");
            public static Guid Sen = new Guid("8C82B413-961C-4B8E-84CB-19C39C419D1B");
            public static Guid Active = new Guid("6AD4F770-0269-4F06-AE34-34CCA8B5E5B1");
        }
    }
}