using Domain.Models;

namespace Application.Queries.Dtos
{
    public class TransportDto
    {
        public double Cost { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public LocationDto From { get; set; }
        public LocationDto To { get; set; }
    }
}
