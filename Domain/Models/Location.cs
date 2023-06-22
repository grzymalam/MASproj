using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Location
    {
        public Location(string name, string city, string street, uint streetNumber, string postalCode, double longitude, double latitude)
        {
            Id = Guid.NewGuid();
            Name = name;
            City = city;
            Street = street;
            StreetNumber = streetNumber;
            PostalCode = postalCode;
            Longitude = longitude;
            Latitude = latitude;
        }

        public double CalculateDistanceFrom(Location l)
        {
            double dLat = ToRadians(l.Latitude - Latitude);
            double dLon = ToRadians(l.Longitude - Longitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(Latitude)) * Math.Cos(ToRadians(Latitude)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = 6371 * c;

            return distance;
        }
        private double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
        protected Location() { }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public uint StreetNumber { get; set; }
        public string PostalCode { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public ICollection<PieceOfEquipment> PiecesOfEquipment { get; set; } = new List<PieceOfEquipment>();
        public ICollection<EquipmentAccessory> EquipmentAccessories { get; set; } = new List<EquipmentAccessory>();
        public ICollection<ClientsLocation> ClientLocations { get; set; } = new List<ClientsLocation>();
        public ICollection<Employees.Employee> Employees { get; set; } = new List<Employees.Employee>();
    }
}
