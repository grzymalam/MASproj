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
    public class RentalsForAccessorySpec: Specification<Rental>
    {
        public RentalsForAccessorySpec(List<EquipmentAccessory> accs)
        {
            Query
                .Where(r => r.Accessories.Intersect(accs).Any());
        }
    }
}
