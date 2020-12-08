using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Models.Reporting
{
    public class CategoricChartDataPoint : IDataPoint
    {
        public string X { get; set; }
        public double Y { get; set; }

        public CategoricChartDataPoint(string x, double y)
        {
            X = x;
            Y = y;
        }
    }
}