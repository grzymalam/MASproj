using Application.Abstractions.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CreateNewRental
{
    public class CreateNewRentalCommand: ICommand
    {
        public Guid SalesmanId { get; set; }
        public Guid ClientId { get; set; }
        public Guid PieceOfEquipmentId { get; set; }
        public List<Guid> AccessoryIds { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
