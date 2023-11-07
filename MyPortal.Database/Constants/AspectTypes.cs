using System;

namespace MyPortal.Database.Constants
{
    public class AspectTypes
    {
        public static Guid Grade { get; } = Guid.Parse("84F43913-ED25-4839-B130-62AC605DEBFA");
        public static Guid MarkInteger { get; } = Guid.Parse("84F43913-ED25-4839-B130-62AC605DEBFB");
        public static Guid MarkDecimal { get; } = Guid.Parse("84F43913-ED25-4839-B130-62AC605DEBFC");
        public static Guid Comment { get; } = Guid.Parse("84F43913-ED25-4839-B130-62AC605DEBFD");
    }
}