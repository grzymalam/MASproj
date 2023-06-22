using Application.Abstractions.Query;
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

namespace Application.Queries.NewFolder
{
    public class GetSalesmenHandler : IQueryHandler<GetSalesmenQuery, List<SalesmanDto>>
    {
        private readonly IRepository<Salesman> _salesmanRepository;
        private readonly IRepository<Location> _locationRepository;

        public GetSalesmenHandler(IRepository<Salesman> salesmanRepository, IRepository<Location> locationRepository)
        {
            _salesmanRepository = salesmanRepository;
            _locationRepository = locationRepository;
        }

        public async Task<Result<List<SalesmanDto>>> Handle(GetSalesmenQuery request, CancellationToken cancellationToken)
        {
            var salesmen = await _salesmanRepository.ListAsync(cancellationToken);

            var location = await _locationRepository.GetByIdAsync(request.LocationId);

            if (location is null)
                return Result.Error("Location does not exist.");

            salesmen = salesmen.Where(salesman => salesman.Location == location).ToList();

            return Result.Success(salesmen.Select(salesman => SalesmanDto.FromEntity(salesman)).ToList());
        }
    }
}
