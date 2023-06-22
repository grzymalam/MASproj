using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Equipment
{
    public class Excavator : PieceOfEquipment
    {
        public Excavator(DateTime dateOfPurchase, double mileage, double pricePerDay, 
            DateTime lastInspection, double mass, double armLength, bool isTracked, string name) 
            : base(dateOfPurchase, mileage, pricePerDay, lastInspection, mass, name)
        {
            ArmLength = armLength;
            IsTracked = isTracked;
        }

        public double ArmLength { get; set; }
        public bool IsTracked { get; set; }
        public override double CalculateEarnedMoney()
        {
            throw new NotImplementedException();
        }
    }
}
