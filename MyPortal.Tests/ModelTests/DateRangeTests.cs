using System;
using System.Collections.Generic;
using MyPortal.Logic.Models.Structures;
using NUnit.Framework;
using Org.BouncyCastle.Asn1.X509;

namespace MyPortal.Tests.ModelTests;

[TestFixture]
public class DateRangeTests
{
    [Test]
    public void Overlaps_DetectsStartBeforeEnd()
    {
        // A:       |---------------|
        // B: |---------------|

        
        // 10:30 - 15:00
        var a = new DateRange(DateTime.Today.AddHours(10.5), DateTime.Today.AddHours(15));
        
        // 09:00 - 13:00
        var b = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(13));

        bool result = a.Overlaps(b, false);
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void Overlaps_DetectsEndAfterStart()
    {
        // A: |---------------|
        // B:         |---------------|

        // 09:00 - 13:00
        var a = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(13));
        
        // 10:30 - 15:00
        var b = new DateRange(DateTime.Today.AddHours(10.5), DateTime.Today.AddHours(15));

        bool result = a.Overlaps(b, false);
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void Overlaps_DetectsStartsBeforeEndAfter()
    {
        // A: |------------------------------|
        // B:           |-------|            

        // 09:00 - 18:00
        var a = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(18));
        
        // 13:00 - 14:00
        var b = new DateRange(DateTime.Today.AddHours(13), DateTime.Today.AddHours(14));

        bool result = a.Overlaps(b, false);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void Overlaps_DetectsDuring()
    {
        // A:           |-------|
        // B: |------------------------------|

        // 13:00 - 14:00
        var a = new DateRange(DateTime.Today.AddHours(13), DateTime.Today.AddHours(14));
        
        // 09:00 - 18:00
        var b = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(18));

        bool result = a.Overlaps(b, false);
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void Overlaps_DetectsExact()
    {
        // A: |---------------|
        // B: |---------------|
        
        // 09:00 - 12:00
        var a = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(12));
        
        // 09:00 - 12:00
        var b = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(12));

        bool result = a.Overlaps(b, false);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    [TestCase(false, false)]
    [TestCase(true, true)]
    public void Overlaps_DetectsAdjacent(bool includeAdjacent, bool expectedResult)
    {
        // A: |---------------|
        // B:                 |---------------|

        
        // 09:00 - 10:00
        var a = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(10));
        
        // 10:00 - 11:00
        var b = new DateRange(DateTime.Today.AddHours(10), DateTime.Today.AddHours(11));

        bool result = a.Overlaps(b, includeAdjacent);
        
        Assert.That(result, Is.EqualTo(expectedResult));
    }

    [Test]
    public void Overlaps_IgnoresSeparate()
    {
        // A: |---------------|
        // B:                       |---------------|

        // 09:00 - 10:00
        var a = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(10));
        
        // 12:00 - 13:00
        var b = new DateRange(DateTime.Today.AddHours(12), DateTime.Today.AddHours(13));

        bool result = a.Overlaps(b, true);
        
        Assert.That(result, Is.False);
    }

    [Test]
    public void Coalesce_JoinsAdjacentRanges()
    {
        var a = new DateRange(new DateTime(2023, 04, 25), new DateTime(2023, 04, 26));
        var b = new DateRange(new DateTime(2023, 04, 26), new DateTime(2023, 04, 27));
        
        a.Coalesce(b);
        
        Assert.That(a.AfterEnd, Is.EqualTo(b));
    }

    [Test]
    public void Coalesce_ThrowsIfNotAdjacent()
    {
        var a = new DateRange(new DateTime(2023, 04, 25), new DateTime(2023, 04, 26));
        var b = new DateRange(new DateTime(2023, 04, 27), new DateTime(2023, 04, 28));
        
        Assert.That(() => a.Coalesce(b), Throws.ArgumentException);
    }

    [Test]
    public void MoveToStart_AdjustsDates()
    {
        var a = new DateRange(new DateTime(2023, 04, 25), new DateTime(2023, 04, 26));
        
        a.MoveToStart(new DateTime(2023, 04, 26));
        
        Assert.That(a.Start, Is.EqualTo(new DateTime(2023, 04, 26)));
        Assert.That(a.End, Is.EqualTo(new DateTime(2023, 04, 27)));
    }

    [Test]
    public void MoveToStart_AdjustsCoalescedDates()
    {
        var a = new DateRange(new DateTime(2023, 04, 25), new DateTime(2023, 04, 26));
        var b = new DateRange(new DateTime(2023, 04, 26), new DateTime(2023, 04, 27));
        var c = new DateRange(new DateTime(2023, 04, 27), new DateTime(2023, 04, 28));
        
        a.Coalesce(b);
        b.Coalesce(c);

        b.Move(TimeSpan.FromDays(-1));
        
        Assert.That(a.Start, Is.EqualTo(new DateTime(2023, 04, 24)));
        Assert.That(a.End, Is.EqualTo(new DateTime(2023, 04, 25)));
        
        Assert.That(c.Start, Is.EqualTo(new DateTime(2023, 04, 26)));
        Assert.That(c.End, Is.EqualTo(new DateTime(2023, 04, 27)));
    }

    [Test]
    public void TestMe()
    {
        var monday = new DateRange(new DateTime(2023, 04, 24), new DateTime(2023, 04, 25));

        var otherDays = new List<DateRange>
        {
            new (new DateTime(2023, 04, 25), new DateTime(2023, 04, 26)),
            new (new DateTime(2023, 04, 26), new DateTime(2023, 04, 27)),
            new (new DateTime(2023, 04, 27), new DateTime(2023, 04, 28)),
            new (new DateTime(2023, 04, 28), new DateTime(2023, 04, 29))
        };

        foreach (var otherDay in otherDays)
        {
            otherDay.TryCoalesce(monday);
        }

        monday.MoveToStart(new DateTime(2023, 06, 14));
    }
}