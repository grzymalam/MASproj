using Ardalis.Specification;
using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specifications
{
    public class AccessoryAvailableInDateRangeAndFittingPieceOfEquipmentSpec: Specification<EquipmentAccessory>
    {
        public AccessoryAvailableInDateRangeAndFittingPieceOfEquipmentSpec(DateTime from, DateTime to, PieceOfEquipment pieceOfEquipment)
        {
            Query
                .Where(acc => acc.Fits.Contains(pieceOfEquipment))
                .Where(acc => acc.RentedIn.All(r => (from > r.End) && (to < r.Start)))
                .Where(acc => acc.Location == pieceOfEquipment.Location);
        }
    }
}
