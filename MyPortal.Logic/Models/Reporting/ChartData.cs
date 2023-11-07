using System.Collections.Generic;
using System.Linq;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Models.Reporting
{
    public class ChartData<TDataPoint> where TDataPoint : IDataPoint
    {
        public string Title { get; set; }
        public string XLabel { get; set; }
        public string YLabel { get; set; }

        private List<ChartSeries<TDataPoint>> _series;

        public ChartSeries<TDataPoint>[] Series
        {
            get { return _series.ToArray(); }
        }

        public ChartData(string title, string xLabel, string yLabel)
        {
            Title = title;
            XLabel = xLabel;
            YLabel = yLabel;
        }

        public ChartData(string title, string xLabel, string yLabel, IEnumerable<ChartSeries<TDataPoint>> series) :
            this(title, xLabel, yLabel)
        {
            _series = series.ToList();
        }

        public void AddSeries(ChartSeries<TDataPoint> series)
        {
            _series.Add(series);
        }
    }
}