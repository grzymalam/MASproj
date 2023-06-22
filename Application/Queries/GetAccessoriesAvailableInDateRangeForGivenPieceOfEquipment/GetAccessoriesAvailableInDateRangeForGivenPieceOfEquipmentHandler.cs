using Application.Abstractions.Query;
using Application.Queries.Dtos;
using Ardalis.Result;
using Domain.Models.Equipment;
using Domain.Specifications;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetAccessoriesAvailableInDateRangeForGivenPieceOfEquipment
{
    public class GetAccessoriesAvailableInDateRangeForGivenPieceOfEquipmentHandler
        : IQueryHandler<GetAccessoriesAvailableInDateRangeForGivenPieceOfEquipmentQuery, List<AccessoryDto>>
    {
        private readonly IRepository<EquipmentAccessory> _accessoriesRepository;
        private readonly IRepository<Loader> _loaderRepository;
        private readonly IRepository<DumpTruck> _dumpTruckRepository;
        private readonly IRepository<Excavator> _excavatorRepository;

        public GetAccessoriesAvailableInDateRangeForGivenPieceOfEquipmentHandler(IRepository<EquipmentAccessory> accessoriesRepository, IRepository<Loader> loaderRepository, IRepository<DumpTruck> dumpTruckRepository, IRepository<Excavator> excavatorRepository)
        {
            _accessoriesRepository = accessoriesRepository;
            _loaderRepository = loaderRepository;
            _dumpTruckRepository = dumpTruckRepository;
            _excavatorRepository = excavatorRepository;
        }

        public async Task<Result<List<AccessoryDto>>> Handle(GetAccessoriesAvailableInDateRangeForGivenPieceOfEquipmentQuery request, CancellationToken cancellationToken)
        {
            var loader = await _loaderRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);
            var excavator = await _excavatorRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);
            var dumpTruck = await _dumpTruckRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);

            var pieceOfEquipment = loader as PieceOfEquipment ?? excavator as PieceOfEquipment ?? dumpTruck;

            var accessories = await _accessoriesRepository.ListAsync(new AccessoryAvailableInDateRangeAndFittingPieceOfEquipmentSpec(request.From, request.To, pieceOfEquipment), cancellationToken);

            var accessoryDtos = accessories.Select(acc => AccessoryDto.FromEntity(acc)).ToList();

            return Result.Success(accessoryDtos);
        }
    }
}
