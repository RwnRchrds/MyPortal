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

        var a = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(13));
        var b = new DateRange(DateTime.Today.AddHours(10.5), DateTime.Today.AddHours(15));

        bool result = a.Overlaps(b);
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void Overlaps_DetectsEndAfterStart()
    {
        // A: |---------------|
        // B:         |---------------|

        var a = new DateRange(DateTime.Today.AddHours(10.5), DateTime.Today.AddHours(15));
        var b = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(13));

        bool result = a.Overlaps(b);
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void Overlaps_DetectsStartsBeforeEndAfter()
    {
        // A: |------------------------------|
        // B:           |-------|            

        var a = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(18));
        var b = new DateRange(DateTime.Today.AddHours(13), DateTime.Today.AddHours(14));

        bool result = a.Overlaps(b);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    public void Overlaps_DetectsDuring()
    {
        // A:           |-------|
        // B: |------------------------------|

        var a = new DateRange(DateTime.Today.AddHours(13), DateTime.Today.AddHours(14));
        var b = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(18));

        bool result = a.Overlaps(b);
        
        Assert.That(result, Is.True);
    }

    [Test]
    public void Overlaps_DetectsExact()
    {
        // A: |---------------|
        // B: |---------------|
        
        var a = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(12));
        var b = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(12));

        bool result = a.Overlaps(b);
        
        Assert.That(result, Is.True);
    }
    
    [Test]
    [Ignore("This creates extra complication and might not be needed")]
    public void Overlaps_IgnoresAdjacent()
    {
        // A: |---------------|
        // B:                 |---------------|

        var a = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(10));
        var b = new DateRange(DateTime.Today.AddHours(10), DateTime.Today.AddHours(11));

        bool result = a.Overlaps(b);
        
        Assert.That(result, Is.False);
    }
    
    [Test]
    public void Overlaps_IgnoresSeparate()
    {
        // A: |---------------|
        // B:                       |---------------|

        var a = new DateRange(DateTime.Today.AddHours(9), DateTime.Today.AddHours(10));
        var b = new DateRange(DateTime.Today.AddHours(12), DateTime.Today.AddHours(13));

        bool result = a.Overlaps(b);
        
        Assert.That(result, Is.False);
    }
}