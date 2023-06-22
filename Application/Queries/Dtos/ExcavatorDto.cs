using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Dtos
{
    public class ExcavatorDto: PieceOfEquipmentDto
    {
        public double ArmLength { get; set; }
        public bool IsTracked { get; set; }
        public static ExcavatorDto FromEntity(Excavator excavator)
        {
            return new ExcavatorDto
            {
                Id = excavator.Id,
                Name = excavator.Name,
                DateOfPurchase = excavator.DateOfPurchase,
                State = excavator.State,
                PricePerDay = excavator.PricePerDay,
                LastInspection = excavator.LastInspection,
                Mass = excavator.Mass,
                Location = LocationDto.FromEntity(excavator.Location),
                ArmLength = excavator.ArmLength,
                IsTracked = excavator.IsTracked
            };
        }
    }
}
