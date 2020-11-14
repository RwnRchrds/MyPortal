﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyPortal.Database.Models;

namespace MyPortal.Database.Helpers
{
    public class DesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private const string ConnectionString = @"data source=NEBULA\DEVELOPMENT;initial catalog=MyPortal;persist security info=True;user id=sa;password=63X3-xo33-cpso;multipleactiveresultsets=True;application name=MyPortal";

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(ConnectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}