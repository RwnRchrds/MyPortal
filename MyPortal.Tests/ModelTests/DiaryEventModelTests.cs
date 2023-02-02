using System;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Extensions;
using MyPortal.Logic.Models.Data.Calendar;
using NUnit.Framework;

namespace MyPortal.Tests.ModelTests;

[TestFixture]
public class DiaryEventModelTests
{
    [Test]
    [TestCase(EventFrequency.Single, 1)]
    [TestCase(EventFrequency.Daily, 24)]
    [TestCase(EventFrequency.Weekly, 4)]
    public void CreateSeries_CreatesCorrectAmount(EventFrequency frequency, int expectedResult)
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

        var series = model.CreateSeries(frequency, endDate);
        
        Assert.That(series, Has.Count.EqualTo(expectedResult));
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
}