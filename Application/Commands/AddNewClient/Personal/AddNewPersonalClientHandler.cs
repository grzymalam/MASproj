using Application.Abstractions.Command;
using Application.Commands.AddNewClient.Dtos;
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

namespace Application.Commands.AddNewClient.Personal
{
    public class AddNewPersonalClientHandler : ICommandHandler<AddNewPersonalClientCommand, PersonalClient>
    {
        private readonly IRepository<PersonalClient> _clientRepository;
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<ClientsLocation> _clientsLocationRepository;

        public AddNewPersonalClientHandler(IRepository<PersonalClient> clientRepository, IRepository<Location> locationRepository, IRepository<ClientsLocation> clientsLocationRepository)
        {
            _clientRepository = clientRepository;
            _locationRepository = locationRepository;
            _clientsLocationRepository = clientsLocationRepository;
        }

        public async Task<Result<PersonalClient>> Handle(AddNewPersonalClientCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetByIdAsync(request.LocationId, cancellationToken);

            if (location is null)
                return Result.Error("The location does not exist");

            var client = new PersonalClient(request.Pesel, request.Name, request.Lastname);

            var clientsLocation = new ClientsLocation(DateTime.UtcNow, client, location);
            client.ClientsLocations.Add(clientsLocation);

            await _clientRepository.AddAsync(client, cancellationToken);
            //await _clientsLocationRepository.AddAsync(clientsLocation, cancellationToken);

            using TransactionScope scope = new();

            await _clientRepository.SaveChangesAsync(cancellationToken);
            await _clientsLocationRepository.SaveChangesAsync(cancellationToken);

            scope.Complete();

            return Result.Success(client);
        }
    }
}
