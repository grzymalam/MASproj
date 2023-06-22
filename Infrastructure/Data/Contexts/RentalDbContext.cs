using Domain.Models;
using Domain.Models.Client;
using Domain.Models.Employees;
using Domain.Models.Equipment;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Contexts
{
    public class RentalDbContext: DbContext
    {
        public DbSet<Rental> Rentals => Set<Rental>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Transport> Transports => Set<Transport>();
        public DbSet<Location> Locations => Set<Location>();
        public DbSet<ClientsLocation> ClientLocations => Set<ClientsLocation>();
        public DbSet<PieceOfEquipment> PiecesOfEquipment => Set<PieceOfEquipment>();
        public DbSet<EquipmentAccessory> EquipmentAccessories => Set<EquipmentAccessory>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Client> Clients => Set<Client>();
        public RentalDbContext(DbContextOptions options): base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PieceOfEquipment>()
                .HasDiscriminator<string>("EquipmentType")
                .HasValue<Excavator>("Excavator")
                .HasValue<Loader>("Loader")
                .HasValue<DumpTruck>("DumpTruck");

            modelBuilder.Entity<PieceOfEquipment>()
                .Navigation(poe => poe.Location)
                .AutoInclude();

            modelBuilder.Entity<Employee>()
                .HasDiscriminator<string>("EmployeeType")
                .HasValue<Mechanic>("Mechanic")
                .HasValue<Salesman>("Salesman");

            modelBuilder.Entity<Client>()
                .HasDiscriminator<string>("ClientType")
                .HasValue<PersonalClient>("PersonalClient")
                .HasValue<BusinessClient>("BusinessClient");

            modelBuilder.Entity<Rental>()
                .HasMany(r => r.EquipmentRented)
                .WithMany(poe => poe.Rentals);

            modelBuilder.Entity<Rental>()
                .HasMany(r => r.Accessories)
                .WithMany(a => a.RentedIn);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.ClientLocations)
                .WithOne(c => c.Location);

            modelBuilder.Entity<Location>()
                .HasMany(l => l.PiecesOfEquipment)
                .WithOne(poe => poe.Location);

            modelBuilder.Entity<ClientsLocation>()
                .HasOne(c => c.Client)
                .WithMany(cl => cl.ClientsLocations);

            modelBuilder.Entity<Client>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Client);

            modelBuilder.Entity<PieceOfEquipment>()
                .HasMany(poe => poe.Fits)
                .WithMany(a => a.Fits);

            modelBuilder.Entity<Transport>()
                .HasMany(tr => tr.Orders)
                .WithOne(poe => poe.Transport);

            modelBuilder.Entity<Transport>()
                .HasOne(t => t.To)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Transport>()
                .HasOne(t => t.From)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PieceOfEquipment>()
               .HasKey(poe => poe.Id);

            modelBuilder.Entity<PieceOfEquipment>()
                .Property(poe => poe.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<EquipmentAccessory>()
               .HasKey(r => r.Id);

            modelBuilder.Entity<EquipmentAccessory>()
                .Property(ea => ea.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<EquipmentAccessory>()
                .HasOne(ea => ea.Location)
                .WithMany(l => l.EquipmentAccessories)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
               .HasKey(e => e.Id);

            modelBuilder.Entity<Employee>()
                .Property(e => e.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Location)
                .WithMany(l => l.Employees)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Client>()
               .HasKey(c => c.Id);

            modelBuilder.Entity<Client>()
                .Property(c => c.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<ClientsLocation>()
                .HasKey(cl => cl.Id);

            modelBuilder.Entity<ClientsLocation>()
                .Property(cl => cl.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Transport>()
                .HasKey(tr => tr.Id);

            modelBuilder.Entity<Transport>()
                .Property(tr => tr.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<Order>()
                .Property(o => o.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Location>()
                .HasKey(l => l.Id);

            modelBuilder.Entity<Location>()
                .Property(l => l.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Rental>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Rental>()
                .Property(r => r.Id)
                .ValueGeneratedNever();

            base.OnModelCreating(modelBuilder);
        }
    }
}
