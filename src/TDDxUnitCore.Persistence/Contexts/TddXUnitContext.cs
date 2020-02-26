using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TDDxUnitCore.Domain.Courses;

namespace TDDxUnitCore.Persistence.Contexts
{
    public class TddXUnitContext : DbContext
    {
        public TddXUnitContext(DbContextOptions<TddXUnitContext> options) : base(options)
        {
                
        }

        public DbSet<Course> Courses { get; set; }



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
