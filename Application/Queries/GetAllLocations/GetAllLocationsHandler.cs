using Application.Abstractions.Query;
using Ardalis.Result;
using Domain.Models;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetAllLocations
{
    public class GetAllLocationsHandler : IQueryHandler<GetAllLocationsQuery, List<Location>>
    {
        private readonly IRepository<Location> _locationsRepository;

        public GetAllLocationsHandler(IRepository<Location> locationsRepository)
        {
            _locationsRepository = locationsRepository;
        }

        public async Task<Result<List<Location>>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
        {
            var locations = await _locationsRepository.ListAsync(cancellationToken);

            return Result.Success(locations);
        }
    }
}
