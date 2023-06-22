using Ardalis.Specification;
using Domain.Models;
using Domain.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public class PersonalClientsInLocationSpec: Specification<PersonalClient>
    {
        public PersonalClientsInLocationSpec(Location location)
        {
            Query
                .Where(cl => cl.ClientsLocations.Where(clo => clo.TimeLeft == null).First().Location == location);
        }
    }
}
