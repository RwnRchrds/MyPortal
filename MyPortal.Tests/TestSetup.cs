using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Models;

namespace MyPortal.Tests
{
    public class TestSetup
    {
        public ApplicationDbContext GetTestData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("MyPortalTest")
                .Options;

            var context = new ApplicationDbContext(options);

            context.UpdateBasedata();
        }
    }
}
