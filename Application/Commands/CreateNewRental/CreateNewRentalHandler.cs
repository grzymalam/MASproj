using Application.Abstractions.Command;
using Ardalis.Result;
using Domain.Models;
using Domain.Models.Employees;
using Domain.Models.Equipment;
using Domain.Specifications;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CreateNewRental
{
    public class CreateNewRentalHandler : ICommandHandler<CreateNewRentalCommand>
    {
        private readonly IRepository<Rental> _rentalRepository;
        private readonly IRepository<Salesman> _salesmanRepository;
        private readonly IRepository<Loader> _loaderRepository;
        private readonly IRepository<Excavator> _excavatorRepository;
        private readonly IRepository<DumpTruck> _dumpTruckRepository;
        private readonly IRepository<EquipmentAccessory> _accessoryRepository;

        public CreateNewRentalHandler(IRepository<Rental> rentalRepository, IRepository<Salesman> salesmanRepository, IRepository<EquipmentAccessory> accessoryRepository, IRepository<Loader> loaderRepository, IRepository<Excavator> excavatorRepository, IRepository<DumpTruck> dumpTruckRepository)
        {
            _rentalRepository = rentalRepository;
            _salesmanRepository = salesmanRepository;
            _accessoryRepository = accessoryRepository;
            _loaderRepository = loaderRepository;
            _excavatorRepository = excavatorRepository;
            _dumpTruckRepository = dumpTruckRepository;
        }

        public async Task<Result> Handle(CreateNewRentalCommand request, CancellationToken cancellationToken)
        {
            if (request.From > request.To)
                return Result.Error("Start can not occur after end");

            if(request.From < DateTime.UtcNow)
                return Result.Error("Start can not be in the past");


            var loader = await _loaderRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);
            var excavator = await _excavatorRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);
            var dumpTruck = await _dumpTruckRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);

            var salesman = await _salesmanRepository.GetByIdAsync(request.SalesmanId, cancellationToken);
            var accessories = await _accessoryRepository.ListAsync(new AccessoryIdEqualToAnyIdProvidedSpec(request.AccessoryIds), cancellationToken);

            var pieceOfEquipment = loader as PieceOfEquipment ?? excavator as PieceOfEquipment ?? dumpTruck;

            if (pieceOfEquipment is null)
                return Result.Error("This piece of equipment does not exist.");

            if (salesman is null)
                return Result.Error("Salesman does not exist.");

            var rentalsForThisPieceOfEquipment = await _rentalRepository.ListAsync(new RentalsForPieceOfEquipmentSpec(pieceOfEquipment), cancellationToken);
            var overlapping = rentalsForThisPieceOfEquipment.Where(r => request.From < r.End || request.To > r.Start);

            var rentalsForAccessories = await _rentalRepository.ListAsync(new RentalsForAccessorySpec(accessories), cancellationToken);
            var overlappingAccRentals = rentalsForAccessories.Where(r => request.From < r.End || request.To > r.Start);

            if (overlapping.Any())
                return Result.Error("There are overlapping rentals for piece of equipment.");

            if (overlappingAccRentals.Any())
                return Result.Error("There are overlapping rentals for accessories.");
            var rental = new Rental(salesman, request.From, request.To);

            if (pieceOfEquipment is null)
                return Result.Error("Piece of equipment does not exist.");


            rental.AddEquipmentRented(pieceOfEquipment);

            accessories.ForEach(acc =>
                rental.AddAccessory(acc)
            );

            pieceOfEquipment.State = EquipmentState.Rented;

            await _rentalRepository.AddAsync(rental, cancellationToken);
            await _rentalRepository.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
