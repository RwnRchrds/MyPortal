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
        private List<TDataPoint> _dataPoints;

        public TDataPoint[] DataPoints
        {
            get { return _dataPoints.ToArray(); }
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
