namespace MyPortal.Database.Models.Filters
{
    public class PageFilter
    {
        public PageFilter()
        {
        }

        public PageFilter(int skip, int take)
        {
            Skip = skip;
            Take = take;
        }

        public int Skip { get; set; }
        public int Take { get; set; }
    }
}