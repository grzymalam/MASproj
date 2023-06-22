using Api.Dtos.Transport;
using Application.Commands.CreateNewFastTransport;
using Application.Commands.CreateNewSlowTransport;
using Application.Queries.Dtos;
using Application.Queries.GetFastTransportEstimate;
using Application.Queries.GetSlowTransportEstimate;
using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportController : ControllerBase
    {
        private readonly IMediator _bus;
        public TransportController(IMediator bus)
        {
            _bus = bus;
        }

        [HttpPost("estimate/slow")]
        public async Task<ActionResult<TransportDto>> GetSlowTransportEstimate([FromBody] TransportInfoDto dto)
        {
            var result = await _bus.Send(new GetSlowTransportEstimateQuery
            {
                PieceOfEquipmentId = dto.PieceOfEquipmentId,
                LocationFromId = dto.FromLocationId,
                LocationToId = dto.ToLocationId,
                ClientId = dto.ClientId
            });

            return result.Value;
        }
        [HttpPost("estimate/fast")]
        public async Task<ActionResult<TransportDto>> GetFastTransportEstimate([FromBody] TransportInfoDto dto)
        {
            var result = await _bus.Send(new GetFastTransportEstimateQuery
            {
                PieceOfEquipmentId = dto.PieceOfEquipmentId,
                LocationFromId = dto.FromLocationId,
                LocationToId = dto.ToLocationId,
                ClientId = dto.ClientId
            });

            return result.Value;
        }

        [HttpPost("slow")]
        public async Task<ActionResult> CreateSlowTransport([FromBody] TransportInfoDto dto)
        {
            var result = await _bus.Send(new CreateNewSlowTransportCommand
            {
                PieceOfEquipmentId = dto.PieceOfEquipmentId,
                LocationFromId = dto.FromLocationId,
                LocationToId = dto.ToLocationId,
                ClientId = dto.ClientId
            });

            return result.IsSuccess ? Ok() : NotFound();
        }
        [HttpPost("fast")]
        public async Task<ActionResult> CreateFastTransport([FromBody] TransportInfoDto dto)
        {
            var result = await _bus.Send(new CreateNewFastTransportCommand
            {
                PieceOfEquipmentId = dto.PieceOfEquipmentId,
                LocationFromId = dto.FromLocationId,
                LocationToId = dto.ToLocationId,
                ClientId = dto.ClientId
            });

            return result.IsSuccess ? Ok() : NotFound();
        }
    }
}
