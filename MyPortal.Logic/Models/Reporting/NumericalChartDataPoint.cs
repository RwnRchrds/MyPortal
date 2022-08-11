using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Models.Reporting
{
    public class NumericalChartDataPoint : IDataPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public NumericalChartDataPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}