namespace MyPortal.Logic.Models.Reporting
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