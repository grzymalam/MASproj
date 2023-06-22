using Application.Queries.GetAllPiecesOfEquipment;
using Domain.Models.Equipment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ardalis.Result;
using Api.Dtos.Equipment;
using Application.Queries.GetPiecesOfEquipmentAvailableInDateRange;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IMediator _bus;
        public EquipmentController(IMediator bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<ActionResult<EquipmentListDto>> GetAll()
        {
            var result = await _bus.Send(new GetAllPiecesOfEquipmentQuery());

            return result.Value;
        }
        [HttpPost]
        public async Task<ActionResult<EquipmentListDto>> PiecesOfEquipmentAvailableInDateRange([FromBody]EquipmentInDateRangeDto dto)
        {
            var result = await _bus.Send(new GetPiecesOfEquipmentAvailableInDateRangeQuery
            {
                From = dto.From,
                To = dto.To,
            });

            return result.Value;
        }
    }
}
