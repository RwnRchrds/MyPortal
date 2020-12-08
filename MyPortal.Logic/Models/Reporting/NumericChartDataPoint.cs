using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Models.Reporting
{
    public class NumericChartDataPoint : IDataPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public NumericChartDataPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}