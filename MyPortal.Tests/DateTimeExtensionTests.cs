using System;
using System.Diagnostics;
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
}