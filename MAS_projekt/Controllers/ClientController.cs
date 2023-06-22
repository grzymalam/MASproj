using Api.Dtos.Client;
using Api.NewFolder.NewFolder;
using Application.Commands.AddNewClient.Business;
using Application.Commands.AddNewClient.Dtos;
using Application.Commands.AddNewClient.Personal;
using Application.Queries.GetAllBusinessClients;
using Application.Queries.GetAllClients;
using Domain.Models.Client;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMediator _bus;

        public ClientController(IMediator bus)
        {
            _bus = bus;
        }

        [HttpGet("individual/{locationId}")]
        public async Task<ActionResult<List<PersonalClient>>> GetAllPersonal(Guid locationId)
        {
            var result = await _bus.Send(new GetAllPersonalClientsQuery
            {
                LocationId = locationId
            });

            return result.Value;
        }
        [HttpGet("business/{locationId}")]
        public async Task<ActionResult<List<BusinessClient>>> GetAllBusiness(Guid locationId)
        {
            var result = await _bus.Send(new GetAllBusinessClientsQuery
            {
                LocationId = locationId
            });

            return result.Value;
        }
        [HttpPost("individual")]
        public async Task<ActionResult<PersonalClient>> AddIndividualClient([FromBody]AddIndividualClientDto dto)
        {
            var result = await _bus.Send(new AddNewPersonalClientCommand
            {
                LocationId = dto.LocationId,
                Pesel = dto.Pesel,
                Name = dto.Name,
                Lastname = dto.Lastname
            });

            return result.Value;
        }
        [HttpPost("business")]
        public async Task<ActionResult<BusinessClient>> AddBusinessClient([FromBody] AddBusinessClientDto dto)
        {
            var result = await _bus.Send(new AddNewBusinessClientCommand
            {
                LocationId = dto.LocationId,
                Nip = dto.Nip,
                Discount = dto.Discount
            });

            return result.Value;
        }
    }
}
