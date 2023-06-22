using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Equipment
{
    public class EquipmentAccessory
    {
        public EquipmentAccessory(string name, Location location)
        {
            Id = Guid.NewGuid();
            Name = name;
            IsRented = false;
            Location = location;
        }
        protected EquipmentAccessory() { }

        public Guid Id { get; set; }
        public Location Location { get; set; }
        public string Name { get; set; }
        public bool IsRented { get; set; }
        public double PricePerDay { get; set; }
        public ICollection<PieceOfEquipment> Fits { get; set; } = new List<PieceOfEquipment>();
        public ICollection<Rental> RentedIn { get; set; } = new List<Rental>();
    }
}
