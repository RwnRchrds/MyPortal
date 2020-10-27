using System;
using System.Collections.Generic;
using System.Text;

namespace MyPortal.Database.Permissions
{
    public partial class Permissions
    {
        public static class Finance
        {
            public static class Accounts
            {
                public static Guid ViewAccounts = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC632F");
                public static Guid EditAccounts = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6330");
            }

            public static class Bills
            {
                public static Guid ViewBills = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6333");
                public static Guid EditBills = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6334");
            }

            public static class Products
            {
                public static Guid ViewProducts = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6331");
                public static Guid EditProducts = Guid.Parse("F9F0F415-B47C-4C53-B897-D5964FBC6332");
            }
        }
    }
}
