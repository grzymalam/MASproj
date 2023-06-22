using Application.Abstractions.Query;
using Application.Queries.Dtos;
using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetAccessoriesAvailableInDateRangeForGivenPieceOfEquipment
{
    public class GetAccessoriesAvailableInDateRangeForGivenPieceOfEquipmentQuery: IQuery<List<AccessoryDto>>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid PieceOfEquipmentId { get; set; }
        public Guid LocationId { get; set; }
    }
}
