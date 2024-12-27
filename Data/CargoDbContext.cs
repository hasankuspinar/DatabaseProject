using CS306_Phase2.Models;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace CS306_Phase2.Data
{
    public partial class CargoDbContext : DbContext
    {
        public CargoDbContext(DbContextOptions<CargoDbContext> options) : base(options) { }

        public DbSet<CargoCompany> CargoCompany { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CS306_Phase2.Models.Route> Route { get; set; }
        public DbSet<Courier> Courier { get; set; }
        public DbSet<TransportVehicle> TransportVehicle { get; set; }
        public DbSet<Shipment> Shipment { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<Tracking> Tracking { get; set; }


        // Override OnModelCreating if necessary to configure relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Existing configurations...

            // Configure ShipmentViewModel as a keyless entity
            modelBuilder.Entity<ShipmentViewModel>(entity =>
            {
                entity.HasNoKey();
                entity.ToView(null); // Indicates that this entity doesn't map to a database table or view
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
