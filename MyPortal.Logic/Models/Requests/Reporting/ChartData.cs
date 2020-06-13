using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyPortal.Logic.Extensions;

namespace MyPortal.Logic.Models.Requests.Reporting
{
    public class ChartData
    {
        private List<ChartDataPoint> _dataPoints;
        private bool _numericDataPoints;

        public ChartData(IEnumerable<ChartDataPoint> dataPoints)
        {
            var testPoint = dataPoints.FirstOrDefault();

            _numericDataPoints = testPoint is NumericChartDataPoint;
        }

        public void AddPoint(ChartDataPoint dataPoint)
        {
            if (_numericDataPoints && dataPoint is CategoricChartDataPoint ||
                !_numericDataPoints && dataPoint is NumericChartDataPoint)
            {
                throw new Exception("Incorrect data point type.");
            }

            _dataPoints.Add(dataPoint);
        }

        public List<T> GetData<T>() where T : ChartDataPoint
        {
            if (_numericDataPoints && typeof(T) == typeof(CategoricChartDataPoint) ||
                !_numericDataPoints && typeof(T) == typeof(NumericChartDataPoint))
            {
                throw new Exception("Incorrect output type.");
            }

            return _dataPoints.OfType<T>().ToList();
        }
    }
}
