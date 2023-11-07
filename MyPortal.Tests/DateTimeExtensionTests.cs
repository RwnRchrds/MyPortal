using System;
using MyPortal.Logic.Enums;
using MyPortal.Logic.Extensions;
using NUnit.Framework;

namespace MyPortal.Tests;

[TestFixture]
public class DateTimeExtensionTests
{
    [Test]
    [TestCase("2022-8-10", DayOfWeek.Monday, "2022-8-8")]
    [TestCase("2022-8-10", DayOfWeek.Sunday, "2022-8-14")]
    public void GetDayOfWeek_ReturnsCorrectDate(DateTime input, DayOfWeek dayOfWeek, DateTime expected)
    {
        var result = input.GetDayOfWeek(dayOfWeek);

        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    [TestCase("2022-11-07", EventFrequency.Daily, "2022-11-08")]
    [TestCase("2022-11-07", EventFrequency.Weekly, "2022-11-14")]
    [TestCase("2022-11-07", EventFrequency.BiWeekly, "2022-11-21")]
    [TestCase("2022-11-07", EventFrequency.Monthly, "2022-12-07")]
    [TestCase("2022-11-07", EventFrequency.BiMonthly, "2023-01-07")]
    [TestCase("2022-11-07", EventFrequency.Annually, "2023-11-07")]
    [TestCase("2022-11-07", EventFrequency.BiAnnually, "2024-11-07")]
    public void GetNextOccurrence_ReturnsCorrectDate(DateTime input, EventFrequency frequency, DateTime expected)
    {
        var result = input.GetNextOccurrence(frequency);

        Assert.That(result, Is.EqualTo(expected));
    }
}