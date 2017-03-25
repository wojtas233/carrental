namespace CarRental.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Entities;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity.Infrastructure;

    public interface IApplicationDbContext
    {
        DbSet<Vehicle> Vehicles { get; set; }
        DbSet<VehicleType> VehicleTypes { get; set; }
        DbSet<Availability> Availabilities { get; set; }
        DbSet<Reservation> Reservations { get; set; }
        DbSet<Location> Locations { get; set; }
        DbSet<Image> Images { get; set; }
        DbSet<VehicleImage> VehicleImages { get; set; }
        DbSet<Equipment> Equipments { get; set; }
        IDbSet<ApplicationUser> Users { get; set; }
        IDbSet<IdentityRole> Roles { get; set; }
        DbSet<ReservationEquipment> ReservationEquipments { get; set; }

        DbContextConfiguration Configuration { get; }
        int SaveChanges();
        void Dispose();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext()
            : base("CarRental", throwIfV1Schema: false)
        {
            //Database.Delete();
            //Database.CreateIfNotExists();
            //var a = Database.Connection.ConnectionString;
            //Console.WriteLine(a);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<VehicleImage> VehicleImages { get; set; }
        public DbSet<Equipment> Equipments { get; set; }
        public DbSet<ReservationEquipment> ReservationEquipments { get; set; }
    }
}
