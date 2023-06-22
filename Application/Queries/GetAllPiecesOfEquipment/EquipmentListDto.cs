using Application.Queries.Dtos;
using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetAllPiecesOfEquipment
{
    public class EquipmentListDto
    {
        public List<LoaderDto> Loaders { get; set; }
        public List<ExcavatorDto> Excavators { get; set; }
        public List<DumpTruckDto> DumpTrucks { get; set; }
    }
}
