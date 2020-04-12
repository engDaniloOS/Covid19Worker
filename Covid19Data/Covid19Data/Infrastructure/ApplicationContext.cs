using Covid19Data.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Covid19Data.Infrastructure
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) { }

        #region Properties
        public DbSet<DayData> DayDatas { get; set; }
        public DbSet<StateInfo> StateInfos { get; set; }
        public DbSet<DestinationEmail> DestinationEmails { get; set; } 
        #endregion

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<DayData>().HasKey(d => d.Id);
            builder.Entity<StateInfo>().HasKey(d => d.Id);
            builder.Entity<DestinationEmail>().HasKey(d => d.Id);
        }

    }
}
