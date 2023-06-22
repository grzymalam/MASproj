using Application.Queries.GetAllLocations;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly IMediator _bus;

        public LocationController(IMediator bus)
        {
            _bus = bus;
        }
        [HttpGet]
        public async Task<ActionResult<List<Location>>> GetAll()
        {
            var result = await _bus.Send(new GetAllLocationsQuery());

            return result.Value;
        }
    }
}
