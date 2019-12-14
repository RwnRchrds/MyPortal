namespace MyPortal.BusinessLogic.Models.Data
{
    public class ChartDataCategoric
    {
        public string X { get; set; }
        public double Y { get; set; }

        public ChartDataCategoric(string x, double y)
        {
            X = x;
            Y = y;
        }
    }

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