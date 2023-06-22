using Application.Abstractions.Query;
using Application.Queries.Dtos;
using Ardalis.Result;
using Domain.Models;
using Domain.Models.Client;
using Domain.Models.Equipment;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetSlowTransportEstimate
{
    public class GetSlowTransportEstimateHandler : IQueryHandler<GetSlowTransportEstimateQuery, TransportDto>
    {
        private readonly IRepository<Loader> _loaderRepository;
        private readonly IRepository<Excavator> _excavatorRepository;
        private readonly IRepository<DumpTruck> _dumpTruckRepository;
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<EquipmentAccessory> _accessoryRepository;
        private readonly IRepository<BusinessClient> _businessClientRepository;
        private readonly IRepository<PersonalClient> _individualClientRepository;
        private readonly IRepository<Transport> _transportRepository;

        public GetSlowTransportEstimateHandler(IRepository<Loader> loaderRepository,
            IRepository<Excavator> excavatorRepository, IRepository<DumpTruck> dumpTruckRepository,
            IRepository<Location> locationRepository, IRepository<EquipmentAccessory> accessoryRepository, IRepository<Transport> transportRepository, IRepository<BusinessClient> businessClientRepository, IRepository<PersonalClient> individualClientRepository)
        {
            _loaderRepository = loaderRepository;
            _excavatorRepository = excavatorRepository;
            _dumpTruckRepository = dumpTruckRepository;
            _locationRepository = locationRepository;
            _accessoryRepository = accessoryRepository;
            _transportRepository = transportRepository;
            _businessClientRepository = businessClientRepository;
            _individualClientRepository = individualClientRepository;
        }

        public async Task<Result<TransportDto>> Handle(GetSlowTransportEstimateQuery request, CancellationToken cancellationToken)
        {
            var from = await _locationRepository.GetByIdAsync(request.LocationFromId, cancellationToken);
            var to = await _locationRepository.GetByIdAsync(request.LocationToId, cancellationToken);
            var loader = await _loaderRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);
            var excavator = await _excavatorRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);
            var dumpTruck = await _dumpTruckRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);

            var pieceOfEquipment = loader as PieceOfEquipment ?? excavator as PieceOfEquipment ?? dumpTruck;

            if (pieceOfEquipment is null)
            {
                return Result.Error();
            }
            if (from is null && to is null)
                return Result.Error();

            var personalClient = await _individualClientRepository.GetByIdAsync(request.ClientId, cancellationToken);
            var businessClient = await _businessClientRepository.GetByIdAsync(request.ClientId, cancellationToken);

            if (personalClient is null && businessClient is null)
                return Result.Error();

            var discount = 100.0;

            if (businessClient is not null)
                discount -= businessClient.Discount;

            discount /= 100;

            var existingTransports = await _transportRepository.ListAsync(cancellationToken);
            var sameRouteTransports = existingTransports.Where(t => t.From == from && t.To == to);
            var upcomingTransports = sameRouteTransports.Where(t => t.DateOfDeparture > DateTime.UtcNow.AddHours(1));
            var transportsWithEnoughSpace = upcomingTransports.Where(t => t.Orders.Sum(o => o.PieceOfEquipment.Mass) < 28000);
            var distance = from.CalculateDistanceFrom(to);

            if (transportsWithEnoughSpace.Count() > 0) {
                var availableTransport = transportsWithEnoughSpace.FirstOrDefault();
                return Result.Success(new TransportDto
                {
                    Cost = (pieceOfEquipment.PricePerDay * pieceOfEquipment.Mass / 28000 + distance * 0.5) * discount,
                    DateOfDeparture = availableTransport.DateOfDeparture,
                    From = LocationDto.FromEntity(from),
                    To = LocationDto.FromEntity(to)
                });
            }

            var dto = new TransportDto
            {
                Cost = (pieceOfEquipment.PricePerDay * pieceOfEquipment.Mass / 28000 + distance) * discount,
                DateOfDeparture = DateTime.UtcNow,
                From = LocationDto.FromEntity(from),
                To = LocationDto.FromEntity(to)
            };

            return Result.Success(dto);
        }
    }
}
