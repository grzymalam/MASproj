﻿using Application.Abstractions.Command;
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

namespace Application.Commands.CreateNewSlowTransport
{
    public class CreateNewSlowTransportHandler : ICommandHandler<CreateNewSlowTransportCommand>
    {
        private readonly IRepository<Transport> _transportRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Loader> _loaderRepository;
        private readonly IRepository<Excavator> _excavatorRepository;
        private readonly IRepository<DumpTruck> _dumpTruckRepository;
        private readonly IRepository<Location> _locationRepository;
        private readonly IRepository<BusinessClient> _businessClientRepository;
        private readonly IRepository<PersonalClient> _individualClientRepository;

        public CreateNewSlowTransportHandler(
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
        public async Task<Result> Handle(CreateNewSlowTransportCommand request, CancellationToken cancellationToken)
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

            var discount = 100.0;

            if (businessClient is not null)
                discount -= businessClient.Discount;

            discount /= 100;

            var existingTransports = await _transportRepository.ListAsync(cancellationToken);
            var sameRouteTransports = existingTransports.Where(t => t.From == from && t.To == to);
            var upcomingTransports = sameRouteTransports.Where(t => t.DateOfDeparture > DateTime.UtcNow.AddHours(1));
            var transportsWithEnoughSpace = upcomingTransports.Where(t => t.Orders.Sum(o => o.PieceOfEquipment.Mass) < 28000);
            var distance = from.CalculateDistanceFrom(to);

            if(transportsWithEnoughSpace.Count() > 0)
            {
                var availableTransport = transportsWithEnoughSpace.FirstOrDefault();
                var orderInAvailableTransport = new Order((pieceOfEquipment.PricePerDay * pieceOfEquipment.Mass / 28000 + distance * 0.5) * discount, client, availableTransport, pieceOfEquipment);
                await _orderRepository.AddAsync(orderInAvailableTransport, cancellationToken);
                await _orderRepository.SaveChangesAsync(cancellationToken);
                return Result.Success();
            }

            var transport = new Transport(from, to, DateTime.UtcNow.AddDays(1).Date.AddHours(8), TransportType.Fast);
            var order = new Order((pieceOfEquipment.PricePerDay * pieceOfEquipment.Mass / 28000 + distance) * discount, client, transport, pieceOfEquipment);
            
            pieceOfEquipment.State = EquipmentState.ToBeTransported;

            await _transportRepository.AddAsync(transport, cancellationToken);
            await _orderRepository.AddAsync(order, cancellationToken);

            await _individualClientRepository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
