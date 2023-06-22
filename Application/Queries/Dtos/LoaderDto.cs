using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Dtos
{
    public class LoaderDto: PieceOfEquipmentDto
    {
        public LoaderType LoaderType { get; set; }
        public double Width { get; set; }
        public static LoaderDto FromEntity(Loader loader)
        {
            return new LoaderDto
            {
                Id = loader.Id,
                Name = loader.Name,
                DateOfPurchase = loader.DateOfPurchase,
                State = loader.State,
                PricePerDay = loader.PricePerDay,
                LastInspection = loader.LastInspection,
                Mass = loader.Mass,
                Location = LocationDto.FromEntity(loader.Location),
                LoaderType = loader.LoaderType,
                Width = loader.Width
            };
        }
    }
}
