using Ardalis.Specification;
using Domain.Models;
using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public class RentalsForPieceOfEquipmentSpec: Specification<Rental>
    {
        public RentalsForPieceOfEquipmentSpec(PieceOfEquipment p)
        {
            Query
                .Where(r => r.EquipmentRented.Contains(p));
        }
    }
}
