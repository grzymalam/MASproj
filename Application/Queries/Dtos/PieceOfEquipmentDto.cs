using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Dtos
{
    public abstract class PieceOfEquipmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public EquipmentState State { get; set; }
        public double PricePerDay { get; set; }
        public DateTime LastInspection { get; set; }
        public double Mass { get; set; }
        public LocationDto Location { get; set; }
    }
}
