using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Client
{
    public abstract class Client
    {
        public Client()
        {
            Id = Guid.NewGuid();
            AmountPaid = 0;
            AmountOwed = 0;
        }
        public Guid Id { get; set; }
        public double AmountPaid { get; set; }
        public double AmountOwed { get; set; }

        public ICollection<ClientsLocation> ClientsLocations = new List<ClientsLocation>();
        public ICollection<Transport> Transports = new List<Transport>();
        public ICollection<Order> Orders = new List<Order>();
    }
}
