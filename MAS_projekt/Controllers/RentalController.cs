using Api.Dtos.Rental;
using Application.Commands.CreateNewRental;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _bus;
        public RentalController(IMediator bus)
        {
            _bus = bus;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddRentalDto dto)
        {
            await _bus.Send(new CreateNewRentalCommand
            {
                SalesmanId = dto.SalesmanId,
                ClientId = dto.ClientId,
                PieceOfEquipmentId = dto.PieceOfEquipmentId,
                AccessoryIds = dto.AccessoryIds,
                From = dto.From,
                To = dto.To
            });

            return Ok();
        }
    }
}
