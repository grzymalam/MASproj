using Application.Abstractions.Query;
using Domain.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetAllClients
{
    public class GetAllPersonalClientsQuery : IQuery<List<PersonalClient>>
    {
        public Guid LocationId { get; set; }
    }
}
