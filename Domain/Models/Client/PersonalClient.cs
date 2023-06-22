using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Client
{
    public class PersonalClient : Client
    {
        public PersonalClient(string pesel, string name, string lastname): base()
        {
            Pesel = pesel;
            Name = name;
            Lastname = lastname;
        }
        public string Pesel { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        protected PersonalClient() { }
    }
}
