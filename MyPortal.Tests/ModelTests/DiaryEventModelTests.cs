using System;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Models.Entity;
using NUnit.Framework;

namespace MyPortal.Tests.ModelTests;

[TestFixture]
public class DiaryEventModelTests
{
    [Test]
    public void CreateSeries_Single()
    {
        var now = DateTime.Now;
        var endDate = now.AddDays(23);
        
        var diaryEvent = new DiaryEvent
        {
            Subject = "Test Event",
            StartTime = now,
            EndTime = now.AddHours(1)
        };

        var model = new DiaryEventModel(diaryEvent);

        var series = model.CreateSeries(EventFrequency.Single, endDate);
        
        Assert.That(series, Has.Count.EqualTo(1));
    }

    [Test]
    public void CreateSeries_Daily()
    {
        var now = DateTime.Now;
        var endDate = now.AddDays(23);
        
        var diaryEvent = new DiaryEvent
        {
            Subject = "Test Event",
            StartTime = now,
            EndTime = now.AddHours(1)
        };

        var model = new DiaryEventModel(diaryEvent);

        var series = model.CreateSeries(EventFrequency.Daily, endDate);
        
        Assert.That(series, Has.Count.EqualTo(24));
    }

    [Test]
    public void CreateSeries_AllDay_Daily()
    {
        var now = DateTime.Now;
        var endDate = now.AddDays(23);
        
        var diaryEvent = new DiaryEvent
        {
            Subject = "Test Event",
            StartTime = now.Date,
            EndTime = now.GetEndOfDay()
        };

        var model = new DiaryEventModel(diaryEvent);

        var series = model.CreateSeries(EventFrequency.Daily, endDate);
        
        Assert.That(series, Has.Count.EqualTo(24));
    }

    [Test]
    public void CreateSeries_Weekly()
    {
        var now = DateTime.Now;
        var endDate = now.AddDays(23);

        var diaryEvent = new DiaryEvent
        {
            Subject = "Test Event",
            StartTime = now,
            EndTime = now.AddHours(1)
        };

        var model = new DiaryEventModel(diaryEvent);

        var series = model.CreateSeries(EventFrequency.Weekly, endDate);
        
        Assert.That(series, Has.Count.EqualTo(4));
    }
}