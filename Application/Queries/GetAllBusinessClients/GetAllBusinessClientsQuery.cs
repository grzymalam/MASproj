using Application.Abstractions.Query;
using Domain.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetAllBusinessClients
{
    public class GetAllBusinessClientsQuery : IQuery<List<BusinessClient>>
    {
        public Guid LocationId { get; set; }
    }
}
