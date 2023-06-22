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

namespace Application.Queries.GetAllClients
{
    public class GetAllPersonalClientsHandler : IQueryHandler<GetAllPersonalClientsQuery, List<PersonalClient>>
    {
        private readonly IRepository<PersonalClient> _clientRepository;
        private readonly IRepository<Location> _locationRepository;

        public GetAllPersonalClientsHandler(IRepository<PersonalClient> clientRepository, IRepository<Location> locationRepository)
        {
            _clientRepository = clientRepository;
            _locationRepository = locationRepository;
        }

        public async Task<Result<List<PersonalClient>>> Handle(GetAllPersonalClientsQuery request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetByIdAsync(request.LocationId, cancellationToken);
            if (location is null)
                return Result.Error("Given location does not exist.");

            var clients = await _clientRepository.ListAsync(new PersonalClientsInLocationSpec(location), cancellationToken);
            return Result.Success(clients);
        }
    }
}
