using Domain.Models.Employees;
using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Rental
    {
        public Rental(Salesman salesman, DateTime start, DateTime end)
        {
            Id = Guid.NewGuid();
            Salesman = salesman;
            Start = start;
            End = end;
        }
        protected Rental() { }
        public void AddEquipmentRented(PieceOfEquipment pieceOfEquipment)
        {
            EquipmentRented.Add(pieceOfEquipment);
            Cost += (End - Start).TotalDays * pieceOfEquipment.PricePerDay;
        }

        public void AddAccessory(EquipmentAccessory accessory)
        {
            if (Accessories.Count > 2)
                throw new ArgumentOutOfRangeException("There can only be 3 accessories rented at a time");
            Accessories.Add(accessory);
            Cost += (End - Start).TotalDays * accessory.PricePerDay;
        }
        public Guid Id { get; set; }
        public Salesman Salesman { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Cost { get; set; }
        public ICollection<PieceOfEquipment> EquipmentRented { get; set; } = new List<PieceOfEquipment>();
        public ICollection<EquipmentAccessory> Accessories { get; set; } = new List<EquipmentAccessory>();

    }
}
