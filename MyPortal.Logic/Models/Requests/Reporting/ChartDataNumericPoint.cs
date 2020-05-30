namespace MyPortal.Logic.Models.Requests.Reporting
{
    public class ChartDataNumericPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public ChartDataNumericPoint(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}