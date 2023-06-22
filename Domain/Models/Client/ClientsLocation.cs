using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class ClientsLocation
    {
        public ClientsLocation(DateTime timeJoined, Client.Client client, Location location, DateTime? timeLeft = null)
        {
            Id = Guid.NewGuid();
            TimeJoined = timeJoined;
            TimeLeft = timeLeft;
            Client = client;
            Location = location;
        }
        protected ClientsLocation() { }

        public Guid Id { get; set; }
        public DateTime TimeJoined { get; set; }
        public DateTime? TimeLeft { get; set; }
        public Client.Client Client { get; set; }
        public Location Location { get; set; }
    }
}
