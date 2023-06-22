using Domain.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Order
    {
        public Order(double amount, Client.Client client, Transport transport, PieceOfEquipment pieceOfEquipment)
        {
            Id = Guid.NewGuid();
            OrderDate = DateTimeOffset.UtcNow;
            Amount = amount;
            PieceOfEquipment = pieceOfEquipment;
            Client = client;
            Transport = transport;
        }
        protected Order() { }

        public Guid Id { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public double Amount { get; set; }
        public PieceOfEquipment PieceOfEquipment { get; set; }
        public Client.Client Client { get; set; }
        public Transport Transport { get; set; }
    }
}
