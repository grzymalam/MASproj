using Application.Abstractions.Query;
using Ardalis.Result;
using Domain.Models.Equipment;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.GetAvailableAccessoriesForPieceOfEquipment
{
    public class GetAvailableAccessoriesForPieceOfEquipmentHandler : IQueryHandler<GetAvailableAccessoriesForPieceOfEquipmentQuery, List<EquipmentAccessory>>
    {
        private readonly IRepository<PieceOfEquipment> _equipmentRepository;

        public GetAvailableAccessoriesForPieceOfEquipmentHandler(IRepository<PieceOfEquipment> equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        public async Task<Result<List<EquipmentAccessory>>> Handle(GetAvailableAccessoriesForPieceOfEquipmentQuery request, CancellationToken cancellationToken)
        {
            var pieceOfEquipment = await _equipmentRepository.GetByIdAsync(request.PieceOfEquipmentId, cancellationToken);

            if (pieceOfEquipment is null)
                return Result.Error("Given piece of equipment does not exist.");

            var accessories = pieceOfEquipment.Fits.Where(accessory => !accessory.IsRented).ToList();

            return Result.Success(accessories);
        }
    }
}
