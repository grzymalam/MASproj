using Api.Dtos.Equipment;
using Application.Queries.Dtos;
using Application.Queries.GetAccessoriesAvailableInDateRangeForGivenPieceOfEquipment;
using Domain.Models.Equipment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessoryController : ControllerBase
    {
        private readonly IMediator _bus;
        public AccessoryController(IMediator bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<List<AccessoryDto>> GetAccessoriesFittingEquipmentIdDateRangeInLocation([FromBody] AccessoryFittingEquipmentIdDateRangeInLocationDto dto)
        {
            var result = await _bus.Send(new GetAccessoriesAvailableInDateRangeForGivenPieceOfEquipmentQuery
            {
                From = dto.From,
                To = dto.To,
                PieceOfEquipmentId = dto.PieceOfEquipmentId,
                LocationId = dto.LocationId
            });

            return result.Value;
        }
    }
}
