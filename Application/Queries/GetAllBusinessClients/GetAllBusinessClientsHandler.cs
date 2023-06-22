using Application.Abstractions.Query;
using Ardalis.Result;
using Domain.Models;
using Domain.Models.Client;
using Domain.Specifications;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetAllBusinessClients
{
    public class GetAllBusinessClientsHandler : IQueryHandler<GetAllBusinessClientsQuery, List<BusinessClient>>
    {
        private readonly IRepository<BusinessClient> _clientRepository;
        private readonly IRepository<Location> _locationRepository;

        public GetAllBusinessClientsHandler(IRepository<BusinessClient> clientRepository, IRepository<Location> locationRepository)
        {
            _clientRepository = clientRepository;
            _locationRepository = locationRepository;
        }

        public async Task<Result<List<BusinessClient>>> Handle(GetAllBusinessClientsQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetByIdAsync(request.LocationId, cancellationToken);

            if (location is null)
                return Result.Error("The location does not exist");

            var clients = await _clientRepository.ListAsync(new BusinessClientsInLocationSpec(location), cancellationToken);

            return Result.Success(clients);
        }
    }
}
