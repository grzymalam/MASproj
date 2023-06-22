using Domain.Models.Equipment;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Infrastructure.Data.Contexts;
using Domain.Models.Employees;
using Domain.Models.Client;

namespace Api
{
    public static class SeedData
    {
        public static readonly Location Location = new("Biuro warszawa", "Warszawa", "Marszalkowska", 150, "05-081", 21.0122, 52.2297);
        public static readonly Location Location1 = new("Biuro lublin", "Lublin", "Zlomiarka", 69, "12-381", 22.5684, 51.2465);
        public static readonly Loader Loader = new(DateTime.UtcNow, 4503, 300, DateTime.UtcNow.AddDays(-94), 6000, "Ladowarka", LoaderType.Articulated, 230);
        public static readonly Excavator Excavator = new(DateTime.UtcNow.AddDays(-1953), 14032, 240, DateTime.UtcNow.AddDays(36), 3500, 4, true, "Koparka");
        public static readonly DumpTruck DumpTruck = new(DateTime.UtcNow.AddDays(-3423), 160345, 150, DateTime.UtcNow.AddDays(-145), 2400, 1500, 120, "Wywrotka");
        public static readonly EquipmentAccessory AccessoryForLoader = new("Duza lyzka", Location);
        public static readonly EquipmentAccessory AccessoryForExcavator = new("Lyzka do piasku", Location);
        public static readonly EquipmentAccessory AccessoryForExcavator2 = new("Lyzka szeroka", Location1);
        public static readonly Salesman Salesman1 = new("123123123", "Jan", "Kowalski", 25, 125, DateTime.UtcNow.Date, 13, Location);
        public static readonly Salesman Salesman2 = new("897897977", "Adam", "Nowak", 29, 98, DateTime.UtcNow.Date.AddDays(-123), 95, Location1);
        public static readonly PersonalClient Client1 = new("69696969966", "Klient", "Indywidualny");
        public static readonly PersonalClient Client2 = new("71731723712", "Drugi", "Indywidualny");
        public static readonly BusinessClient Client3 = new("989348394", 10);
        public static void Initialize(IServiceProvider serviceProvider)
        {
            Loader.Location = Location;
            Excavator.Location = Location;
            DumpTruck.Location = Location1;
            Loader.Fits.Add(AccessoryForLoader);
            Excavator.Fits.Add(AccessoryForExcavator);
            Excavator.Fits.Add(AccessoryForExcavator2);
            Client1.ClientsLocations.Add(new ClientsLocation(DateTime.UtcNow.Date, Client1, Location));
            Client2.ClientsLocations.Add(new ClientsLocation(DateTime.UtcNow.Date, Client2, Location));
            Client2.ClientsLocations.Add(new ClientsLocation(DateTime.UtcNow.Date, Client3, Location1));

            using (var dbContext = new RentalDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<RentalDbContext>>()))
            {
                //if (dbContext.PiecesOfEquipment.Any())
                //{
                //    return;   // DB has been seeded
                //}

                PopulateTestData(dbContext);


            }
        }
        public static void PopulateTestData(RentalDbContext dbContext)
        {
            foreach (var item in dbContext.Rentals)
            {
                dbContext.Remove(item);
            }
            foreach (var item in dbContext.Orders)
            {
                dbContext.Remove(item);
            }
            foreach (var item in dbContext.Transports)
            {
                dbContext.Remove(item);
            }
            foreach (var item in dbContext.Locations)
            {
                dbContext.Remove(item);
            }
            foreach (var item in dbContext.ClientLocations)
            {
                dbContext.Remove(item);
            }
            foreach (var item in dbContext.PiecesOfEquipment)
            {
                dbContext.Remove(item);
            }
            foreach (var item in dbContext.EquipmentAccessories)
            {
                dbContext.Remove(item);
            }
            foreach (var item in dbContext.Employees)
            {
                dbContext.Remove(item);
            }
            foreach (var item in dbContext.Clients)
            {
                dbContext.Remove(item);
            }

            dbContext.SaveChanges();
            
            dbContext.Employees.Add(Salesman1);
            dbContext.Employees.Add(Salesman2);

            dbContext.Clients.Add(Client1);
            dbContext.Clients.Add(Client2);
            dbContext.Clients.Add(Client3);

            dbContext.PiecesOfEquipment.Add(Loader);
            dbContext.PiecesOfEquipment.Add(Excavator);
            dbContext.PiecesOfEquipment.Add(DumpTruck);

            dbContext.SaveChanges();
        }
    }
}
