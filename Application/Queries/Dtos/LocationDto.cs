using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Dtos
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public uint StreetNumber { get; set; }
        public string PostalCode { get; set; }

        public static LocationDto FromEntity(Location location)
        {
            return new LocationDto
            {
                Id = location.Id,
                Name = location.Name,
                City = location.City,
                Street = location.Street,
                StreetNumber = location.StreetNumber,
                PostalCode = location.PostalCode
            };
        }
    }
}
