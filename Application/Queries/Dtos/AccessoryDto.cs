using Domain.Models;
using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Dtos
{
    public class AccessoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsRented { get; set; }
        public double PricePerDay { get; set; }
        public static AccessoryDto FromEntity(EquipmentAccessory acc) =>
            new AccessoryDto
            {
                Id = acc.Id,
                Name = acc.Name,
                IsRented = acc.IsRented,
                PricePerDay = acc.PricePerDay
            };
    }
}
