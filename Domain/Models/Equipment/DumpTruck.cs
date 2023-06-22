using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Equipment
{
    public class DumpTruck: PieceOfEquipment
    {
        public DumpTruck(DateTime dateOfPurchase, double mileage, double pricePerDay,
            DateTime lastInspection, double mass, int loadCapacity, int maxSpeed, string name)
            : base(dateOfPurchase, mileage, pricePerDay, lastInspection, mass, name)
        {
            LoadCapacity = loadCapacity;
            MaxSpeed = maxSpeed;
        }

        public int LoadCapacity { get; set; }
        public int MaxSpeed { get; set; }

        public override double CalculateEarnedMoney()
        {
            throw new NotImplementedException();
        }
    }
}
