using Application.Commands.AddNewSalesman;
using Application.Queries.Dtos;
using Application.Queries.GetAllClients;
using Application.Queries.NewFolder;
using Domain.Models;
using Domain.Models.Client;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesmenController : ControllerBase
    {
        private readonly IMediator _bus;
        public SalesmenController(IMediator bus)
        {
            _bus = bus;
        }

        [HttpGet("{locationId}")]
        public async Task<ActionResult<List<SalesmanDto>>> GetAll([FromRoute]Guid locationId)
        {
            var result = await _bus.Send(new GetSalesmenQuery
            {
                LocationId = locationId
            });

            return result.Value;
        }
        [HttpPost]
        public async Task<ActionResult<SalesmanDto>> AddSalesman([FromBody] SalesmanDto dto)
        {
            var result = await _bus.Send(new AddNewSalesmanCommand
            {
                HourlyWage = dto.HourlyWage, 
                HoursWorked = dto.HoursWorked,
                EmployedDate = dto.EmployedDate,
                Id = dto.SalesmanId,
                Pesel = dto.Pesel,
                Name = dto.Name,
                Lastname = dto.Lastname,
                LocationId = dto.Location.Id,
                AmountOfRentedPiecesOfEquipment = dto.AmountOfRentedPiecesOfEquipment

            });

            return result.Value;
        }
    }
}
