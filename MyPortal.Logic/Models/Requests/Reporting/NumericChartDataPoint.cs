namespace MyPortal.Logic.Models.Requests.Reporting
{
    public class NumericChartDataPoint : ChartDataPoint
    {
        public double X { get; set; }

        public NumericChartDataPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}