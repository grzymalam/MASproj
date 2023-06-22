using Application.Abstractions.Query;
using Application.Queries.GetAllPiecesOfEquipment;
using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetPiecesOfEquipmentAvailableInDateRange
{
    public class GetPiecesOfEquipmentAvailableInDateRangeQuery: IQuery<EquipmentListDto>
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
