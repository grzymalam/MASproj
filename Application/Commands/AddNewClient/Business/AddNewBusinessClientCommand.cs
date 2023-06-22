using Application.Abstractions.Command;
using Domain.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.AddNewClient.Business
{
    public class AddNewBusinessClientCommand : ICommand<BusinessClient>
    {
        public Guid LocationId { get; set; }
        public string Nip { get; set; }
        public double Discount { get; set; }
    }
}
