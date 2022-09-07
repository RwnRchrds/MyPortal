using System.Collections.Generic;
using System.Linq;
using MyPortal.Logic.Interfaces;

namespace MyPortal.Logic.Models.Reporting;

public class ChartSeries<TDataPoint> where TDataPoint : IDataPoint
{
    public string SeriesName { get; set; }
    private List<TDataPoint> _dataPoints;

    public TDataPoint[] DataPoints
    {
        get { return _dataPoints.ToArray(); }
    }

    public ChartSeries(string seriesName)
    {
        SeriesName = seriesName;
    }

    public ChartSeries(string seriesName, IEnumerable<TDataPoint> dataPoints) : this(seriesName)
    {
        _dataPoints = dataPoints.ToList();
    }

    public void AddPoint(TDataPoint newPoint)
    {
        _dataPoints.Add(newPoint);
    }
}