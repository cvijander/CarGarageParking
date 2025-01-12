using CarGarageParking.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGarageParking
{
    public class CarGarageParkingDBContext : DbContext
    {
        public static readonly ILoggerFactory loggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });

        public CarGarageParkingDBContext(DbContextOptions<CarGarageParkingDBContext> options) : base(options) { }

        public DbSet<Application> Applications { get; set; }

        public DbSet<Garage> Garages    { get; set; }

        public DbSet<Owner> Owners { get; set; }

        public DbSet<Payment> Payments  { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        public DbSet<VehicleInGarage> VehicleInGarages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Garage>()
                 .HasMany(g => g.VehicleInGarages)
                 .WithOne(vg => vg.Garage)
                 .HasForeignKey(vg => vg.GarageId)
                 .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Owner>()
                .HasMany(o => o.Vehicles)
                .WithOne(v => v.Owner )
                .HasForeignKey(v => v.OwnerId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Application>()
                .Property(a => a.Credit)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.TotalCharge)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
               .Property(p => p.VehicleHourlyRate)
               .HasPrecision(18, 2);

            modelBuilder.Entity<VehicleInGarage>()
               .HasOne(vg => vg.Garage)
               .WithMany(g => g.VehicleInGarages)
               .HasForeignKey(vg => vg.GarageId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VehicleInGarage>()
                .HasOne(vg => vg.Vehicle)
                .WithMany(v => v.VehicleInGarages)
                .HasForeignKey(vg => vg.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<VehicleInGarage>()
               .HasOne(vg => vg.Owner)
               .WithMany(o => o.VehicleInGarages)
               .HasForeignKey(vg => vg.OwnerId)
               .OnDelete(DeleteBehavior.SetNull); 

            modelBuilder.Entity<VehicleInGarage>()
                .Property(vg => vg.HourlyRate)
                .HasPrecision(18, 2);

        }

    }
}
