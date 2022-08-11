using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Models.Reporting
{
    public class CategoricalChartDataPoint : IDataPoint
    {
        public string X { get; set; }
        public double Y { get; set; }

        public CategoricalChartDataPoint(string x, double y)
        {
            X = x;
            Y = y;
        }
    }
}