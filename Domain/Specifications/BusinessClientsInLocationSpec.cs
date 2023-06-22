using Ardalis.Specification;
using Domain.Models.Client;
using Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public class BusinessClientsInLocationSpec : Specification<BusinessClient>
    {
        public BusinessClientsInLocationSpec(Location location)
        {
            Query
                   .Where(cl => cl.ClientsLocations.Where(clo => clo.TimeLeft == null).First().Location == location);
        }
    }
}
