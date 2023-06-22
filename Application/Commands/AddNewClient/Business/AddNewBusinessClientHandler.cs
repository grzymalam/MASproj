using Application.Abstractions.Command;
using Ardalis.Result;
using Domain.Models;
using Domain.Models.Client;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Application.Commands.AddNewClient.Business
{
    public class AddNewBusinessClientHandler : ICommandHandler<AddNewBusinessClientCommand, BusinessClient>
    {
        private readonly IRepository<BusinessClient> _clientRepository;
        private readonly IRepository<Location> _locationRepository;

        public AddNewBusinessClientHandler(IRepository<BusinessClient> clientRepository, IRepository<Location> locationRepository)
        {
            _clientRepository = clientRepository;
            _locationRepository = locationRepository;
        }

        public async Task<Result<BusinessClient>> Handle(AddNewBusinessClientCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetByIdAsync(request.LocationId, cancellationToken);

            if (location is null)
                return Result.Error("The location does not exist");

            var client = new BusinessClient(request.Nip, request.Discount);

            var clientsLocation = new ClientsLocation(DateTime.UtcNow, client, location);

            client.ClientsLocations.Add(clientsLocation);

            await _clientRepository.AddAsync(client, cancellationToken);

            await _clientRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(client);
        }
    }
}
