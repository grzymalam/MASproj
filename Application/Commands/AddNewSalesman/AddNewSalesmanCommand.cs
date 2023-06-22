using Application.Abstractions.Command;
using Application.Queries.Dtos;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.AddNewSalesman
{
    public class AddNewSalesmanCommand: ICommand<SalesmanDto>
    {
        public double HourlyWage { get; set; }
        public uint HoursWorked { get; set; } = 0;
        public DateTime EmployedDate { get; set; } = DateTime.UtcNow.Date;
        public Guid Id { get; set; }
        public string Pesel { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public Guid LocationId { get; set; }
        public uint AmountOfRentedPiecesOfEquipment { get; set; }
    }
}
