using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Dtos
{
    public class DumpTruckDto: PieceOfEquipmentDto
    {
        public int LoadCapacity { get; set; }
        public int MaxSpeed { get; set; }
        public static DumpTruckDto FromEntity(DumpTruck dumpTruck)
        {
            return new DumpTruckDto
            {
                Id = dumpTruck.Id,
                Name = dumpTruck.Name,
                DateOfPurchase = dumpTruck.DateOfPurchase,
                State = dumpTruck.State,
                PricePerDay = dumpTruck.PricePerDay,
                LastInspection = dumpTruck.LastInspection,
                Mass = dumpTruck.Mass,
                Location = LocationDto.FromEntity(dumpTruck.Location),
                LoadCapacity = dumpTruck.LoadCapacity,
                MaxSpeed = dumpTruck.MaxSpeed
            };
        }
    }
}
