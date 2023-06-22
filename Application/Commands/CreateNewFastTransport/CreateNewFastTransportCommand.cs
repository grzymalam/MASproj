using Application.Abstractions.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.CreateNewFastTransport
{
    public class CreateNewFastTransportCommand: ICommand
    {
        public Guid PieceOfEquipmentId { get; set; }
        public Guid LocationFromId { get; set; }
        public Guid LocationToId { get; set; }
        public Guid ClientId { get; set; }

    }
}
