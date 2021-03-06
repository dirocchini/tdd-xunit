﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TDDxUnitCore.Domain.Courses;
using TDDxUnitCore.Domain.Students;

namespace TDDxUnitCore.Persistence.Contexts
{
    public class TddXUnitContext : DbContext
    {
        public TddXUnitContext(DbContextOptions<TddXUnitContext> options) : base(options)
        {
            if (Database.GetPendingMigrations().Count() > 0)
                Database.Migrate();
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


        public async Task Commit()
        {
            await SaveChangesAsync();
        }
    }
}
