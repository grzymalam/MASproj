using Application.Abstractions.Query;
using Application.Queries.Dtos;
using Ardalis.Result;
using Domain.Models.Client;
using Domain.Models.Equipment;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedKernel;

namespace Application.Queries.GetFastTransportEstimate
{
    public class GetFastTransportEstimateHandler : IQueryHandler<GetFastTransportEstimateQuery, TransportDto>
    {
        private readonly IRepository<Transport> _transportRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Loader> _loaderRepository;
        private readonly IRepository<Excavator> _excavatorRepository;
        private readonly IRepository<DumpTruck> _dumpTruckRepository;
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<BusinessClient> _businessClientRepository;
        private readonly IRepository<PersonalClient> _individualClientRepository;

        public GetFastTransportEstimateHandler(
            IRepository<Transport> transportRepository,
            IRepository<Loader> loaderRepository,
            IRepository<Excavator> excavatorRepository,
            IRepository<DumpTruck> dumpTruckRepository,
            IRepository<Location> locationRepository,
            IRepository<BusinessClient> businessClientRepository,
            IRepository<PersonalClient> individualClientRepository,
            IRepository<Order> orderRepository)
        {
            _transportRepository = transportRepository;
            _loaderRepository = loaderRepository;
            _excavatorRepository = excavatorRepository;
            _dumpTruckRepository = dumpTruckRepository;
            _locationRepository = locationRepository;
            _businessClientRepository = businessClientRepository;
            _individualClientRepository = individualClientRepository;
            _orderRepository = orderRepository;
        }
        public async Task<Result<TransportDto>> Handle(GetFastTransportEstimateQuery request, CancellationToken cancellationToken)
        {
            var loader = await _loaderRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);
            var excavator = await _excavatorRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);
            var dumpTruck = await _dumpTruckRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);

            var pieceOfEquipment = loader as PieceOfEquipment ?? excavator as PieceOfEquipment ?? dumpTruck;

            if (pieceOfEquipment is null)
            {
                return Result.Error();
            }

            var from = await _locationRepository.GetByIdAsync(request.LocationFromId, cancellationToken);
            var to = await _locationRepository.GetByIdAsync(request.LocationToId, cancellationToken);

            if (from is null || to is null)
                return Result.Error();

            var personalClient = await _individualClientRepository.GetByIdAsync(request.ClientId, cancellationToken);
            var businessClient = await _businessClientRepository.GetByIdAsync(request.ClientId, cancellationToken);

            var client = personalClient as Client ?? businessClient;

            if (client is null)
                return Result.Error();

            return Result.Success(new TransportDto
            {
                Cost = (pieceOfEquipment.PricePerDay + pieceOfEquipment.Mass/28000) * 3,
                DateOfDeparture = DateTime.UtcNow.AddDays(1).Date.AddHours(8),
                From = LocationDto.FromEntity(from),
                To = LocationDto.FromEntity(to)
            });
        }
    }
}
