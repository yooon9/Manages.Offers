namespace Manage.Offers.Data.Context
{
    using Manage.Offers.Data.Entities;
    using Microsoft.EntityFrameworkCore;

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Broker> Brokers { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Parcel> Parcels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Broker>()
                .Property(e => e.Type)
                .HasConversion<string>();

            modelBuilder
                .Entity<Parcel>()
                .Property(e => e.LandUseGroup)
                .HasConversion<string>();
        }
    }
}
