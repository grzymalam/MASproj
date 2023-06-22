using Application.Abstractions.Query;
using Application.Queries.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetSlowTransportEstimate
{
    public class GetSlowTransportEstimateQuery: IQuery<TransportDto>
    {
        public Guid PieceOfEquipmentId { get; set; }
        public Guid LocationFromId { get; set; }
        public Guid LocationToId { get; set; }
        public Guid ClientId { get; set; }
    }
}
