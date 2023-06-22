using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.Models.Client
{
    public class BusinessClient: Client
    {
        public BusinessClient(string nip, double discount) : base()
        {
            NIP = nip;
            Discount = discount;
        }
        protected BusinessClient() { }
        public string NIP { get; set; }
        public double Discount { get; set; }
    }
}
