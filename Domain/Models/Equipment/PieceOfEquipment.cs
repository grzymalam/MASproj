using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Equipment
{
    public abstract class PieceOfEquipment
    {
        protected PieceOfEquipment(DateTime dateOfPurchase, double mileage, double pricePerDay, DateTime lastInspection, double mass, string name)
        {
            Id = Guid.NewGuid();
            DateOfPurchase = dateOfPurchase;
            Mileage = mileage;
            PricePerDay = pricePerDay;
            LastInspection = lastInspection;
            Mass = mass;
            Name = name;
        }
        protected PieceOfEquipment() { }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public double Mileage { get; set; }
        public EquipmentState State { get; set; }
        public double PricePerDay { get; set; }
        public DateTime LastInspection { get; set; }
        public double Mass { get; set; }
        public Location Location { get; set; }
        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
        public ICollection<EquipmentAccessory> Fits { get; set; } = new List<EquipmentAccessory>();
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public abstract double CalculateEarnedMoney();
    }
    public enum EquipmentState
    {
        Available,Rented,ToBeTransported,InTransit
    }
}
