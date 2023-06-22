using Application.Abstractions.Command;
using Ardalis.Result;
using Domain.Models.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands.AddNewClient.Personal
{
    public class AddNewPersonalClientCommand : ICommand<PersonalClient>
    {
        public Guid LocationId { get; set; }
        public string Pesel { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
    }
}
