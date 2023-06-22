using Ardalis.Specification;
using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public class PieceOfEquipmentAvailableInDateRangeSpec<T>: Specification<T> where T: PieceOfEquipment 
    {
        public PieceOfEquipmentAvailableInDateRangeSpec(DateTime from, DateTime to)
        {
            Query
                .Where(poe => poe.Rentals.All(r => (from > r.End) && (to < r.Start)))
                .Where(poe => poe.State != EquipmentState.ToBeTransported)
                .Include(poe => poe.Location);
                
        }
    }
}
