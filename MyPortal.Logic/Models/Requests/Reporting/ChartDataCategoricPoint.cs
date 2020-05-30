namespace MyPortal.Logic.Models.Requests.Reporting
{
    public class ChartDataCategoricPoint
    {
        public string X { get; set; }
        public double Y { get; set; }

        public ChartDataCategoricPoint(string x, double y)
        {
            X = x;
            Y = y;
        }
    }
}