using Application.Abstractions.Command;
using Application.Queries.Dtos;
using Ardalis.Result;
using Domain.Models;
using Domain.Models.Employees;
using SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.AddNewSalesman
{
    public class AddNewSalesmanHandler : ICommandHandler<AddNewSalesmanCommand, SalesmanDto>
    {
        private readonly IRepository<Salesman> _salesmanRepository;
        private readonly IRepository<Location> _locationRepository;

        public AddNewSalesmanHandler(IRepository<Salesman> salesmanRepository, IRepository<Location> locationRepository)
        {
            _salesmanRepository = salesmanRepository;
            _locationRepository = locationRepository;
        }

        public async Task<Result<SalesmanDto>> Handle(AddNewSalesmanCommand request, CancellationToken cancellationToken)
        {
            var location = await _locationRepository.GetByIdAsync(request.LocationId);

            if (location is null)
                return Result.Error("Location does not exist");

            var salesman = new Salesman(request.Pesel, request.Name, request.Lastname, request.HourlyWage, request.HoursWorked, request.EmployedDate, request.AmountOfRentedPiecesOfEquipment, location);

            await _salesmanRepository.AddAsync(salesman, cancellationToken);
            await _salesmanRepository.SaveChangesAsync(cancellationToken);

            return Result.Success(SalesmanDto.FromEntity(salesman));
        }
    }
}
