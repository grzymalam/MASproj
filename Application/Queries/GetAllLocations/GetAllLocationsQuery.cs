using Application.Abstractions.Query;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetAllLocations
{
    public class GetAllLocationsQuery: IQuery<List<Location>>
    {
    }
}
