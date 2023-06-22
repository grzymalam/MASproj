using Application.Abstractions.Query;
using Application.Queries.Dtos;
using Ardalis.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.NewFolder
{
    public class GetSalesmenQuery: IQuery<List<SalesmanDto>>
    {
        public Guid LocationId { get; set; }
    }
}
