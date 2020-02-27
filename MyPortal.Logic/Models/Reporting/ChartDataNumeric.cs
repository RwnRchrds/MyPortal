namespace MyPortal.Logic.Models.Reporting
{
    public class ChartDataNumeric
    {
        public double X { get; set; }
        public double Y { get; set; }

        public ChartDataNumeric(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}