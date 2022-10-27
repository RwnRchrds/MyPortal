using System;
using MyPortal.Logic.Models.Data;
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

        bool result = a.Overlaps(b);
        
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

        bool result = a.Overlaps(b);
        
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

        bool result = a.Overlaps(b);
        
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

        bool result = a.Overlaps(b);
        
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

        bool result = a.Overlaps(b);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void Overlaps_DetectsAdjacent()
    {
        // A: |---------------|
        // B:                 |---------------|

        
        // 09:00 - 10:00
        var a = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(10));
        
        // 10:00 - 11:00
        var b = new DateRange(DateTime.Today.AddHours(10), DateTime.Today.AddHours(11));

        bool result = a.Overlaps(b);
        
        Assert.That(result, Is.True);
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

        bool result = a.Overlaps(b);
        
        Assert.That(result, Is.False);
    }
}