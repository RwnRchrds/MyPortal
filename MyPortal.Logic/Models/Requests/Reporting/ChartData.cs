using System;
using System.Collections.Generic;
using System.Linq;

namespace MyPortal.Logic.Models.Requests.Reporting
{
    public class ChartData
    {
        private List<ChartDataPoint> _dataPoints;
        private bool _numericData;

        public ChartData(IEnumerable<ChartDataPoint> dataPoints)
        {
            var input = dataPoints.ToList();

            if (input.All(x => x is NumericChartDataPoint))
            {
                _numericData = true;
            }
            else
            {
                if (!input.All(x => x is CategoricChartDataPoint))
                {
                    throw new Exception("Data points must be the same type when creating a chart data object.");
                }
            }

            _dataPoints.AddRange(input);
        }

        public void AddPoint(ChartDataPoint dataPoint)
        {
            if (_numericData && dataPoint is CategoricChartDataPoint ||
                !_numericData && dataPoint is NumericChartDataPoint)
            {
                throw new Exception("The data point is the incorrect type for this data collection.");
            }

            _dataPoints.Add(dataPoint);
        }

        public T[] GetData<T>() where T : ChartDataPoint
        {
            var outputData = _dataPoints.OfType<T>().ToArray();

            if (!outputData.Any())
            {
                throw new Exception($"The specified output type is incorrect for this data collection.");
            }

            return outputData;
        }
    }
}
