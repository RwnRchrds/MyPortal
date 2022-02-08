using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Models.Reporting
{
    public class ChartData<TDataPoint> where TDataPoint : IDataPoint
    {
        public string ChartTitle { get; set; }
        public string XLabel { get; set; }
        public string YLabel { get; set; }

        private List<TDataPoint> _dataPoints;

        public TDataPoint[] DataPoints
        {
            get { return _dataPoints.ToArray(); }
        }

        public ChartData(string title, string xLabel, string yLabel, IEnumerable<TDataPoint> dataPoints) : this(
            dataPoints)
        {
            ChartTitle = title;
            XLabel = xLabel;
            YLabel = yLabel;
        }

        public ChartData(IEnumerable<TDataPoint> dataPoints)
        {
            _dataPoints = dataPoints.ToList();
        }

        public void AddPoint(TDataPoint newPoint)
        {
            _dataPoints.Add(newPoint);
        }
    }
}
