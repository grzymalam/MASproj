using Domain.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Dtos
{
    public class SalesmanDto
    {
        public Guid SalesmanId { get; set; }
        public uint AmountOfRentedPiecesOfEquipment { get; set; }
        public double HourlyWage { get; set; }
        public uint HoursWorked { get; set; }
        public DateTime EmployedDate { get; set; }
        public string Pesel { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public LocationDto Location { get; set; }
        public static SalesmanDto FromEntity(Salesman salesman) => new()
        {
            SalesmanId = salesman.Id,
            AmountOfRentedPiecesOfEquipment = salesman.AmountOfRentedPiecesOfEquipment,
            HourlyWage = salesman.HourlyWage,
            HoursWorked = salesman.HoursWorked,
            EmployedDate = salesman.EmployedDate,
            Pesel = salesman.Pesel,
            Name = salesman.Name,
            Lastname = salesman.Lastname,
            Location = LocationDto.FromEntity(salesman.Location)
        };
    }
}
