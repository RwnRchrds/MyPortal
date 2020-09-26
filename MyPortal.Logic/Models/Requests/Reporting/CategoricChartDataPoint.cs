namespace MyPortal.Logic.Models.Requests.Reporting
{
    public class CategoricChartDataPoint : ChartDataPoint
    {
        public string X { get; set; }

        public CategoricChartDataPoint(string x, double y)
        {
            X = x;
            Y = y;
        }
    }
}