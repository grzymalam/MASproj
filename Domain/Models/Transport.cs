using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Transport
    {
        public Transport(Location from, Location to, DateTime dateOfDeparture, TransportType type)
        {
            Id = Guid.NewGuid();
            From = from;
            To = to;
            DateOfDeparture = dateOfDeparture;
            Type = type;
        }
        protected Transport() { }

        public Guid Id { get; set; }
        public Location From { get; set; }
        public Location To { get; set; }
        public TransportType Type { get; set; }
        public DateTime DateOfDeparture { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
    public enum TransportType
    {
        Fast,Slow
    }
}
