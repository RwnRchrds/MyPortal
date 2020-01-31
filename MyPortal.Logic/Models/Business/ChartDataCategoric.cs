namespace MyPortal.Logic.Models.Business
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
}