using Application.Abstractions.Query;
using Application.Queries.Dtos;
using Application.Queries.GetAllPiecesOfEquipment;
using Ardalis.Result;
using Domain.Models.Equipment;
using Domain.Specifications;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetPiecesOfEquipmentAvailableInDateRange
{
    public class GetPiecesOfEquipmentAvailableInDateRangeHandler : IQueryHandler<GetPiecesOfEquipmentAvailableInDateRangeQuery, EquipmentListDto>
    {
        private readonly IRepository<Loader> _loaderRepository;
        private readonly IRepository<Excavator> _excavatorRepository;
        private readonly IRepository<DumpTruck> _dumpTruckRepository;

        public GetPiecesOfEquipmentAvailableInDateRangeHandler(IRepository<Loader> loaderRepository, IRepository<Excavator> excavatorRepository, IRepository<DumpTruck> dumpTruckRepository)
        {
            _loaderRepository = loaderRepository;
            _excavatorRepository = excavatorRepository;
            _dumpTruckRepository = dumpTruckRepository;
        }

        public async Task<Result<EquipmentListDto>> Handle(GetPiecesOfEquipmentAvailableInDateRangeQuery request, CancellationToken cancellationToken)
        {
            var loaders = await _loaderRepository.ListAsync(new PieceOfEquipmentAvailableInDateRangeSpec<Loader>(request.From, request.To), cancellationToken);
            var excavators = await _excavatorRepository.ListAsync(new PieceOfEquipmentAvailableInDateRangeSpec<Excavator>(request.From, request.To), cancellationToken);
            var dumpTrucks = await _dumpTruckRepository.ListAsync(new PieceOfEquipmentAvailableInDateRangeSpec<DumpTruck>(request.From, request.To), cancellationToken);

            var loaderDtos = loaders.Select(loader => LoaderDto.FromEntity(loader)).ToList();
            var excavatorDtos = excavators.Select(excavator => ExcavatorDto.FromEntity(excavator)).ToList();
            var dumpTruckDtos = dumpTrucks.Select(dumpTruck => DumpTruckDto.FromEntity(dumpTruck)).ToList();

            var availableEquipment = new EquipmentListDto
            {
                Loaders = loaderDtos,
                Excavators = excavatorDtos,
                DumpTrucks = dumpTruckDtos
            };

            return Result.Success(availableEquipment);
        }
    }
}
