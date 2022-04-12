﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MyPortal.Database.Models;

namespace MyPortal.Database.Helpers
{
    public class DesignTimeFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private const string ConnectionString = @"data source=SNOWPIERCER\DEVELOPMENT;initial catalog=MyPortal;persist security info=True;user id=sa;password=IZc%5&7vt7SVWQDtMaZ5;multipleactiveresultsets=True;application name=MyPortal";
        //private const string ConnectionString = @"data source=localhost\DEVELOPMENT;initial catalog=MyPortal;persist security info=True;user id=sa;password=ILSils123!;multipleactiveresultsets=True;application name=MyPortal";

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(ConnectionString);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
