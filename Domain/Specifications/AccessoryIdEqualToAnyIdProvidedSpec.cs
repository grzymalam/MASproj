using Ardalis.Specification;
using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public class AccessoryIdEqualToAnyIdProvidedSpec: Specification<EquipmentAccessory>
    {
        public AccessoryIdEqualToAnyIdProvidedSpec(List<Guid> ids)
        {
            Query
                .Where(acc => ids.Contains(acc.Id));
        }
    }
}
