using Application.Abstractions.Command;
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

namespace Application.Commands.CreateNewFastTransport
{
    public class CreateNewFastTransportHandler : ICommandHandler<CreateNewFastTransportCommand>
    {
        private readonly IRepository<Transport> _transportRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Loader> _loaderRepository;
        private readonly IRepository<Excavator> _excavatorRepository;
        private readonly IRepository<DumpTruck> _dumpTruckRepository;
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<BusinessClient> _businessClientRepository;
        private readonly IRepository<PersonalClient> _individualClientRepository;

        public CreateNewFastTransportHandler(
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

        public async Task<Result> Handle(CreateNewFastTransportCommand request, CancellationToken cancellationToken)
        {
            var loader = await _loaderRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);
            var excavator = await _excavatorRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);
            var dumpTruck = await _dumpTruckRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);

            var pieceOfEquipment = loader as PieceOfEquipment ?? excavator as PieceOfEquipment ?? dumpTruck;

            if(pieceOfEquipment is null)
            {
                return Result.Error();
            }

            var from = await _locationRepository.GetByIdAsync(request.LocationFromId, cancellationToken);
            var to = await _locationRepository.GetByIdAsync(request.LocationToId, cancellationToken);


            if (from is null || to is null)
                return Result.Error();

            var personalClient = await _individualClientRepository.GetByIdAsync(request.ClientId, cancellationToken);
            var businessClient = await _businessClientRepository.GetByIdAsync(request.ClientId, cancellationToken);

            var client = personalClient as Client ?? businessClient as Client; 

            if (client is null)
                return Result.Error();

            var transport = new Transport(from, to, DateTime.UtcNow.AddDays(1).Date.AddHours(8), TransportType.Fast);
            var order = new Order((pieceOfEquipment.PricePerDay + pieceOfEquipment.Mass) * 3, client, transport, pieceOfEquipment);

            pieceOfEquipment.State = EquipmentState.ToBeTransported;

            await _transportRepository.AddAsync(transport, cancellationToken);
            await _orderRepository.AddAsync(order, cancellationToken);
            
            await _individualClientRepository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
