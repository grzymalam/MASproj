using Application.Abstractions.Query;
using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetAvailableAccessoriesForPieceOfEquipment
{
    public class GetAvailableAccessoriesForPieceOfEquipmentQuery: IQuery<List<EquipmentAccessory>>
    {
        public Guid PieceOfEquipmentId { get; set; }
    }
}
