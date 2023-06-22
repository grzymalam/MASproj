using Application.Abstractions.Query;
using Application.Queries.Dtos;
using Ardalis.Result;
using Domain.Models.Equipment;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetAllPiecesOfEquipment
{
    public class GetAllPiecesOfEquipmentHandler : IQueryHandler<GetAllPiecesOfEquipmentQuery, EquipmentListDto>
    {
        private readonly IRepository<Loader> _loaderRepository;
        private readonly IRepository<Excavator> _excavatorRepository;
        private readonly IRepository<DumpTruck> _dumpTruckRepository;

        public GetAllPiecesOfEquipmentHandler(IRepository<Loader> loaderRepository, IRepository<Excavator> excavatorRepository, IRepository<DumpTruck> dumpTruckRepository)
        {
            _loaderRepository = loaderRepository;
            _excavatorRepository = excavatorRepository;
            _dumpTruckRepository = dumpTruckRepository;
        }

        public async Task<Result<EquipmentListDto>> Handle(GetAllPiecesOfEquipmentQuery request, CancellationToken cancellationToken)
        {
            var loaders = await _loaderRepository.ListAsync(cancellationToken);
            var excavators = await _excavatorRepository.ListAsync(cancellationToken);
            var dumpTrucks = await _dumpTruckRepository.ListAsync(cancellationToken);

            var loaderDtos = loaders.Select(loader => LoaderDto.FromEntity(loader)).ToList();
            var excavatorDtos = excavators.Select(excavator => ExcavatorDto.FromEntity(excavator)).ToList();
            var dumpTruckDtos = dumpTrucks.Select(dumpTruck => DumpTruckDto.FromEntity(dumpTruck)).ToList();

            var listOfEquipment = new EquipmentListDto
            {
                Loaders = loaderDtos,
                Excavators = excavatorDtos,
                DumpTrucks = dumpTruckDtos
            };

            return Result.Success(listOfEquipment);
        }
    }
}
